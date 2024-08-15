using DSL.Extensor_Methods;

namespace DSL.Evaluator.AST.Expressions.NumberExpressions
{
    internal class DivideOperation : BinaryExpression
    {
        public DivideOperation(IExpression left, IExpression right) : base(left, right)
        {
        }

        protected override object Operate(object left, object right)
        {
            double l = (double)left;
            double r = (double)right;
            if (l.IsInteger() && r.IsInteger())
            {
                return (int)l / (int)r;
            }
            else
            {
                return l / r;
            }
        }
    }
}
