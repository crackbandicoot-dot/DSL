using DSL.Evaluator.AST.Expressions;
using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.LenguajeTypes.DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.AST.Expressions.BooleanExpressions.Comparators
{
    internal class NotEqual : BinaryExpression
    {
        public NotEqual(IExpression left, IExpression right) : base(left, right)
        {
        }

        protected override IDSLType Operate(IDSLType left, IDSLType right)
        {
            Bool res = !left.Equals(right);
            return res;
        }
    }
}
