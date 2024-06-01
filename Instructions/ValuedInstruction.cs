using System.Threading.Channels;

namespace DSL.Instructions
{
    internal abstract class ValuedInstruction<TOut> : Instruction
    {
       
        public abstract TOut Execute();
    }


}

