using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace EmployeeManager.Framework
{
    public static class DeepCloneExtension
    {
        /// <summary>
        ///     Perform a deep clone of the object via a <see cref="MemoryStream" /> copy.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(this T source)
        {
            if (!typeof (T).IsSerializable)
            {
                throw new ArgumentException("The type must be marked as serializable.", nameof(source));
            }

            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            var stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T) formatter.Deserialize(stream);
            }
        }
    }
}