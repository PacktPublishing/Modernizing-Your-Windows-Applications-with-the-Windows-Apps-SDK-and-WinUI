using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EmployeeManager.Framework
{
    /// <summary>
    ///     ValidationExtensions to the <see cref="ObservableCollection{T}" /> class.
    /// </summary>
    public static class ObservableCollectionExtensions
    {
        /// <summary>
        ///     Adds the provided range of elements to the ObservableCollection.
        ///     The add operation is performed per element.
        /// </summary>
        /// <typeparam name="T">The type of the collection's items.</typeparam>
        /// <param name="collection">The observable collection to add the elements to.</param>
        /// <param name="range">The enumerable of elements to add.</param>
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> range)
        {
            foreach (var element in range)
            {
                collection.Add(element);
            }
        }

        /// <summary>
        ///     Replaces the contents of the observable collection with the provided elements.
        /// </summary>
        /// <typeparam name="T">The type of the collection's items.</typeparam>
        /// <param name="collection">The observable collection which elements should be replaced.</param>
        /// <param name="range">The elements that are newly inserted into the collection.</param>
        public static void Replace<T>(this ObservableCollection<T> collection, IEnumerable<T> range)
        {
            collection.Clear();
            collection.AddRange(range);
        }
    }
}