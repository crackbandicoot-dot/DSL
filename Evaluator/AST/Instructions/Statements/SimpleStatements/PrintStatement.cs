using DSL.Evaluator.AST.Expressions;
using DSL.Evaluator.AST.Instructions;

namespace DSL.Evaluator.AST.Instructions.Statements.SimpleStatements
{
    internal class PrintStatement : IInstruction
    {
        private readonly IExpression exp;

        public PrintStatement(IExpression exp)
        {
            this.exp = exp;
        }

        public void Execute()
        {
            Console.WriteLine(exp.Evaluate());
        }
    }
}
