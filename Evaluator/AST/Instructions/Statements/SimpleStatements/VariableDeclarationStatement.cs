using DSL.Evaluator.AST.Expressions;
namespace DSL.Evaluator.AST.Instructions.Statements.SimpleStatements
{
    internal class VariableDeclarationStatement : IInstruction
    {
        private readonly Scope.Scope scope;
        private readonly string identifier;
        private readonly IExpression exp;

        public VariableDeclarationStatement(Scope.Scope scope, string identifier, IExpression exp)
        {
            this.scope = scope;
            this.identifier = identifier;
            this.exp = exp;
        }

        public void Execute()
        => scope.Declare(identifier, exp.Evaluate());

    }
}
