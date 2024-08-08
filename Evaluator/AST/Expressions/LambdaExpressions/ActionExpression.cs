using DSL.Evaluator.AST.Instructions.Statements;

namespace DSL.Evaluator.AST.Expressions.LambdaExpressions
{
    internal class ActionExpression : IExpression
    {
        private readonly string[] parameters;
        private readonly InstructionBlock instructionBlock;

        public ActionExpression(string[] parameters, InstructionBlock instructionBlock)
        {
            this.parameters = parameters;
            this.instructionBlock = instructionBlock;
        }
        public object Evaluate()
        {
            return new LenguajeTypes.Action(parameters, instructionBlock);
        }
    }
}
