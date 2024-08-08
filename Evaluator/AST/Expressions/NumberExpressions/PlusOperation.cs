namespace DSL.Evaluator.AST.Expressions.NumberExpressions
{
    internal class PlusOperation : BinaryExpression
    {
        public PlusOperation(IExpression left, IExpression right) : base(left, right)
        { }
        protected override object Operate(object left, object right)
        {
            return (double)(left) + (double)(right);
        }
    }
}

