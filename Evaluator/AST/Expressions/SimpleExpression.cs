using DSL.Errors;
using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.AST.Expressions
{
    internal class SimpleExpression : IExpression
    {
        private readonly IDSLType value;
        public SimpleExpression(IDSLType value)
        {
            this.value = value;
        }

        public bool CheckSemantics(List<Error> compilationErrors)
        {
            return true;
        }

        public IDSLType Evaluate() => value;
    }

}
