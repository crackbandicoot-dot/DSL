using DSL.Evaluator.LenguajeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Expressions
{
    internal abstract class BinaryExpression : Expression
    {
        private readonly Expression left;
        private readonly Expression right;

        protected BinaryExpression(Expression left, Expression right)
        {
            this.left = left;
            this.right = right;
        }

        protected abstract IDSLType Operate(IDSLType left, IDSLType right);
        public override IDSLType Evaluate() => Operate(left.Evaluate(), right.Evaluate());
    }

}
