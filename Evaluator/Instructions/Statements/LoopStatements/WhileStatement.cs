using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSL.Evaluator.Expressions;
using DSL.Evaluator.Instructions;
using DSL.Evaluator.Instructions.Statements;
using DSL.Evaluator.LenguajeTypes.DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.Instructions.Statements.LoopStatements
{
    internal class WhileStatement : Instruction
    {
        private readonly Bool condition;
        private readonly InstructionBlock actionBlock;

        public WhileStatement(Bool condition, InstructionBlock actionBlock)
        {
            this.condition = condition;
            this.actionBlock = actionBlock;
        }

        public override void Execute()
        {
            while (condition)
            {
                actionBlock.Execute();
            }
        }
    }
}
