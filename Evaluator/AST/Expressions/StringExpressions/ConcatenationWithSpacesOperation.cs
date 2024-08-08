namespace DSL.Evaluator.AST.Expressions.StringExpressions
{
    internal class ConcatenationWithSpacesOperation : BinaryExpression
    {
        public ConcatenationWithSpacesOperation(IExpression left, IExpression right) : base(left, right)
        {

        }
        protected override object Operate(object left, object right)
        {
            return left + " " + right;
        }
    }
}
