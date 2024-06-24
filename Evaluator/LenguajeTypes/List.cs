namespace DSL.Evaluator.LenguajeTypes
{
    internal class List : IDSLType
    {
        private List<IDSLType> list = new();

        public Number Count => list.Count;

        public bool IsReadOnly => false;

        public IDSLType this[int index] { get => list[index]; set => list[index] = value; }

        public void Add(IDSLType item)
        {
            list.Add(item);
        }
        public void Shuffle()
        {

        }
        public void Pop()
        {

        }
        public bool Equals(IDSLType? other)
        {
            if (other is List l)
            {
                if (l.Count != this.Count)
                {
                    return false;
                }
                for (int i = 0; i < l.Count; i++)
                {
                    if (l[i] != this[i]) return false;
                }
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return "[" + string.Join(',', list) + "]";
        }
    }
}
