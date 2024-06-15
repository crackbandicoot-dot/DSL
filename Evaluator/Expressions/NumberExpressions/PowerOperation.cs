using DSL.Evaluator.LenguajeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Expressions.NumberExpressions
{
    internal class PowerOperation : BinaryExpression
    {
        public PowerOperation(Expression left, Expression right) : base(left, right)
        {
        }

        protected override IDSLType Operate(IDSLType left, IDSLType right)
        {
            if (left is Number l && right is Number r)
            {
                return l ^ r;
            }
            throw new Exception("Cannot use the operation ^ betwen l and r");
        }
    }
}
