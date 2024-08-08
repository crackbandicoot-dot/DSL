namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration.CardDeclration.OnActivation.Selector
{
    internal class SourceDeclaration : IInstruction
    {
        private static readonly string[] allowedValues = new[]
        {
            "hand","otherHand","deck","otherDeck","field",
            "otherField","board","parent"
        };
        private readonly LenguajeTypes.Selector parent;
        private LenguajeTypes.Selector result;
        private Dictionary<string, object> properties;

#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public SourceDeclaration(LenguajeTypes.Selector? parent, LenguajeTypes.Selector result, Dictionary<string, object> properties)
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        {
#pragma warning disable CS8601 // Posible asignación de referencia nula
            this.parent = parent;
#pragma warning restore CS8601 // Posible asignación de referencia nula
            this.result = result;
            this.properties = properties;
        }

        public void Execute()
        {
            if (properties.TryGetValue("Source", out object? value))
            {

                var source = (string)value;
                if (allowedValues.Contains(source))
                {
                    if (source == "parent")
                    {
                        result.Source = parent.Source;
                    }
                    else
                    {
                        result.Source = source;
                    }
                }
                else
                {
                    throw new Exception($"Invalid source,source can be just {string.Join(',', allowedValues)}");
                }
            }
            else
            {
                throw new ArgumentException("Selector has not source property");
            }
        }
    }
}