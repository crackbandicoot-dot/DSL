using DSL.Evaluator.Instructions.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.LenguajeTypes
{
    internal class Action : IDSLType
    {
        private readonly string[] parametersNames;
        private readonly InstructionBlock instructionBlock;

        public Action(string[] parametersNames,InstructionBlock instructionBlock){
            this.parametersNames = parametersNames;
            this.instructionBlock = instructionBlock;
        }
        public bool Equals(IDSLType? other)
        {
            throw new NotImplementedException();
        }
    }
}
