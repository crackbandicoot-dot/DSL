using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.Expressions
{
    internal interface IExpression
    {
        public IDSLType Evaluate();
    }

}
