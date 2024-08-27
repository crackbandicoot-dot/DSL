using DSL.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.Selector
{
    internal class SourceDeclaration : IInstruction
    {
        private static readonly string[] allowedValues = new[]
        {
            "hand","otherHand","deck","otherDeck","field",
            "otherField","board","parent"
        };
        private readonly Token selectorToken;
        private readonly LenguajeTypes.Selector? parent;
        private LenguajeTypes.Selector result;
        private AnonimusObject properties;
        public SourceDeclaration(Token selectorToken,LenguajeTypes.Selector? parent, LenguajeTypes.Selector result, AnonimusObject properties)
        {
            this.selectorToken = selectorToken;
            this.parent = parent;
            this.result = result;
            this.properties = properties;
        }

        public void Execute()
        {
            if (properties.TryGetValue("Source", out object? value))
            {
                var sourceToken = properties.GetAssociatedToken("Source");
                var source = (string)value;
                if (allowedValues.Contains(source))
                {
                    if (source == "parent")
                    {
                        if (parent is not null)
                        {
                            result.Source = parent.Source;
                        }
                        else
                        {
                            throw new Exception($"No parent source to assign{sourceToken.Pos}");
                        }
                    }
                    else
                    {
                        result.Source = source;
                    }
                }
                else
                {
                    throw new Exception($"Invalid source,source can be just {string.Join(',', allowedValues)} in {sourceToken.Pos}");
                }
            }
            else
            {
                throw new Exception($"Selector has not source property in {selectorToken.Pos}");
            }
        }
    }
}