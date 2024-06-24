// Ignore Spelling: DSL

using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.Scope;

namespace DSL.Evaluator.Expressions.DotChainExpressions
{
    internal class Variable : IExpression
    {
        private readonly Scope<IDSLType> scope;
        private readonly string id;

        public Variable(string id, Scope<IDSLType> scope)
        {
            this.id = id;
            this.scope = scope;
        }
        public IDSLType Evaluate()
        {
            return scope.GetFromHierarchy(id);
        }
    }
}
