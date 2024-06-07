namespace DSL.Evaluator.Expressions
{
    internal abstract class UnaryExpression<TReturn> : Expression<TReturn>
    {
        private readonly Expression<TReturn> operand;

        public UnaryExpression(Expression<TReturn> operand)
        {
            this.operand = operand;
        }

        public override TReturn Evaluate() => Operate(operand.Evaluate());
        protected abstract TReturn Operate(TReturn operand);
    }

}
