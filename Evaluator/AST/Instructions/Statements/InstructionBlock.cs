using DSL.Evaluator.AST.Instructions;
using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.Scope;

namespace DSL.Evaluator.AST.Instructions.Statements
{
    internal class InstructionBlock : IInstruction
    {
        private readonly List<IInstruction> instructions;
        public Scope<IDSLType> ScopeVariables { get; }
        public InstructionBlock(List<IInstruction> instructions, Scope<IDSLType> scopeVariables)
        {
            this.instructions = instructions;
            ScopeVariables = scopeVariables;
        }
        public void Execute()
        {
            foreach (var instruction in instructions)
            {
                instruction.Execute();
            }
        }
    }
}
