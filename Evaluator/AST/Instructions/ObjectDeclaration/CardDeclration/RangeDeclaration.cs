
using DSL.Evaluator.LenguajeTypes;
using DSL.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration
{
    internal class RangeDeclaration : IInstruction
    {
        private static readonly string[] allowedValues = new[] { "Melee", "Ranged", "Siesge" };
        private readonly Token cardToken;
        private readonly CardInfo card;
        private readonly AnonimusObject properties;

        public RangeDeclaration(Token cardToken,CardInfo card, AnonimusObject properties)
        {
            this.cardToken = cardToken;
            this.card = card;
            this.properties = properties;
        }

        public void Execute()
        {
            if (properties.TryGetValue("Range", out object? value))
            {
                HashSet<string> values = new HashSet<string>();
                var range = (List<object>)value;
                foreach (var rangeValue in range)
                {
                    var rangeToken = properties.GetAssociatedToken("Range");

                    if (rangeValue is string current)
                    {
                        if (!allowedValues.Contains(current))
                        {
                            throw new Exception($"{current} is not a valid range element in {rangeToken.Pos}");
                        }
                        else if (values.Contains(current))
                        {
                            throw new Exception($"{current} is  a already a range element in {rangeToken.Pos}");
                        }
                        else
                        {
                            values.Add(current);
                        }
                    }
                    else
                    {
                        throw new Exception($"Elements of range must be strings in {rangeToken.Pos}");
                    }
                }
                card.Range = values.ToList();
            }
            else
            {
                throw new Exception($"Card has not a Range Property in {cardToken.Pos}");
            }
        }
    }
}
