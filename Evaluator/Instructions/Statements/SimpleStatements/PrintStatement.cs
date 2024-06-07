using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSL.Evaluator.Expressions;
using DSL.Evaluator.Instructions;

namespace DSL.Evaluator.Instructions.Statements.SimpleStatements
{
    internal class PrintStatement : Instruction
    {
        private readonly Expression<string> str;

        public PrintStatement(Expression<string> str)
        {
            this.str = str;
        }

        public override void Execute()
        {
            Console.WriteLine(str.Evaluate());
        }
    }
}
