
using DSL.Evaluator.AST.Expressions;
using DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration;
using DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation;
using DSL.Evaluator.LenguajeTypes;
using DSL.Lexer;
using System.Collections.Generic;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.NewFolder
{
    internal class CardDeclaration : IInstruction
    {
        private readonly Token cardToken;
        private readonly Context context;
        private readonly IExpression cardBody;

        public CardDeclaration(Token cardToken,Context context, IExpression cardBody)
        {
            this.cardToken = cardToken;
            this.context = context;
            this.cardBody = cardBody;
        }
        public void Execute()
        {
            CardInfo card = new();
            var properties = (AnonimusObject)(cardBody.Evaluate());
            var nameDeclaration = new NameDeclaration(cardToken,card, properties);
            var factionDeclaration = new FactionDeclaration(cardToken, card, properties);
            var rangeDeclaration = new RangeDeclaration(cardToken,card, properties);
            var powerDeclaration = new PowerDeclaration(cardToken,card, properties);
            var onActivationDeclaration = new OnActivationDeclaration(cardToken,card, properties, context); ;
            var typeDeclaration = new TypeDeclaration(cardToken,card, properties);
            nameDeclaration.Execute();
            factionDeclaration.Execute();
            rangeDeclaration.Execute();
            powerDeclaration.Execute();
            onActivationDeclaration.Execute();
            typeDeclaration.Execute();
            context.Declare(card);
        }
    }
}
