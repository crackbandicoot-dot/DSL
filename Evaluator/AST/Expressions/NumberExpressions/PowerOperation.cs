using System;

namespace DSL.Evaluator.AST.Expressions.NumberExpressions
{
    internal class PowerOperation : BinaryExpression
    {
        public PowerOperation(IExpression left, IExpression right) : base(left, right)
        {
        }

        protected override object Operate(object left, object right)
        {
            return Math.Pow((double)left, (double)right);
        }
    }
}
