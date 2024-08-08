namespace DSL.Evaluator.AST.Expressions.BooleanExpressions
{
    internal class OrOperation : BinaryExpression
    {
        public OrOperation(IExpression left, IExpression right) : base(left, right)
        {
        }

        protected override object Operate(object left, object right)
        {
            return (bool)left || (bool)right;
        }
    }
}

