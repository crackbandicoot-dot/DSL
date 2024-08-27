
using DSL.Evaluator.LenguajeTypes;
using DSL.Lexer;
using System;
using System.Collections.Generic;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration
{
    internal class NameDeclaration : IInstruction
    {
        private readonly Token cardToken;
        private readonly CardInfo card;
        private readonly AnonimusObject properties;

        public NameDeclaration(Token cardToken,CardInfo card, AnonimusObject properties)
        {
            this.cardToken = cardToken;
            this.card = card;
            this.properties = properties;
        }

        public void Execute()
        {
            if (properties.TryGetValue("Name", out object? value))
            {
                card.Name = (string)value;
            }
            else
            {
                throw new Exception($"Card has not name in {cardToken.Pos}");
            }
        }
    }
}
