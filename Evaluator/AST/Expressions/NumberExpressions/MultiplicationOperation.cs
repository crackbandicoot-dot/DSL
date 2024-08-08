namespace DSL.Evaluator.AST.Expressions.NumberExpressions
{
    internal class MultiplicationOperation : BinaryExpression
    {
        public MultiplicationOperation(IExpression left, IExpression right) : base(left, right)
        {
        }

        protected override object Operate(object left, object right)
        {
            return (double)left * (double)right;
        }
    }
}
