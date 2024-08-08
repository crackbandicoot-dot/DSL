
using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration
{
    internal class RangeDeclaration : IInstruction
    {
        private static readonly string[] allowedValues = new[] { "Melee", "Ranged", "Siesge" };
        private readonly Card card;
        private readonly Dictionary<string, object> properties;

        public RangeDeclaration(Card card, Dictionary<string, object> properties)
        {
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
                    string current = (string)rangeValue;
                    if (!allowedValues.Contains(current))
                    {
                        throw new Exception($"{current} is not a valid range element");
                    }
                    else if (values.Contains(current))
                    {
                        throw new Exception($"{current} is  a already a range element");
                    }
                    else
                    {
                        values.Add(current);
                    }
                }
                card.Range = values.ToList();
            }
            else
            {
                throw new Exception("card has not a Range Property");
            }
        }
    }
}
