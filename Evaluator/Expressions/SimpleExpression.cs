using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.Expressions
{
    internal class SimpleExpression : IExpression
    {
        private readonly IDSLType value;
        public SimpleExpression(IDSLType value)
        {
            this.value = value;
        }
        public IDSLType Evaluate() => value;
    }

}
