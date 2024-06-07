using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Expressions
{
    internal abstract class BinaryExpression<TReturn> : Expression<TReturn>
    {
        private readonly Expression<TReturn> left;
        private readonly Expression<TReturn> right;

        protected BinaryExpression(Expression<TReturn> left, Expression<TReturn> right)
        {
            this.left = left;
            this.right = right;
        }

        protected abstract TReturn Operate(TReturn left, TReturn right);
        public override TReturn Evaluate() => Operate(left.Evaluate(), right.Evaluate());
    }

}
