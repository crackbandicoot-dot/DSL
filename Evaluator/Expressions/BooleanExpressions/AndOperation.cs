using DSL.Evaluator.Expressions;
using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.LenguajeTypes.DSL.Evaluator.LenguajeTypes;
namespace DSL.Evaluator.Expressions.BooleanExpressions
{
    internal class AndOperation : BinaryExpression<Bool>
    {
        public AndOperation(Expression<Bool> left, Expression<Bool> right) : base(left, right)
        {
        }

        protected override Bool Operate(Bool left, Bool right) => left & right;
    }
}

