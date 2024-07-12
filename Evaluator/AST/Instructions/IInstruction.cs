namespace DSL.Evaluator.AST.Instructions
{
    internal interface IInstruction : IASTNode
    {
        public void Execute();
    }
}

