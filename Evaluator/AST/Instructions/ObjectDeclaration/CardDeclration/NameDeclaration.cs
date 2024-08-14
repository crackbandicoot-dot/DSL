
using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration
{
    internal class NameDeclaration : IInstruction
    {
        private readonly CardInfo card;
        private readonly Dictionary<string, object> properties;

        public NameDeclaration(CardInfo card, Dictionary<string, object> properties)
        {
            this.card = card;
            this.properties = properties;
        }

        public void Execute()
        {
            if (properties.TryGetValue("Name", out object? value))
            {
                card.Name = (string)value;
            }
            else
            {
                throw new Exception("Card has not name");
            }
        }
    }
}
