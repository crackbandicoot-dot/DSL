
// Ignore Spelling: DSL

using DSL.Evaluator.AST.Instructions;
using DSL.Evaluator.LenguajeTypes;
using DSL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSL.Evaluator.AST.Expressions.DotChainExpressions
{
    internal class FunctionCall : IExpression, IInstruction
    {
        private readonly IExpression leftExpression;
        private readonly string functionName;
        private readonly List<IExpression> args;
        public FunctionCall(IExpression leftExpression, string functionName, List<IExpression> args)
        {
            this.leftExpression = leftExpression;
            this.functionName = functionName;
            this.args = args;
        }
        public object Evaluate()
        {
            object l = leftExpression.Evaluate();
            return l switch
            {
                IContext context => functionName switch
                {
                    "DeckOfPlayer" => context.DeckOfPlayer(Convert.ToInt32(args[0].Evaluate())),
                    "HandOfPlayer" => context.HandOfPlayer(Convert.ToInt32(args[0].Evaluate())),
                    "GraveYardOfPlayer" => context.GraveYardOfPlayer(Convert.ToInt32(args[0].Evaluate())),
                    "FieldOfPlayer" => context.FieldOfPlayer(Convert.ToInt32(args[0].Evaluate())),
                    _ => throw new Exception($"IContext does not have a{functionName} method"),
                },
                LenguajeTypes.Delegate d => functionName switch
                {
                    "Invoke" => d.Invoke(args.Select(x => x.Evaluate()).ToArray()),
                    _ => throw new Exception($"{l.GetType()} doesn't have a{functionName} function"),
                },
                IList<ICard> cardList => functionName switch
                {
                    "Remove" => cardList.Remove((ICard)args[0].Evaluate()),
                    "Push" => cardList.Push((ICard)args[0].Evaluate()),
                    "Pop" => cardList.Pop(),
                    _ => throw new Exception($"{l.GetType()} doesn't have a{functionName} function"),
                },
                IList<object> list => functionName switch
                {
                    "Remove" => list.Remove(args[0].Evaluate()),
                    "Push" => list.Push(args[0].Evaluate()),
                    "Pop" => list.Pop(),
                    _ => throw new Exception($"{l.GetType()} doesn't have a{functionName} function"),
                },
                _ => throw new Exception($"{l.GetType()} type is not a valid lenguaje type")
            };
        }
        public void Execute()
        {
            Evaluate();
        }
    }
}
