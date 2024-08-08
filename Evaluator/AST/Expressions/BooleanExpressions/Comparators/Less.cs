namespace DSL.Evaluator.AST.Expressions.BooleanExpressions.Comparators
{
    internal class Less : BinaryExpression
    {
        public Less(IExpression left, IExpression right) : base(left, right)
        {
        }
        protected override object Operate(object left, object right)
        {
            return (double)left < (double)right;
        }
    }
}
