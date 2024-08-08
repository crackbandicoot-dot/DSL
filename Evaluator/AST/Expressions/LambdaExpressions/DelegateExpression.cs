namespace DSL.Evaluator.AST.Expressions.LambdaExpressions
{
    internal class DelegateExpression : IExpression
    {
        private readonly string[] parameters;
        private readonly Scope.Scope scope;

        public IExpression Exp { get; }
        public DelegateExpression(string[] parameters, IExpression exp, Scope.Scope scope)
        {
            this.parameters = parameters;
            Exp = exp;
            this.scope = scope;
        }
        public object Evaluate()
        {
            return new LenguajeTypes.Delegate(parameters, Exp, scope);
        }
    }
}
