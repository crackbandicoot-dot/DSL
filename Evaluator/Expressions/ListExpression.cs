// Ignore Spelling: exps

using DSL.Evaluator.LenguajeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Expressions
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
