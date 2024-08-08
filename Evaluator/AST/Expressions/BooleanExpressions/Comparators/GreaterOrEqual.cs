namespace DSL.Evaluator.AST.Expressions.BooleanExpressions.Comparators
{
    internal class GreaterOrEqual : BinaryExpression
    {
        public GreaterOrEqual(IExpression left, IExpression right) : base(left, right)
        {
        }
        protected override object Operate(object left, object right)
        {
            return (double)left >= (double)right;
        }
    }
}
