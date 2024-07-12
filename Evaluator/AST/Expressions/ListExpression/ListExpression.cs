// Ignore Spelling: exps
using DSL.Evaluator.AST.Expressions;
using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.AST.Expressions.ListExpression
{
    internal class ListExpression : IExpression
    {
        private readonly List<IExpression> exps;
        public ListExpression(List<IExpression> exps)
        {
            this.exps = exps;
        }
        public IDSLType Evaluate()
        {
            List res = new();
            foreach (IExpression exp in exps)
            {
                res.Add(exp.Evaluate());
            }
            return res;
        }
    }
}
