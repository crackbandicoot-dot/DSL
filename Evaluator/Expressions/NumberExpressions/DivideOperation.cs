using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.Expressions.NumberExpressions
{
    internal class DivideOperation : BinaryExpression
    {
        public DivideOperation(IExpression left, IExpression right) : base(left, right)
        {
        }

        protected override IDSLType Operate(IDSLType left, IDSLType right)
        {
            if (left is Number l && right is Number r)
            {
                return l / r;
            }
            throw new ArgumentException("Cannot apply the operator / betwen left and rigth");
        }
    }
}
