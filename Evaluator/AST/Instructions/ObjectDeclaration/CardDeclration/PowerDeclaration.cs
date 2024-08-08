
using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration
{
    internal class PowerDeclaration : IInstruction
    {
        private readonly Card card;
        private readonly Dictionary<string, object> properties;

        public PowerDeclaration(Card card, Dictionary<string, object> properties)
        {
            this.card = card;
            this.properties = properties;
        }
        public void Execute()
        {
            if (properties.TryGetValue("Power", out object? value))
            {
                var power = (double)value;
                card.Power = power;
            }
            else
            {
                throw new Exception("Card has not Power");
            }
        }
    }
}
