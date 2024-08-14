// Ignore Spelling: DSL

using DSL.Evaluator.LenguajeTypes;
using DSL.Interfaces;
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
            if (obj is ICard c)
            {
                return propertyName switch
                {
                    "Name"=> c.Name,
                    "Range"=> c.Range,
                    "Faction"=>c.Faction,
                    "Power"=> c.Power,
                    "Type"=> c.Type,
                    _=> throw new Exception($"Card does not have a property{propertyName}")
                };
            }
            else if (obj is IList<object> objectList)
            {
                return propertyName switch
                {
                    "Count" => objectList.Count,
                    "Indexer" => objectList[int.Parse(args[0].Evaluate().ToString())],
                    _ => throw new Exception($"Exception")
                };
            }
            else if (obj is IList<ICard> list)
            {
                return propertyName switch
                {
                    "Count" => double.Parse(list.Count.ToString()),
                    "Indexer" => list[int.Parse(args[0].Evaluate().ToString())],
                    _ => throw new Exception($"List does not have a property{propertyName}")
                } ; 
              
            }
            else if (obj is IContext context)
            {
                return propertyName switch
                {
                    "Board"=> context.Board,
                    "TriggerPlayer"=>(double)context.TriggerPlayer,
                    "Hand"=> context.HandOfPlayer(context.TriggerPlayer),
                    "Field" => context.FieldOfPlayer(context.TriggerPlayer),
                    "Deck"=> context.DeckOfPlayer(context.TriggerPlayer),
                    "GraveYard"=>context.GraveYardOfPlayer(context.TriggerPlayer),
                };
            }
            throw new Exception("The given type is not a valid lenguaje type");
        }
    }
}

