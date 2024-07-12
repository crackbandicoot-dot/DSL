using DSL.Evaluator.AST.Expressions;
using DSL.Evaluator.AST.Instructions;
using DSL.Evaluator.AST.Instructions.Statements;
using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.LenguajeTypes.DSL.Evaluator.LenguajeTypes;

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
            IDSLType value = condition.Evaluate();
            if (value is Bool b)
            {
                if (b)
                {
                    instructionBlock.Execute();
                }
            }
            else
            {
                throw new Exception($"Cannot convert {value} to bool");
            }

        }
    }
}
