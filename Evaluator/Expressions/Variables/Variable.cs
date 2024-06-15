using DSL.Evaluator.LenguajeTypes;
using DSL.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Expressions.Variables
{
    internal class Variable : Expression
    {
        private readonly Scope<IDSLType> scope;
        private readonly string id;

        public Variable(string id,Scope<IDSLType> scope) 
        {
            this.id= id;
            this.scope = scope;
        }

        

        public override IDSLType Evaluate()
        {
            return scope.GetFromHierarchy(id);
        }
    }
}
