using DSL.Evaluator.AST.Expressions;
using DSL.Evaluator.AST.Instructions;
using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.Scope;

namespace DSL.Evaluator.AST.Instructions.Statements.SimpleStatements
{
    internal class VariableDeclarationStatement : IInstruction
    {
        private readonly Scope<IDSLType> scope;
        private readonly string identifier;
        private readonly IExpression exp;

        public VariableDeclarationStatement(Scope<IDSLType> scope, string identifier, IExpression exp)
        {
            this.scope = scope;
            this.identifier = identifier;
            this.exp = exp;
        }

        public void Execute()
        {
            scope.Declare(identifier, exp.Evaluate());
        }
    }
}
