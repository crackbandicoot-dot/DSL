using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSL.Evaluator.Expressions;
using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.LenguajeTypes.DSL.Evaluator.LenguajeTypes;
namespace DSL.Evaluator.Expressions.BooleanExpressions.Comparators
{
    internal class LessOrEqual : BinaryExpression
    {
        public LessOrEqual(IExpression left, IExpression right) : base(left, right)
        {
        }
        protected override IDSLType Operate(IDSLType left, IDSLType right)
        {
            if (left is Number l && right is Number r)
            {
                Bool res = l <= r;
                return res;
            }
            throw new Exception("Cannot compare l and r");
        }
    }
}
