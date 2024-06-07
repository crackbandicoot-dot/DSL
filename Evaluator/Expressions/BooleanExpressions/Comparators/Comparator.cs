using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSL.Evaluator.Expressions;

namespace DSL.Evaluator.Expressions.BooleanExpressions.Comparators
{
    internal abstract class Comparator<TLeft, TRigth> : Expression<bool>
    {
        private readonly Expression<TLeft> left;
        private readonly Expression<TRigth> right;

        public Comparator(Expression<TLeft> left, Expression<TRigth> right)
        {
            this.left = left;
            this.right = right;
        }
        public override bool Evaluate() => Compare(left.Evaluate(), right.Evaluate());
        public abstract bool Compare(TLeft left, TRigth right);
    }
}
