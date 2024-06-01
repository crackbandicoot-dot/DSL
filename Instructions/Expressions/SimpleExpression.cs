namespace DSL.Instructions.Expressions
{
    internal class SimpleExpression<TReturn> : Expression<TReturn>
    {
        private readonly TReturn value;
        public SimpleExpression(TReturn value)
        {
            this.value = value;
        }
        public override TReturn Evaluate() => value;
    }

}
