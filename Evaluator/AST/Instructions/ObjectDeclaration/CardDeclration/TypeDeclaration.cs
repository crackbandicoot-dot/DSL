
using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration
{
    internal class TypeDeclaration : IInstruction
    {
        private static readonly string[] allowedValues = new[] {"Gold",
            "Silver","Decoy","Leader","Weather","Boost"};
        private readonly Dictionary<string, object> properties;
        private readonly CardInfo card;

        public TypeDeclaration(CardInfo card, Dictionary<string, object> properties)
        {
            this.card = card;
            this.properties = properties;
        }
        public void Execute()
        {
            if (properties.TryGetValue("Type", out object? value))
            {
                var type = (string)value;
                if (allowedValues.Contains(type))
                {
                    card.Type = type;
                }
                else
                {
                    throw new Exception($"Allowed values are just{string.Join(",", allowedValues)}");
                }
            }
            else
            {
                throw new Exception("Card has not Type");
            }
        }
    }
}
