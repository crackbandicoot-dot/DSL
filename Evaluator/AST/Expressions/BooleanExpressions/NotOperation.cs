namespace DSL.Evaluator.AST.Expressions.BooleanExpressions
{
    internal class NotOperation : UnaryExpression
    {
        public NotOperation(IExpression operand) : base(operand)
        {
        }

        protected override object Operate(object operand)
        {
            return !(bool)operand;
        }
    }
}
