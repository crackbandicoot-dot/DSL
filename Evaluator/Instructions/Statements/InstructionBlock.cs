using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSL.Evaluator.Instructions;

namespace DSL.Evaluator.Instructions.Statements
{
    internal class InstructionBlock : Instruction
    {
        private readonly List<Instruction> instructions;
        private Dictionary<string, object> scopeVariables;
        public InstructionBlock(List<Instruction> instructions, Dictionary<string, object> scopeVariables)
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
