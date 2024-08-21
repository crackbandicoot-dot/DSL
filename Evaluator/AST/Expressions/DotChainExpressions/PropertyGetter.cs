// Ignore Spelling: DSL

using DSL.Evaluator.LenguajeTypes;
using DSL.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DSL.Evaluator.AST.Expressions.DotChainExpressions
{
    internal class PropertyGetter : IExpression
    {
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
            return obj switch
            {
                ICard c => propertyName switch
                {
                    "Name" => c.Name,
                    "Range" => c.Range,
                    "Faction" => c.Faction,
                    "Power" => c.Power,
                    "Type" => c.Type,
                    _ => throw new Exception($"Card does not have a property{propertyName}")
                },
                IList<object> objectList => propertyName switch
                {
                    "Count" => objectList.Count,
                    "Indexer" => objectList[Convert.ToInt32(args[0].Evaluate())],
                    _ => throw new Exception($"Exception")
                },
                IList<ICard> list => propertyName switch
                {
                    "Count" => double.Parse(list.Count.ToString()),
                    "Indexer" => list[Convert.ToInt32(args[0].Evaluate())],
                    _ => throw new Exception($"List does not have a property{propertyName}")
                },
                IContext context => propertyName switch
                {
                    "Board" => context.Board,
                    "TriggerPlayer" => (double)context.TriggerPlayer,
                    "Hand" => context.HandOfPlayer(context.TriggerPlayer),
                    "Field" => context.FieldOfPlayer(context.TriggerPlayer),
                    "Deck" => context.DeckOfPlayer(context.TriggerPlayer),
                    "GraveYard" => context.GraveYardOfPlayer(context.TriggerPlayer),
                    _ => throw new Exception($"Context type does not have a property called {propertyName}")
                },
                _ => throw new Exception("The given type is not a valid lenguaje type"),
            };
        }
    }
}

