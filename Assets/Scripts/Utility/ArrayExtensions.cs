namespace Utility
{
    /// <summary>
    /// 配列の拡張メソッドを管理するうクラス
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// 最大要素数
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int MaxIndex<T>(this T[] array)
        {
            return array.Length - 1;
        }

        /// <summary>
        /// 範囲外にアクセス
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsIndexOutOfRange<T>(this T[] array, int index)
        {
            return array == null || index < 0 || index > array.MaxIndex();
        }

        /// <summary>
        /// 範囲外 or NULL要素にアクセス
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsIndexOutOfRangeOrNull<T>(this T[] array, int index)
        {
            return array.IsIndexOutOfRange(index) || array[index] == null;
        }

        /// <summary>
        /// 配列版TryGetValue
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryGetValue<T>(this T[] array, int index, out T value)
        {
            if (array.IsIndexOutOfRangeOrNull(index))
            {
                value = default;
                return false;
            }

            value = array[index];
            return true;
        }
    }
}