namespace DSL.Evaluator.AST.Expressions.TernaryExpressions
{
    internal class TernaryExpression : IExpression
    {
        private readonly IExpression condition;
        private readonly IExpression optionTrue;
        private readonly IExpression optionFalse;

        public TernaryExpression(IExpression condition, IExpression optionTrue, IExpression optionFalse)
        {
            this.condition = condition;
            this.optionTrue = optionTrue;
            this.optionFalse = optionFalse;
        }

        public object Evaluate()
        {
            if ((bool)condition.Evaluate())
            {
                return optionTrue.Evaluate();
            }
            else
            {
                return optionFalse.Evaluate();
            }
        }
    }
}
