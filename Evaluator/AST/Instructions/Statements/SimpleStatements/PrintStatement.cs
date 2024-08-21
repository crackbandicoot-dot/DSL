using DSL.Evaluator.AST.Expressions;
using System;

namespace DSL.Evaluator.AST.Instructions.Statements.SimpleStatements
{
    internal class PrintStatement : IInstruction
    {
        internal static Action<string> printerFunction;
        private readonly IExpression exp;

        public PrintStatement(IExpression exp)
        {
            this.exp = exp;
        }

        public void Execute()
        {
            printerFunction.Invoke(exp.Evaluate().ToString());
        }
    }
}
