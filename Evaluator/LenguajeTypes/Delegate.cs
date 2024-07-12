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
        private readonly IExpression expression;

        public Delegate(string[] identifiers,IExpression expression) 
        {
            Identifiers = identifiers;
            this.expression = expression;
        }

        public IDSLType Invoke(params IDSLType[] parameters)
        {
            //TODO
            throw new NotImplementedException();
        }
        public string[] Identifiers { get; }

        public bool Equals(IDSLType? other)
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
