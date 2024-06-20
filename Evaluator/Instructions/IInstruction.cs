using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Instructions
{
    internal interface IInstruction
    {
        public abstract void Execute();
    }
}

