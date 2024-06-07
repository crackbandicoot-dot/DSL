using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSL.Evaluator.Expressions;
using DSL.Evaluator.Instructions;
using DSL.Evaluator.Instructions.Statements;

namespace DSL.Evaluator.Instructions.Statements.LoopStatements
{
    internal class WhileStatement : Instruction
    {
        private readonly Expression<bool> stopCondition;
        private readonly InstructionBlock actionBlock;

        public WhileStatement(Expression<bool> stopCondition, InstructionBlock actionBlock)
        {
            this.stopCondition = stopCondition;
            this.actionBlock = actionBlock;
        }

        public override void Execute()
        {
            while (stopCondition.Evaluate())
            {
                actionBlock.Execute();
            }
        }
    }
}
