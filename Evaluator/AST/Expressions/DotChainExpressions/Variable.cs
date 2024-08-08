// Ignore Spelling: DSL

using DSL.Lexer;

namespace DSL.Evaluator.AST.Expressions.DotChainExpressions
{
    internal class Variable : IExpression
    {
        public readonly Token identifierToken;
        public readonly Scope.Scope scope;
        public Variable(Token identifierToken, Scope.Scope scope)
        {
            this.identifierToken = identifierToken;
            this.scope = scope;
        }
        public object Evaluate()
        {
            return scope.GetFromHierarchy(identifierToken.Value);
        }
    }
}
