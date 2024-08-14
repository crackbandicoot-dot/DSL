
using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation
{
    internal class OnActivationDeclaration : IInstruction
    {
        private readonly CardInfo card;
        private readonly Dictionary<string, object> properties;
        private readonly Context context;

        public OnActivationDeclaration(CardInfo card, Dictionary<string, object> properties, Context context)
        {
            this.card = card;
            this.properties = properties;
            this.context = context;
        }
        public void Execute()
        {
            if (properties.TryGetValue("OnActivation", out object? value))
            {
                var OnActivation = ((List<object>)value)
                    .Select(x => (Dictionary<string, object>)x)
                    .ToList();
                var result = new List<OnActivationObject>();
                foreach (var onActivationObj in OnActivation)
                {
                    var declaration = new
                        OnActivationObjectDeclaration(onActivationObj, result, context);
                    declaration.Execute();
                }
                card.OnActivation = result;
            }
        }
    }
}
