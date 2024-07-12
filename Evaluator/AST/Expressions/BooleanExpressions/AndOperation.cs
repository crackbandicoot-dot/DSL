using DSL.Evaluator.AST.Expressions;
using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.LenguajeTypes.DSL.Evaluator.LenguajeTypes;
namespace DSL.Evaluator.AST.Expressions.BooleanExpressions
{
    internal class AndOperation : BinaryExpression
    {
        public AndOperation(IExpression left, IExpression right) : base(left, right)
        {
        }
        
        protected override IDSLType Operate(IDSLType left, IDSLType right)
        {
            if (left is Bool l && right is Bool r)
            {
                return l & r;
            }
            throw new ArgumentException("Cannot apply the operator && betwen left and rigth");
        }
    }
}

