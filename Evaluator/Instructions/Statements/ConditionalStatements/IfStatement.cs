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
    internal class IfStatement : IInstruction
    {
        
        private readonly InstructionBlock instructionBlock;
        private readonly IExpression condition;

        public IfStatement(IExpression condition, InstructionBlock instructionBlock)
        {
            this.condition = condition;
            this.instructionBlock = instructionBlock;
            
        }

        public void Execute()
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
