using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSL.Evaluator.Expressions;
using DSL.Evaluator.LenguajeTypes.DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.Expressions.BooleanExpressions.Comparators
{
    internal class Equal : BinaryExpression
    {
        public Equal(Expression left, Expression right) : base(left, right)
        {
        }
        protected override IDSLType Operate(IDSLType left, IDSLType right)
        {
            Bool res = left.Equals(right);
            return res;
        }
    }
}
