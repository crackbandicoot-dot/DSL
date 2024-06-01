using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Instructions.Expressions
{
    internal abstract class Expression<TReturn> : ValuedInstruction<TReturn>
    {
        public abstract TReturn Evaluate();
        public override TReturn Execute() => Evaluate();
    }

}
