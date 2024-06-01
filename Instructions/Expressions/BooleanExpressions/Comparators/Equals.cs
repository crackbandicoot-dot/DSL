using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Instructions.Expressions.BooleanExpressions.Comparators
{
    internal class Equals<TLeft, TRigth> : Comparator<TLeft, TRigth>
    {
        public Equals(Expression<TLeft> left, Expression<TRigth> right) : base(left, right)
        {
        }
        public override bool Compare(TLeft left, TRigth right) => left!.Equals(right);
    }
}
