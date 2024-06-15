using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.LenguajeTypes.DSL.Evaluator.LenguajeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Expressions.BooleanExpressions.Comparators
{
    internal class NotEqual : BinaryExpression
    {
        public NotEqual(Expression left, Expression right) : base(left, right)
        {
        }

        protected override IDSLType Operate(IDSLType left, IDSLType right)
        {
            Bool res = !left.Equals(right);
            return res; 
        }
    }
}
