using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HelperFunction.Utilities
{
    public static class ByteManipulation
    {
        /// <summary>
        /// Convert Object to byte array.
        /// </summary>
        /// <param name="obj"> object data. </param>
        /// <typeparam name="T"> object type. </typeparam>
        /// <returns> byte array of given object. </returns>
        public static byte[] ToByteArray<T>(T obj)
        {
            if(obj == null) return null;
            var bf = new BinaryFormatter();
            using var ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        /// <summary>
        /// Convert byte array into Object.
        /// </summary>
        /// <param name="data"> byte array. </param>
        /// <typeparam name="T"> the type need to convert that byte. </typeparam>
        /// <returns> convert byte into specific object type. </returns>
        public static T FromByteArray<T>(byte[] data)
        {
            if(data == null) return default;
            var bf = new BinaryFormatter();
            using var ms = new MemoryStream(data);
            var obj = bf.Deserialize(ms);
            return (T)obj;
        }
    }
}