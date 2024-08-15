﻿namespace DSL.Evaluator.LenguajeTypes
{
    internal static class ListExtensions
    {
        public static T Pop<T>(this IList<T> list)
        {
            var item = list[^1];
            list.RemoveAt(list.Count - 1);
            return item;
        }

        public static object Push<T>(this IList<T> list, T item)
        {
            list.Add(item);
            return typeof(void);
        }
        public static IList<T> Find<T>(this IList<T> list, Delegate predicate)
        {
            IList<T> result = new List<T>();
            foreach (var item in list)
            {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
                if ((bool)(predicate.Invoke(item)))
                {
                    result.Add(item);
                }
#pragma warning restore CS8604 // Posible argumento de referencia nulo
            }
            return result;
        }
    }
}