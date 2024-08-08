namespace DSL.Evaluator.AST.Expressions.NumberExpressions
{
    internal class OppositeOperator : UnaryExpression
    {
        public OppositeOperator(IExpression operand) : base(operand)
        {
        }

        protected override object Operate(object operand)
        {
            return -(double)(operand);
        }
    }
}
