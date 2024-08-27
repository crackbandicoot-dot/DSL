
using DSL.Evaluator.LenguajeTypes;
using DSL.Lexer;
using System;
using System.Collections.Generic;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration
{
    internal class PowerDeclaration : IInstruction
    {
        private readonly Token cardToken;
        private readonly CardInfo card;
        private readonly AnonimusObject properties;

        public PowerDeclaration(Token cardToken,CardInfo card, AnonimusObject properties)
        {
            this.cardToken = cardToken;
            this.card = card;
            this.properties = properties;
        }
        public void Execute()
        {
            if (properties.TryGetValue("Power", out object? value))
            {
                var powerToken = properties.GetAssociatedToken("Power");
                if (value is double power)
                {
                    card.Power = power;
                }
                else
                {
                    throw new Exception($"Power must be a number {powerToken.Pos}");
                }
            }
            else
            {
                throw new Exception($"Card has not a defined Power in {cardToken.Pos}");
            }
        }
    }
}
