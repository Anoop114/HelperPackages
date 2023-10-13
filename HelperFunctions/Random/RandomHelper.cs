using System;

namespace HelperFunction.Random
{
    
    public static class RandomHelper
    {
        /// <summary>
        /// Give random enum value.
        /// </summary>
        /// <param name="isFirstExclude"> not include first element like => (None). </param>
        /// <typeparam name="T"> The enum need to pass. </typeparam>
        /// <returns> Random enum value form given enum. </returns>
        public static T RandomEnumValue<T>(bool isFirstExclude = false)
        {
            var startRange = isFirstExclude ? 1 : 0;
            var values = Enum.GetValues(typeof(T));
            var random = UnityEngine.Random.Range(startRange, values.Length);
            return (T)values.GetValue(random);
        }
    }
}

