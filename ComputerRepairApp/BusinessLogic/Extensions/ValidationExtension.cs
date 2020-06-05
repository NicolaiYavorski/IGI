using System;

namespace BusinessLogic.Extensions
{
    public static class ValidationExtension
    {
        public static void CreateValidation<T>(this T item)
            where T : class
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Data for creation not found");
            }
        }

        public static void FindValidation<T>(this T item)
            where T : class
        {
            if (item == null)
            {
                throw new ArgumentException(nameof(item), "No data by this id");
            }
        }
    }
}
