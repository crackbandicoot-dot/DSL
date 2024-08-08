using DSL.Evaluator.AST.Expressions;
namespace DSL.Evaluator.AST.Instructions.Statements.LoopStatements
{
    internal class WhileStatement : IInstruction
    {
        private readonly IExpression condition;
        private readonly InstructionBlock actionBlock;

        public WhileStatement(IExpression condition, InstructionBlock actionBlock)
        {
            this.condition = condition;
            this.actionBlock = actionBlock;
        }

        public void Execute()
        {
            while ((bool)(condition).Evaluate())
            {
                actionBlock.Execute();
            }
        }
    }
}
