using DSL.Evaluator.LenguajeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.AST.Expressions
{
    internal class DelegateExpression : IExpression
    {
        public DelegateExpression(string[] identifiers, IExpression exp)
        {
            Identifiers = identifiers;
            Exp = exp;
        }

        public string[] Identifiers { get; }
        public IExpression Exp { get; }

        public IDSLType Evaluate()
        {
            return new LenguajeTypes.Delegate(Identifiers, Exp);
        }
    }
}
