using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.Expressions
{
    internal class SimpleExpression : Expression
    {
        private readonly IDSLType value;
        public SimpleExpression(IDSLType value)
        {
            this.value = value;
        }
        public override IDSLType Evaluate() => value;
    }

}
