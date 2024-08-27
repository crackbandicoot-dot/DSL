
using DSL.Evaluator.LenguajeTypes;
using DSL.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration
{
    internal class FactionDeclaration : IInstruction
    {
        private static readonly string[] allowedValues = new[] { "Goods", "Bads", "Neutral" };
        private readonly Token cardToken;
        private readonly CardInfo card;
        private readonly AnonimusObject properties;

        public FactionDeclaration(Token cardToken,CardInfo card, AnonimusObject properties)
        {
            this.cardToken = cardToken;
            this.card = card;
            this.properties = properties;
        }
        public void Execute()
        {
            if (properties.TryGetValue("Faction", out object? value))
            {
                var faction = (string)value;
                if (allowedValues.Contains(faction))
                {
                    card.Faction = faction;
                }
                else
                {
                    throw new Exception($"In {properties.GetAssociatedToken("Faction").Pos}, allowed factions values are just{string.Join(",", allowedValues)}");
                }
            }
            else
            {
                throw new Exception($"Card has not faction in {cardToken.Pos}");
            }
        }
    }
}
