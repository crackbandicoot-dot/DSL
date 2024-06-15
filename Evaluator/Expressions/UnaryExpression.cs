using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.Expressions
{
    internal abstract class UnaryExpression: Expression
    {
        private readonly Expression operand;

        public UnaryExpression(Expression operand)
        {
            this.operand = operand;
        }

        public override  IDSLType Evaluate() => Operate(operand.Evaluate());
        protected abstract IDSLType Operate(IDSLType operand);
    }

}
