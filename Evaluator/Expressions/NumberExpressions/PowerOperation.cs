using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.Expressions.NumberExpressions
{
    internal class PowerOperation : BinaryExpression
    {
        public PowerOperation(IExpression left, IExpression right) : base(left, right)
        {
        }

        protected override IDSLType Operate(IDSLType left, IDSLType right)
        {
            if (left is Number l && right is Number r)
            {
                return l ^ r;
            }
            throw new Exception("Cannot use the operation ^ betwen l and r");
        }
    }
}
