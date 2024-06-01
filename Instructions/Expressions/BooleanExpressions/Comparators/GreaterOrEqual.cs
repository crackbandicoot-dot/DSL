using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Instructions.Expressions.BooleanExpressions.Comparators
{
    internal class GreaterOrEqual<TCompare> : Comparator<TCompare, TCompare> where TCompare : IComparable<TCompare>  
    {
        public GreaterOrEqual(Expression<TCompare> left, Expression<TCompare> right) : base(left, right)
        {
        }

        public override bool Compare(TCompare left, TCompare right) => left.CompareTo(right) >= 0;
    }
}
