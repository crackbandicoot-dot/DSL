using DSL.Evaluator.Expressions;

namespace DSL.Evaluator.Instructions.Statements.SimpleStatements
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
