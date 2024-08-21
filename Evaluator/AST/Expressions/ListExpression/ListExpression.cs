// Ignore Spelling: exps
using System.Collections.Generic;
using System.Linq;

namespace DSL.Evaluator.AST.Expressions.ListExpression
{
    internal class ListExpression : IExpression
    {
        private readonly List<IExpression> exps;
        public ListExpression(List<IExpression> exps)
        {
            this.exps = exps;
        }
        public object Evaluate()
        => exps.Select(x => x.Evaluate()).ToList();
    }
}
