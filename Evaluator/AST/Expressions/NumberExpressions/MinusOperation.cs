namespace DSL.Evaluator.AST.Expressions.NumberExpressions
{
    internal class MinusOperation : BinaryExpression
    {
        public MinusOperation(IExpression left, IExpression right) : base(left, right)
        {
        }
        protected override object Operate(object left, object right)
        {
            return (double)left - (double)right;
        }
    }
}
