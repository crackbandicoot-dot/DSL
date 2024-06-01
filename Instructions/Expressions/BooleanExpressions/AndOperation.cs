namespace DSL.Instructions.Expressions.BooleanExpressions
{
    internal class AndOperation : BinaryExpression<bool>
    {
        public AndOperation(Expression<bool> left, Expression<bool> right) : base(left, right)
        {
        }

        protected override bool Operate(bool left, bool right) => left && right;
    }
}

