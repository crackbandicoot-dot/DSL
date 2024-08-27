
using DSL.Evaluator.LenguajeTypes;
using DSL.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation
{
    internal class OnActivationDeclaration : IInstruction
    {
        private readonly Token cardToken;
        private readonly CardInfo card;
        private readonly AnonimusObject properties;
        private readonly Context context;

        public OnActivationDeclaration(Token cardToken,CardInfo card, AnonimusObject properties, Context context)
        {
            this.cardToken = cardToken;
            this.card = card;
            this.properties = properties;
            this.context = context;
        }
        public void Execute()
        {
            if (properties.TryGetValue("OnActivation", out object? value))
            {
                var onActivationToken = properties.GetAssociatedToken("OnActivation");
                var OnActivation = ((List<object>)value)
                    .Select(x => (AnonimusObject)x)
                    .ToList();
                var result = new List<OnActivationObject>();
                foreach (var onActivationObj in OnActivation)
                {
                    var declaration = new
                        OnActivationObjectDeclaration(onActivationToken,onActivationObj, result, context);
                    declaration.Execute();
                }
                card.OnActivation = result;
            }
            else
            {
                throw new Exception($"Card has not OnActivation property in {cardToken.Pos}");
            }
        }
    }
}
