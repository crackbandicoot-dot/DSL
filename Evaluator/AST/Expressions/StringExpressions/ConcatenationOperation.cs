namespace DSL.Evaluator.AST.Expressions.StringExpressions
{
    internal class ConcatenationOperation : BinaryExpression
    {
        public ConcatenationOperation(IExpression left, IExpression right) : base(left, right)
        {
        }

        protected override object Operate(object left, object right)
        {
            return left.ToString() + right.ToString();
        }
    }
}
