﻿using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.LenguajeTypes.DSL.Evaluator.LenguajeTypes;


namespace DSL.Evaluator.Expressions.BooleanExpressions.Comparators
{
    internal class Greater : BinaryExpression
    {
        public Greater(IExpression left, IExpression right) : base(left, right)
        {
        }
        protected override IDSLType Operate(IDSLType left, IDSLType right)
        {
            if (left is Number l && right is Number r)
            {
                Bool res = l > r;
                return res;
            }
            throw new Exception("Cannot apply operator < betwen l and r");
        }
    }
}
