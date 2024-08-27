using DSL.Evaluator.AST.Expressions;
using DSL.Lexer;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.Selector
{
    internal class PredicateDeclaration : IInstruction
    {
        private readonly Token sourceToken;
        private LenguajeTypes.Selector result;
        private readonly AnonimusObject selectorProperties;

        public PredicateDeclaration(Token sourceToken,LenguajeTypes.Selector result, AnonimusObject selectorProperties)
        {
            this.sourceToken = sourceToken;
            this.result = result;
            this.selectorProperties = selectorProperties;
        }

        public void Execute()
        {
            if (selectorProperties.ContainsKey("Predicate")) 
            {
                result.Predicate = (LenguajeTypes.Delegate)selectorProperties["Predicate"];
            }
            else
            {
                result.Predicate = new LenguajeTypes.Delegate(new[] { "unit" }, new SimpleExpression(true), new Expressions.Scope.Scope(null));
            }
        }
    }
}