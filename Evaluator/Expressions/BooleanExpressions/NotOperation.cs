using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.LenguajeTypes.DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.Expressions.BooleanExpressions
{
    internal class NotOperation : UnaryExpression
    {
        public NotOperation(IExpression operand) : base(operand)
        {
        }

        protected override IDSLType Operate(IDSLType operand)
        {
            if (operand is Bool o)
            {
                return !o;
            }
            throw new ArgumentException("Cannot apply the operator ! with the operand");
        }
    }
}
