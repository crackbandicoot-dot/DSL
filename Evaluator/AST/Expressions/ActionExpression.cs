using DSL.Evaluator.AST.Instructions.Statements;
using DSL.Evaluator.LenguajeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.AST.Expressions
{
    internal class ActionExpression : IExpression
    {
        private readonly string[] parametersNames;
        private readonly InstructionBlock instructionBlock;

        public ActionExpression(string[] parametersNames, InstructionBlock instructionBlock)
        {
            this.parametersNames = parametersNames;
            this.instructionBlock = instructionBlock;
        }
        public IDSLType Evaluate()
        {
            return new LenguajeTypes.Action(parametersNames, instructionBlock);
        }
    }
}
