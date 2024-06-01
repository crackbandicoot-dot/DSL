namespace DSL.Instructions.Expressions
{
    internal abstract class UnaryExpression<TReturn> : Expression<TReturn>
    {
        private readonly Expression<TReturn> operand;

        protected UnaryExpression(Expression<TReturn> operand)
        {
            this.operand = operand;
        }
        protected abstract TReturn Operate(TReturn operand);
        public override TReturn Evaluate() => Operate(operand.Evaluate());   
    }

}
