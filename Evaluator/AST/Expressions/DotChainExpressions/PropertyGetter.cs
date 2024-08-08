// Ignore Spelling: DSL

using DSL.Evaluator.LenguajeTypes;
using System.Reflection;

namespace DSL.Evaluator.AST.Expressions.DotChainExpressions
{
    internal class PropertyGetter : IExpression
    {
        private static readonly Dictionary<Type, PropertyInfo> propertyCache = new();
        public readonly IExpression left;
        public string propertyName;
        public readonly List<IExpression>? args;

        public PropertyGetter(IExpression left, string propertyName, List<IExpression>? args = null)
        {
            this.left = left;
            this.propertyName = propertyName;
            this.args = args;
        }
        public object Evaluate()
        {
            var obj = left.Evaluate();
            if (obj is Card c)
            {
                Dictionary<object, object> a = new()
                {
                    {"Name", c.Name},
                    {"Range", c.Range},
                    {"Faction", c.Faction},
                    {"Power", c.Power},
                    {"Type", c.Type}
                };
                return a[propertyName];
            }
            else if (obj is List<object> objectList)
            {

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8604 // Posible argumento de referencia nulo
                return propertyName switch
                {
                    "Count" => objectList.Count,
                    "Indexer" => objectList[int.Parse(args[0].Evaluate().ToString())],
                    _ => throw new Exception($"Exception")
                };
#pragma warning restore CS8604 // Posible argumento de referencia nulo
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            }
            else if (obj is List<Card> list)
            {
                Dictionary<object, object> a = new()
                {
                    {"Count",double.Parse(list.Count.ToString())},
                   // {"Indexer",list[(int)args[0].Evaluate()]},
                };
                return a[propertyName];
            }
            else if (obj is IContext context)
            {
                Dictionary<object, object> a = new()
                {
                    {"Board", context.Board},
                    {"TriggerPlayer",(double)context.TriggerPlayer},
                    {"Hand", context.HandOfPlayer(context.TriggerPlayer)},
                    {"Field", context.FieldOfPlayer(context.TriggerPlayer)},
                    {"Deck", context.DeckOfPLayer(context.TriggerPlayer)},
                    {"GraveYard", context.GraveYardOfPlayer(context.TriggerPlayer)},
                };
                return a[propertyName];
            }
            throw new Exception("The given type is not a valid lenguaje type");
            //object l = left.Evaluate();
        }
    }
}

