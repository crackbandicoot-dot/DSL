using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSL.Evaluator.Expressions;
using DSL.Evaluator.Instructions;
using DSL.Evaluator.Instructions.Statements;
using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.LenguajeTypes.DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.Instructions.Statements.ConditionalStatements
{
    internal class IfStatement : Instruction
    {
        
        private readonly InstructionBlock instructionBlock;
        private readonly Expression condition;

        public IfStatement(Expression condition, InstructionBlock instructionBlock)
        {
            this.condition = condition;
            this.instructionBlock = instructionBlock;
            
        }

        public override void Execute()
        {
            IDSLType value = condition.Evaluate();
            if (value is Bool b)
            {
                if (b)
                {
                    instructionBlock.Execute();   
                }
            }
            else
            {
                throw new Exception($"Cannot convert {value} to bool");
            }
            
        }
    }
}
