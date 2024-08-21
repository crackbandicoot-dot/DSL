using DSL.Evaluator.AST.Expressions;
using DSL.Evaluator.AST.Expressions.Scope;
using System.Collections.Generic;
using System.Linq;
namespace DSL.Evaluator.AST.Instructions.Statements.LoopStatements
{
    internal class ForStatement : IInstruction
    {
        private readonly string variableIdentifier;
        private readonly IExpression list;
        private readonly InstructionBlock instructions;
    
        public ForStatement(string variableIdentifier, IExpression list, InstructionBlock instructions, Scope scope)
        {
            this.variableIdentifier = variableIdentifier;
            this.list = list;
            this.instructions = instructions;
          
        }
        public void Execute()
        {
            List<object> list = ((IEnumerable<object>)this.list.Evaluate()).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                instructions.ScopeVariables.Declare(variableIdentifier, list[i]);
                instructions.Execute();
            }
        }
    }
}
