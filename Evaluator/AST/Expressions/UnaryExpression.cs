namespace DSL.Evaluator.AST.Expressions
{
    internal abstract class UnaryExpression : IExpression
    {
        private readonly IExpression operand;

        public UnaryExpression(IExpression operand)
        {
            this.operand = operand;
        }
        public object Evaluate() => Operate(operand.Evaluate());
        protected abstract object Operate(object operand);
    }

}
