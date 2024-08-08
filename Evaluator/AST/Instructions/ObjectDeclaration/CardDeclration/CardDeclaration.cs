
using DSL.Evaluator.AST.Expressions;
using DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration;
using DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation;
using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.NewFolder
{
    internal class CardDeclaration : IInstruction
    {
        private readonly Context context;
        private readonly IExpression cardBody;

        public CardDeclaration(Context context, IExpression cardBody)
        {
            this.context = context;
            this.cardBody = cardBody;
        }
        public void Execute()
        {
            Card card = new();
            var properties = (Dictionary<string, object>)(cardBody.Evaluate());
            var nameDeclaration = new NameDeclaration(card, properties);
            var factionDeclaration = new FactionDeclaration(card, properties);
            var rangeDeclaration = new RangeDeclaration(card, properties);
            var powerDeclaration = new PowerDeclaration(card, properties);
            var onActivationDeclaration = new OnActivationDeclaration(card, properties, context); ;
            var typeDeclaration = new TypeDeclaration(card, properties);
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
