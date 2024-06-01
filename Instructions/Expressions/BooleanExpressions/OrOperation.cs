namespace DSL.Instructions.Expressions.BooleanExpressions
{
    internal class OrOperation : BinaryExpression<bool>
    {
        private readonly Expression<bool> left;
        private readonly Expression<bool> right;

        public OrOperation(Expression<bool> left, Expression<bool> right) : base(left, right)
        {
            this.left = left;
            this.right = right;
        }

        protected override bool Operate(bool left, bool right) => left || right;       
    }
}

