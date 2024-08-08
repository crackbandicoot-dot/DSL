using DSL.Evaluator.AST.Expressions;
namespace DSL.Evaluator.AST.Instructions.Statements.ConditionalStatements
{
    internal class IfStatement : IInstruction
    {

        private readonly InstructionBlock instructionBlock;
        private readonly IExpression condition;

        public IfStatement(IExpression condition, InstructionBlock instructionBlock)
        {
            this.condition = condition;
            this.instructionBlock = instructionBlock;

        }
        public void Execute()
        {
            if ((bool)condition.Evaluate())
            {
                instructionBlock.Execute();
            }
        }
    }
}
