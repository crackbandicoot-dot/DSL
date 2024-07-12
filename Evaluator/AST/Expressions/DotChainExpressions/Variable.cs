// Ignore Spelling: DSL

using DSL.Errors;
using DSL.Evaluator.AST.Expressions;
using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.Scope;
using DSL.Lexer;

namespace DSL.Evaluator.AST.Expressions.DotChainExpressions
{
    internal class Variable : IExpression
    {
        private readonly Token identifierToken;
        private readonly Scope<IDSLType> scope;

        public Variable(Token identifierToken, Scope<IDSLType> scope)
        {
            this.identifierToken = identifierToken;
            this.scope = scope;
        }

        public bool CheckSemantics(List<Error> compilationErrors)
        {
            if (scope.ScopeInWhichIsDeclared(identifierToken.Value)==null)
            {
                compilationErrors.Add(new Error($"Variable {identifierToken.Value} is not declared",identifierToken.Pos));
                return false;
            }
            return true;
        }

        public IDSLType Evaluate()
        {
            return scope.GetFromHierarchy(identifierToken.Value);
        }
    }
}
