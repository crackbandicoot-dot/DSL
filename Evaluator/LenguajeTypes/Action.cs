using DSL.Evaluator.AST.Instructions.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.LenguajeTypes
{
    internal class Action : IDSLType
    {
        private readonly string[] parametersIdentifiers;
        private readonly InstructionBlock instructionBlock;

        public Action(string[] parametersIdentifiers, InstructionBlock instructionBlock)
        {
            this.parametersIdentifiers = parametersIdentifiers;
            this.instructionBlock = instructionBlock;
        }

        public bool Equals(IDSLType? other)
        {
            //TODO
            throw new NotImplementedException();
        }

        public void Invoke(params IDSLType[] parametersValues)
        {
            if (parametersIdentifiers.Length!=parametersValues.Length)
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
                    instructionBlock.Execute();
                }
            }
        }

    }
}
