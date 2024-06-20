using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSL.Evaluator.Instructions;
using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.Scope;

namespace DSL.Evaluator.Instructions.Statements
{
    internal class InstructionBlock : IInstruction
    {
        private readonly List<IInstruction> instructions;
        public Scope<IDSLType> ScopeVariables { get; }
        public InstructionBlock(List<IInstruction> instructions, Scope<IDSLType> scopeVariables)
        {
            this.instructions = instructions;
            this.ScopeVariables = scopeVariables;
        }
        public void Execute()
        {
            foreach (var instruction in instructions)
            {
                instruction.Execute();
            }
        }
    }
}
