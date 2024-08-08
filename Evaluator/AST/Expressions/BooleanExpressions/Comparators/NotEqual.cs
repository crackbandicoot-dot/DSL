namespace DSL.Evaluator.AST.Expressions.BooleanExpressions.Comparators
{
    internal class NotEqual : BinaryExpression
    {
        public NotEqual(IExpression left, IExpression right) : base(left, right)
        {
        }

        protected override object Operate(object left, object right)
        {
            object res = !left.Equals(right);
            return res;
        }
    }
}
