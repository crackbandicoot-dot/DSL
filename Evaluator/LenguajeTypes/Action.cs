using DSL.Evaluator.AST.Instructions.Statements;
using System;

namespace DSL.Evaluator.LenguajeTypes
{
    internal class Action
    {
        private readonly string[] parametersIdentifiers;
        public readonly InstructionBlock instructionBlock;

        public Action(string[] parametersIdentifiers, InstructionBlock instructionBlock)
        {
            this.parametersIdentifiers = parametersIdentifiers;
            this.instructionBlock = instructionBlock;
        }
        public void Invoke(params object[] parametersValues)
        {
            if (parametersIdentifiers.Length != parametersValues.Length)
            {
                throw new Exception($"This methods gets {parametersIdentifiers.Length} parameters");
            }
            else
            {
                for (int i = 0; i < parametersIdentifiers.Length; i++)
                {
                    instructionBlock.ScopeVariables.Declare(
                        parametersIdentifiers[i],
                        parametersValues[i]
                       );
                }
                instructionBlock.Execute();
            }
        }

    }
}
