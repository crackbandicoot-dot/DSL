
using DSL.Evaluator.LenguajeTypes;
using DSL.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration
{
    internal class TypeDeclaration : IInstruction
    {
        private static readonly string[] allowedValues = new[] {"Gold",
            "Silver","Decoy","Leader","Weather","Boost","Clearing"};
        private readonly AnonimusObject properties;
        private readonly Token cardToken;
        private readonly CardInfo card;

        public TypeDeclaration(Token cardToken, CardInfo card, AnonimusObject properties)
        {
            this.cardToken = cardToken;
            this.card = card;
            this.properties = properties;
        }
        public void Execute()
        {
            if (properties.TryGetValue("Type", out object? value))
            {
                var typeToken = properties.GetAssociatedToken("Type");
                var type = (string)value;
                if (allowedValues.Contains(type))
                {
                    card.Type = type;
                }
                else
                {
                    throw new Exception($"Allowed values of Type are just{string.Join(",", allowedValues)} in {typeToken.Pos}");
                }
            }
            else
            {
                throw new Exception($"Card has not Type in {cardToken.Pos}");
            }
        }
    }
}
