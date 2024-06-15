using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSL.Evaluator.Instructions;
using DSL.Evaluator.LenguajeTypes;
using DSL.Scope;

namespace DSL.Evaluator.Instructions.Statements
{
    internal class InstructionBlock : Instruction
    {
        private readonly List<Instruction> instructions;
        private Scope<IDSLType> scopeVariables;
        public InstructionBlock(List<Instruction> instructions, Scope<IDSLType> scopeVariables)
        {
            this.instructions = instructions;
            this.scopeVariables = scopeVariables;
        }
        public override void Execute()
        {
            foreach (var instruction in instructions)
            {
                instruction.Execute();
            }
        }
    }
}
