using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.Expressions.NumberExpressions
{
    internal class OppositeOperator : UnaryExpression
    {
        public OppositeOperator(IExpression operand) : base(operand)
        {
        }

        protected override IDSLType Operate(IDSLType operand)
        {
            if (operand is Number o)
            {
                return -o;
            }
            throw new ArgumentException("Cannot apply the operator - with that type");
        }
    }
}
