using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSL.Evaluator.Expressions;
using DSL.Evaluator.Instructions;
using DSL.Evaluator.LenguajeTypes;
using DSL.Scope;

namespace DSL.Evaluator.Instructions.Statements.SimpleStatements
{
    internal class VariableDeclarationStatement : Instruction 
    {
        private readonly Scope<IDSLType> scope;
        private readonly string identifier;
        private readonly Expression exp;

        public VariableDeclarationStatement(Scope<IDSLType> scope, string identifier, Expression exp)
        {
            this.scope= scope;
            this.identifier = identifier;
            this.exp = exp;
        }

        public override void Execute()
        {
            scope.Declare(identifier, exp.Evaluate());
        }
    }
}
