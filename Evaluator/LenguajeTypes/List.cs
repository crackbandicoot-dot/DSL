using System.Collections;

namespace DSL.Evaluator.LenguajeTypes
{
    public class List : IDSLType,IList<IDSLType>
    {
        private List<IDSLType> list = new();
        public Number Count => list.Count;
        public bool IsReadOnly => false;

        int ICollection<IDSLType>.Count => ((ICollection<IDSLType>)list).Count;

        public IDSLType this[int index] { get => ((IList<IDSLType>)list)[index]; set => ((IList<IDSLType>)list)[index] = value; }

        public IDSLType Get(Number index)
        {
            return list[index.ToInteger()];
        }
        public void Set(Number index,IDSLType value)
        {
            list[index.ToInteger()] = value;
        }
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
                    if (l.Get(i) != Get(i)) return false;
                }
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return "[" + string.Join(',', list) + "]";
        }

        public int IndexOf(IDSLType item)
        {
            return ((IList<IDSLType>)list).IndexOf(item);
        }

        public void Insert(int index, IDSLType item)
        {
            ((IList<IDSLType>)list).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<IDSLType>)list).RemoveAt(index);
        }

        public void Clear()
        {
            ((ICollection<IDSLType>)list).Clear();
        }

        public bool Contains(IDSLType item)
        {
            return ((ICollection<IDSLType>)list).Contains(item);
        }

        public void CopyTo(IDSLType[] array, int arrayIndex)
        {
            ((ICollection<IDSLType>)list).CopyTo(array, arrayIndex);
        }

        public bool Remove(IDSLType item)
        {
            return ((ICollection<IDSLType>)list).Remove(item);
        }

        public IEnumerator<IDSLType> GetEnumerator()
        {
            return ((IEnumerable<IDSLType>)list).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)list).GetEnumerator();
        }
    }
}
