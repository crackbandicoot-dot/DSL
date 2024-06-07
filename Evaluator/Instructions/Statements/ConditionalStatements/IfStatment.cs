using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSL.Evaluator.Expressions;
using DSL.Evaluator.Instructions;
using DSL.Evaluator.Instructions.Statements;

namespace DSL.Evaluator.Instructions.Statements.ConditionalStatements
{
    internal class IfStatment : Instruction
    {
        private readonly Expression<bool> condition;
        private readonly InstructionBlock instructionBlock;

        public IfStatment(Expression<bool> condition, InstructionBlock instructionBlock)
        {
            this.condition = condition;
            this.instructionBlock = instructionBlock;
        }

        public override void Execute()
        {
            if (condition.Evaluate())
            {
                instructionBlock.Execute();
            }
        }
    }
}
