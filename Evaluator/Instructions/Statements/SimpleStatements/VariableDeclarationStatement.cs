using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSL.Evaluator.Expressions;
using DSL.Evaluator.Instructions;

namespace DSL.Evaluator.Instructions.Statements.SimpleStatements
{
    internal class VariableDeclarationStatement<T> : Instruction
    {
        private readonly Dictionary<string, object> scopeVariables;
        private readonly string identifier;
        private readonly Expression<T> expValue;

        public VariableDeclarationStatement(Dictionary<string, object> scopeVariables, string identifier, Expression<T> expValue)
        {
            this.scopeVariables = scopeVariables;
            this.identifier = identifier;
            this.expValue = expValue;
        }

        public override void Execute()
        {
            scopeVariables.Add(identifier, expValue.Evaluate());
        }
    }
}
