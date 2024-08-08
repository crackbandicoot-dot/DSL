namespace DSL.Evaluator.AST.Expressions.NumberExpressions
{
    internal class ModuloOperation : BinaryExpression
    {
        public ModuloOperation(IExpression left, IExpression right) : base(left, right)
        {
        }

        protected override object Operate(object left, object right)
        {
            double l = (double)left;
            double r = (double)right;
            if (double.IsInteger(l) && double.IsInteger(r))
            {
                return (int)l % (int)r;
            }
            else
            {
                return l % r;
            }
        }
    }
}
