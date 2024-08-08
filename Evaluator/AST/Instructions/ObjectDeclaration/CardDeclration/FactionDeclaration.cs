
using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration
{
    internal class FactionDeclaration : IInstruction
    {
        private static readonly string[] allowedValues = new[] { "Goods", "Bads", "Neutral" };
        private readonly Card card;
        private readonly Dictionary<string, object> properties;

        public FactionDeclaration(Card card, Dictionary<string, object> properties)
        {
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
                    throw new Exception($"Allowed values are just{string.Join(",", allowedValues)}");
                }
            }
            else
            {
                throw new Exception("Card has not faction");
            }
        }
    }
}
