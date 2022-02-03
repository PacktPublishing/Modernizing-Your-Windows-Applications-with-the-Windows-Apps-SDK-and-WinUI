// Taken and modified from the implementation from the patterns & practices PRISM Source Code.
// ------------------------------------------------------------------------------
//*********************************************************//
//    Copyright (c) Microsoft. All rights reserved.
//    
//    Apache 2.0 License
//    
//    You may obtain a copy of the License at
//    http://www.apache.org/licenses/LICENSE-2.0
//    
//    Unless required by applicable law or agreed to in writing, software 
//    distributed under the License is distributed on an "AS IS" BASIS, 
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or 
//    implied. See the License for the specific language governing 
//    permissions and limitations under the License.
//
//*********************************************************

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManager.Framework.Commands
{
    /// <summary>
    ///     An <see cref="ICommand" /> whose delegates can be attached for <see cref="Execute" /> and <see cref="CanExecute" />
    ///     .
    /// </summary>
    public abstract class DelegateCommandBase : ICommand
    {
        protected readonly Func<object, bool> _canExecuteMethod;
        protected readonly Func<object, Task> _executeMethod;
        private List<WeakReference> _canExecuteChangedHandlers;

        /// <summary>
        ///     Creates a new instance of a <see cref="DelegateCommandBase" />, specifying both the execute action and the can
        ///     execute function.
        /// </summary>
        /// <param name="executeMethod">The <see cref="Action" /> to execute when <see cref="ICommand.Execute" /> is invoked.</param>
        /// <param name="canExecuteMethod">
        ///     The <see cref="Func{Object,Bool}" /> to invoked when <see cref="ICommand.CanExecute" />
        ///     is invoked.
        /// </param>
        protected DelegateCommandBase(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
        {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException(nameof(executeMethod), "Delegates cannot be null");

            _executeMethod = arg =>
            {
                executeMethod(arg);
                return Task.Delay(0);
            };
            _canExecuteMethod = canExecuteMethod;
        }

        /// <summary>
        ///     Creates a new instance of a <see cref="DelegateCommandBase" />, specifying both the Execute action as an awaitable
        ///     Task and the CanExecute function.
        /// </summary>
        /// <param name="executeMethod">
        ///     The <see cref="Func{Object,Task}" /> to execute when <see cref="ICommand.Execute" /> is
        ///     invoked.
        /// </param>
        /// <param name="canExecuteMethod">
        ///     The <see cref="Func{Object,Bool}" /> to invoked when <see cref="ICommand.CanExecute" />
        ///     is invoked.
        /// </param>
        protected DelegateCommandBase(Func<object, Task> executeMethod, Func<object, bool> canExecuteMethod)
        {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException(nameof(executeMethod), "Delegates cannot be null");

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        /// <summary>
        ///     Reference to a busyIndicator indicator.
        /// </summary>
        public IBusyIndicator BusyIndicator { get; protected set; }

        async void ICommand.Execute(object parameter)
        {
            await Execute(parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute. You must keep a hard
        ///     reference to the handler to avoid garbage collection and unexpected results. See remarks for more information.
        /// </summary>
        /// <remarks>
        ///     When subscribing to the <see cref="ICommand.CanExecuteChanged" /> event using
        ///     code (not when binding using XAML) will need to keep a hard reference to the event handler. This is to prevent
        ///     garbage collection of the event handler because the command implements the Weak Event pattern so it does not have
        ///     a hard reference to this handler. An example implementation can be seen in the CompositeCommand and
        ///     CommandBehaviorBase
        ///     classes. In most scenarios, there is no reason to sign up to the CanExecuteChanged event directly, but if you do,
        ///     you
        ///     are responsible for maintaining the reference.
        /// </remarks>
        /// <example>
        ///     The following code holds a reference to the event handler. The myEventHandlerReference value should be stored
        ///     in an instance member to avoid it from being garbage collected.
        ///     <code>
        /// EventHandler myEventHandlerReference = new EventHandler(this.OnCanExecuteChanged);
        /// command.CanExecuteChanged += myEventHandlerReference;
        /// </code>
        /// </example>
        public virtual event EventHandler CanExecuteChanged
        {
            add { WeakEventHandlerManager.AddWeakReferenceHandler(ref _canExecuteChangedHandlers, value, 2); }
            remove { WeakEventHandlerManager.RemoveWeakReferenceHandler(_canExecuteChangedHandlers, value); }
        }

        /// <summary>
        ///     Raises <see cref="ICommand.CanExecuteChanged" /> on the UI thread so every
        ///     command invoker can requery <see cref="ICommand.CanExecute" />.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            WeakEventHandlerManager.CallWeakReferenceHandlers(this, _canExecuteChangedHandlers);
        }

        /// <summary>
        ///     Raises <see cref="DelegateCommandBase.CanExecuteChanged" /> on the UI thread so every command invoker
        ///     can requery to check if the command can execute.
        ///     <remarks>
        ///         Note that this will trigger the execution of <see cref="DelegateCommandBase.CanExecute" /> once for each
        ///         invoker.
        ///     </remarks>
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        /// <summary>
        ///     Executes the command with the provided parameter by invoking the <see cref="Action{Object}" /> supplied during
        ///     construction.
        /// </summary>
        /// <param name="parameter"></param>
        protected async Task Execute(object parameter)
        {
            if (BusyIndicator != null)
            {
                using (var busyScope = new BusyIndicatorScope(BusyIndicator))
                {
                    await _executeMethod(parameter);
                }
            }
            else
            {
                await _executeMethod(parameter);
            }
        }

        /// <summary>
        ///     Determines if the command can execute with the provided parameter by invoking the <see cref="Func{Object,Bool}" />
        ///     supplied during construction.
        /// </summary>
        /// <param name="parameter">The parameter to use when determining if this command can execute.</param>
        /// <returns>Returns <see langword="true" /> if the command can execute.  <see langword="False" /> otherwise.</returns>
        protected bool CanExecute(object parameter)
        {
            return _canExecuteMethod == null || _canExecuteMethod(parameter);
        }
    }

    /// <summary>
    ///     An <see cref="ICommand" /> whose delegates can be attached for <see cref="Execute" /> and <see cref="CanExecute" />
    ///     .
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    /// <remarks>
    ///     The constructor deliberately prevents the use of value types.
    ///     Because ICommand takes an object, having a value type for T would cause unexpected behavior when CanExecute(null)
    ///     is called during XAML initialization for command bindings.
    ///     Using default(T) was considered and rejected as a solution because the implementor would not be able to distinguish
    ///     between a valid and defaulted values.
    ///     <para />
    ///     Instead, callers should support a value type by using a nullable value type and checking the HasValue property
    ///     before using the Value property.
    ///     <example>
    ///         <code>
    /// public MyClass()
    /// {
    ///     this.submitCommand = new DelegateCommand&lt;int?&gt;(this.Submit, this.CanSubmit);
    /// }
    /// 
    /// private bool CanSubmit(int? customerId)
    /// {
    ///     return (customerId.HasValue &amp;&amp; customers.Contains(customerId.Value));
    /// }
    ///     </code>
    ///     </example>
    /// </remarks>
    public class DelegateCommand<T> : DelegateCommandBase
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="DelegateCommand{T}" />.
        /// </summary>
        /// <param name="executeMethod">
        ///     Delegate to execute when Execute is called on the command. This can be null to just hook up
        ///     a CanExecute delegate.
        /// </param>
        /// <remarks><see cref="CanExecute" /> will always return true.</remarks>
        public DelegateCommand(Action<T> executeMethod)
            : this(executeMethod, o => true)
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="DelegateCommand{T}" />.
        /// </summary>
        /// <param name="executeMethod">
        ///     Delegate to execute when Execute is called on the command. This can be null to just hook up
        ///     a CanExecute delegate.
        /// </param>
        /// <param name="canExecuteMethod">Delegate to execute when CanExecute is called on the command. This can be null.</param>
        /// <exception cref="ArgumentNullException">
        ///     When both <paramref name="executeMethod" /> and
        ///     <paramref name="canExecuteMethod" /> ar <see langword="null" />.
        /// </exception>
        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
            : base(o => executeMethod((T) o), o => canExecuteMethod((T) o))
        {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException(nameof(executeMethod), "Delegates cannot be null.");

            var genericTypeInfo = typeof (T).GetTypeInfo();

            // DelegateCommand allows object or Nullable<>.  
            // note: Nullable<> is a struct so we cannot use a class constraint.
            if (genericTypeInfo.IsValueType)
            {
                if ((!genericTypeInfo.IsGenericType) ||
                    (!typeof (Nullable<>).GetTypeInfo()
                        .IsAssignableFrom(genericTypeInfo.GetGenericTypeDefinition().GetTypeInfo())))
                {
                    throw new InvalidCastException("Invalid payload type.");
                }
            }
        }

        private DelegateCommand(Func<T, Task> executeMethod)
            : this(executeMethod, o => true)
        {
        }

        private DelegateCommand(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod)
            : base(o => executeMethod((T) o), o => canExecuteMethod((T) o))
        {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException(nameof(executeMethod), "Delegates cannot be null.");
        }

        /// <summary>
        ///     Factory method to create a new instance of <see cref="DelegateCommand{T}" /> from an awaitable handler method.
        /// </summary>
        /// <param name="executeMethod">Delegate to execute when Execute is called on the command.</param>
        /// <param name="busyIndicator">An object that supports the busyIndicator indicator interface.</param>
        /// <returns>Constructed instance of <see cref="DelegateCommand{T}" /></returns>
        public static DelegateCommand<T> FromAsyncHandler(Func<T, Task> executeMethod, IBusyIndicator busyIndicator)
        {
            return new DelegateCommand<T>(executeMethod)
            {
                BusyIndicator = busyIndicator
            };
        }

        /// <summary>
        ///     Factory method to create a new instance of <see cref="DelegateCommand{T}" /> from an awaitable handler method.
        /// </summary>
        /// <param name="executeMethod">
        ///     Delegate to execute when Execute is called on the command. This can be null to just hook up
        ///     a CanExecute delegate.
        /// </param>
        /// <param name="canExecuteMethod">Delegate to execute when CanExecute is called on the command. This can be null.</param>
        /// <returns>Constructed instance of <see cref="DelegateCommand{T}" /></returns>
        public static DelegateCommand<T> FromAsyncHandler(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod,
            IBusyIndicator busyIndicator)
        {
            return new DelegateCommand<T>(executeMethod, canExecuteMethod)
            {
                BusyIndicator = busyIndicator
            };
        }

        /// <summary>
        ///     Determines if the command can execute by invoked the <see cref="Func{T,Bool}" /> provided during construction.
        /// </summary>
        /// <param name="parameter">Data used by the command to determine if it can execute.</param>
        /// <returns>
        ///     <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.
        /// </returns>
        public virtual bool CanExecute(T parameter)
        {
            return base.CanExecute(parameter);
        }

        /// <summary>
        ///     Executes the command and invokes the <see cref="Action{T}" /> provided during construction.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        public virtual async Task Execute(T parameter)
        {
            if (BusyIndicator != null)
            {
                using (new BusyIndicatorScope(BusyIndicator))
                {
                    await base.Execute(parameter);
                }
            }
            else
            {
                await base.Execute(parameter);
            }
        }
    }

    /// <summary>
    ///     An <see cref="ICommand" /> whose delegates do not take any parameters for <see cref="Execute" /> and
    ///     <see cref="CanExecute" />.
    /// </summary>
    /// <see cref="DelegateCommandBase" />
    /// <see cref="DelegateCommand{T}" />
    public class DelegateCommand : DelegateCommandBase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="DelegateCommand" /> with the <see cref="Action" /> to invoke on execution.
        /// </summary>
        /// <param name="executeMethod">The <see cref="Action" /> to invoke when <see cref="ICommand.Execute" /> is called.</param>
        public DelegateCommand(Action executeMethod)
            : this(executeMethod, () => true)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="DelegateCommand" /> with the <see cref="Action" /> to invoke on execution
        ///     and a <see langword="Func" /> to query for determining if the command can execute.
        /// </summary>
        /// <param name="executeMethod">The <see cref="Action" /> to invoke when <see cref="ICommand.Execute" /> is called.</param>
        /// <param name="canExecuteMethod">
        ///     The <see cref="Func{TResult}" /> to invoke when <see cref="ICommand.CanExecute" /> is
        ///     called
        /// </param>
        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
            : base(o => executeMethod(), o => canExecuteMethod())
        {
            if (executeMethod == null || canExecuteMethod == null)
                throw new ArgumentNullException(nameof(executeMethod), "Delegates cannot be null.");
        }

        private DelegateCommand(Func<Task> executeMethod)
            : this(executeMethod, () => true)
        {
        }

        private DelegateCommand(Func<Task> executeMethod, Func<bool> canExecuteMethod)
            : base(o => executeMethod(), o => canExecuteMethod())
        {
            if (executeMethod == null || canExecuteMethod == null)
            {
                throw new ArgumentNullException(nameof(executeMethod), "Delegates cannot be null.");
            }
        }

        /// <summary>
        ///     Factory method to create a new instance of <see cref="DelegateCommand" /> from an awaitable handler method.
        /// </summary>
        /// <param name="executeMethod">Delegate to execute when Execute is called on the command.</param>
        /// <param name="busyIndicator">A busyIndicator indicator.</param>
        /// <returns>Constructed instance of <see cref="DelegateCommand" /></returns>
        public static DelegateCommand FromAsyncHandler(
            Func<Task> executeMethod, 
            IBusyIndicator busyIndicator)
        {
            return new DelegateCommand(executeMethod) { BusyIndicator = busyIndicator };
        }

        /// <summary>
        ///     Factory method to create a new instance of <see cref="DelegateCommand" /> from an awaitable handler method.
        /// </summary>
        /// <param name="executeMethod">
        ///     Delegate to execute when Execute is called on the command. This can be null to just hook up
        ///     a CanExecute delegate.
        /// </param>
        /// <param name="canExecuteMethod">Delegate to execute when CanExecute is called on the command. This can be null.</param>
        /// <returns>Constructed instance of <see cref="DelegateCommand" /></returns>
        /// <param name="busyIndicator">A busyIndicator indicator.</param>
        public static DelegateCommand FromAsyncHandler(
            Func<Task> executeMethod, 
            Func<bool> canExecuteMethod,
            IBusyIndicator busyIndicator)
        {
            return new DelegateCommand(executeMethod, canExecuteMethod) { BusyIndicator = busyIndicator };
        }

        /// <summary>
        ///     Executes the command.
        /// </summary>
        public virtual async Task Execute()
        {
            if (BusyIndicator != null)
            {
                using (new BusyIndicatorScope(BusyIndicator))
                {
                    await Execute(null);
                }
            }
            else
            {
                await Execute(null);
            }
        }

        /// <summary>
        ///     Determines if the command can be executed.
        /// </summary>
        /// <returns>Returns <see langword="true" /> if the command can execute, otherwise returns <see langword="false" />.</returns>
        public virtual bool CanExecute()
        {
            return CanExecute(null);
        }
    }

    /// <summary>
    ///     Handles management and dispatching of EventHandlers in a weak way.
    /// </summary>
    public static class WeakEventHandlerManager
    {
        private static readonly SynchronizationContext SyncContext = SynchronizationContext.Current;

        /// <summary>
        ///     Invokes the handlers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="handlers"></param>
        public static void CallWeakReferenceHandlers(object sender, List<WeakReference> handlers)
        {
            if (handlers != null)
            {
                // Take a snapshot of the handlers before we call out to them since the handlers
                // could cause the array to me modified while we are reading it.
                var callees = new EventHandler[handlers.Count];
                var count = 0;

                //Clean up handlers
                count = CleanupOldHandlers(handlers, callees, count);

                // Call the handlers that we snapshotted
                for (var i = 0; i < count; i++)
                {
                    CallHandler(sender, callees[i]);
                }
            }
        }

        private static void CallHandler(object sender, EventHandler eventHandler)
        {
            if (eventHandler != null)
            {
                if (SyncContext != null)
                {
                    SyncContext.Post(o => eventHandler(sender, EventArgs.Empty), null);
                }
                else
                {
                    eventHandler(sender, EventArgs.Empty);
                }
            }
        }

        private static int CleanupOldHandlers(List<WeakReference> handlers, EventHandler[] callees, int count)
        {
            for (var i = handlers.Count - 1; i >= 0; i--)
            {
                var reference = handlers[i];
                var handler = reference.Target as EventHandler;
                if (handler == null)
                {
                    // Clean up old handlers that have been collected
                    handlers.RemoveAt(i);
                }
                else
                {
                    callees[count] = handler;
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        ///     Adds a handler to the supplied list in a weak way.
        /// </summary>
        /// <param name="handlers">Existing handler list.  It will be created if null.</param>
        /// <param name="handler">Handler to add.</param>
        /// <param name="defaultListSize">Default list size.</param>
        public static void AddWeakReferenceHandler(ref List<WeakReference> handlers, EventHandler handler,
            int defaultListSize)
        {
            if (handlers == null)
            {
                handlers = (defaultListSize > 0 ? new List<WeakReference>(defaultListSize) : new List<WeakReference>());
            }

            handlers.Add(new WeakReference(handler));
        }

        /// <summary>
        ///     Removes an event handler from the reference list.
        /// </summary>
        /// <param name="handlers">Handler list to remove reference from.</param>
        /// <param name="handler">Handler to remove.</param>
        public static void RemoveWeakReferenceHandler(List<WeakReference> handlers, EventHandler handler)
        {
            if (handlers != null)
            {
                for (var i = handlers.Count - 1; i >= 0; i--)
                {
                    var reference = handlers[i];
                    var existingHandler = reference.Target as EventHandler;
                    if ((existingHandler == null) || (existingHandler == handler))
                    {
                        // Clean up old handlers that have been collected
                        // in addition to the handler that is to be removed.
                        handlers.RemoveAt(i);
                    }
                }
            }
        }
    }
}