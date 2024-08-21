// Ignore Spelling: Gwent

using DSL.Evaluator.AST.Instructions.ObjectDeclaration;
using DSL.Extensor_Methods;
using System.Collections.Generic;

namespace DSL.Evaluator.AST.Instructions
{
    internal class GwentProgram : IInstruction
    {
        public Context Context { get; }
        private readonly IEnumerable<IInstruction> objectDeclarations;
        public GwentProgram(IEnumerable<IInstruction> objectDeclarations, Context context)
        {
            this.objectDeclarations = objectDeclarations;
            Context = context;
        }
        public void Execute()
        => objectDeclarations.ForEach(od => od.Execute());
    }
}
