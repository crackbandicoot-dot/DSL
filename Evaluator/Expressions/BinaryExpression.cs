// Ignore Spelling: DSL

using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.Expressions
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

        protected abstract IDSLType Operate(IDSLType left, IDSLType right);
        public IDSLType Evaluate() => Operate(left.Evaluate(), right.Evaluate());
    }

}
