using DSL.Lexer;
using System.Collections.Generic;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.Selector
{
    internal class SingleDeclaration : IInstruction
    {
        private readonly Token selectorToken;
        private LenguajeTypes.Selector result;
        private AnonimusObject properties;

        public SingleDeclaration(Token selectorToken,LenguajeTypes.Selector result, AnonimusObject properties)
        {
            this.selectorToken = selectorToken;
            this.result = result;
            this.properties = properties;
        }

        public void Execute()
        {
            if (properties.TryGetValue("Single", out object? value))
            {
                result.Single = (bool)(value);
            }
        }
    }
}