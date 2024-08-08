// Ignore Spelling: DSL

namespace DSL.Evaluator.AST.Expressions
{
    internal abstract class BinaryExpression : IExpression
    {
        private readonly IExpression left;
        private readonly IExpression right;

        protected BinaryExpression(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

        protected abstract object Operate(object left, object right);
        public object Evaluate() => Operate(left.Evaluate(), right.Evaluate());

    }

}
