namespace DSL.Evaluator.LenguajeTypes
{
    internal static class ListExtensions
    {
        public static T Pop<T>(this List<T> list)
        {
            var item = list[^1];
            list.RemoveAt(list.Count - 1);
            return item;
        }

        public static object Push<T>(this List<T> list, T item)
        {
            list.Add(item);
            return typeof(void);
        }
        public static List<T> Find<T>(this List<T> list, Delegate predicate)
        {
            List<T> result = new();
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
