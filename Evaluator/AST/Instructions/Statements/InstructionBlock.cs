using DSL.Extensor_Methods;

namespace DSL.Evaluator.AST.Instructions.Statements
{
    internal class InstructionBlock : IInstruction
    {
        private readonly IEnumerable<IInstruction> instructions;
        public Scope.Scope ScopeVariables { get; }
        public InstructionBlock(IEnumerable<IInstruction> instructions,
            Scope.Scope scopeVariables)
        {
            this.instructions = instructions; ScopeVariables = scopeVariables;
        }
        public void Execute()
        => instructions.ForEach(inst => inst.Execute());
    }
}
