using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.Expressions
{
    internal abstract class UnaryExpression: IExpression
    {
        private readonly IExpression operand;

        public UnaryExpression(IExpression operand)
        {
            this.operand = operand;
        }

        public  IDSLType Evaluate() => Operate(operand.Evaluate());
        protected abstract IDSLType Operate(IDSLType operand);
    }

}
