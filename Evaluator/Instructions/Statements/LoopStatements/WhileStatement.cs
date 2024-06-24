using DSL.Evaluator.Expressions;
using DSL.Evaluator.LenguajeTypes.DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.Instructions.Statements.LoopStatements
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


            if (condition.Evaluate() is Bool b)
            {
                while (b)
                {
                    actionBlock.Execute();
                    b = (Bool)(condition.Evaluate());
                }
            }
            else
            {
                throw new NotImplementedException("Expected boolean condition");
            }
        }
    }
}
