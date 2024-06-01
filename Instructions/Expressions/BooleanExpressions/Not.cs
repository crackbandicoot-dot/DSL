using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Instructions.Expressions.BooleanExpressions
{
    internal class Not : UnaryExpression<bool>
    {
        public Not(Expression<bool> operand) : base(operand)
        {
        }

        protected override bool Operate(bool operand) => !operand;
    }
}
