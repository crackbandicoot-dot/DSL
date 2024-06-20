using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return "[" + string.Join(',', list) + "]";
        }
    }
}
