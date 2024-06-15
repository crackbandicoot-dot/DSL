using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.LenguajeTypes.DSL.Evaluator.LenguajeTypes;
namespace DSL.Evaluator.Expressions.NumberExpressions
{
    internal class PlusOperation : BinaryExpression
    {
        public PlusOperation(Expression left, Expression right) : base(left, right)
        { }
     

        protected override IDSLType Operate(IDSLType left, IDSLType right)
        {
            if (left is Number l && right is Number r)
            {
                return l + r;
            }
            throw new ArgumentException("Cannot apply the operator + betwen left and rigth");
        }
    }
}
