namespace DSL.Evaluator.AST.Expressions
{
    internal class SimpleExpression : IExpression
    {
        private readonly object value;
        public SimpleExpression(object value)
        {
            this.value = value;
        }
        public object Evaluate() => value;
    }

}
