namespace DSL.Evaluator.AST.Expressions
{
    internal interface IExpression : IASTNode
    {
        public object Evaluate();
    }

}
