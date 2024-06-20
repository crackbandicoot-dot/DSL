using DSL.Evaluator.LenguajeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Expressions.NumberExpressions
{
    internal class MinusOperation : BinaryExpression
    {
        public MinusOperation(IExpression left, IExpression right) : base(left, right)
        {
        }
        protected override IDSLType Operate(IDSLType left, IDSLType right)
        {
            if (left is Number l && right is Number r)
            {
                return l - r;
            }
            throw new ArgumentException("Cannot apply the operator - betwen left and rigth");
        }
    }
}
