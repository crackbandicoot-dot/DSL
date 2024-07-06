using DSL.Evaluator.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.LenguajeTypes
{
    internal class Delegate : IDSLType
    {
        private readonly string[] identifiers;
        private readonly IExpression expression;
        public Delegate(string[] identifiers,IExpression expression)
        {
            this.identifiers = identifiers;
            this.expression = expression;
        }
        public bool Equals(IDSLType? other)
        {
            throw new NotImplementedException();
        }
    }
}
