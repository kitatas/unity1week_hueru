namespace Utility
{
    public static class ArrayExtensions
    {
        public static int MaxIndex<T>(this T[] array)
        {
            return array.Length - 1;
        }

        public static bool IsIndexOutOfRange<T>(this T[] array, int index)
        {
            return array == null || index < 0 || index > array.MaxIndex();
        }

        public static bool IsIndexOutOfRangeOrNull<T>(this T[] array, int index)
        {
            return array.IsIndexOutOfRange(index) || array[index] == null;
        }

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