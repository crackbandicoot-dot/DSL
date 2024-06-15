using System;
using System.Collections.Generic;
using System.Linq;
using DSL.Evaluator.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Instructions.Statements.SimpleStatements
{
    internal class PrintStatement :Instruction
    {
        private readonly Expression exp;

        public PrintStatement(Expression exp)
        {
            this.exp = exp;
        }

        public override void Execute()
        {
            Console.WriteLine(exp.Evaluate());
        }
    }
}
