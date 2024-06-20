using System;
using System.Collections.Generic;
using System.Linq;
using DSL.Evaluator.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Instructions.Statements.SimpleStatements
{
    internal class PrintStatement :IInstruction
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
