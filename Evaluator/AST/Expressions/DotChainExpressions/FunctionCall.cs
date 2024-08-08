// Ignore Spelling: DSL

using DSL.Evaluator.AST.Instructions;
using DSL.Evaluator.LenguajeTypes;

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
            if (l is IContext context)
            {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
#pragma warning disable CS8604 // Posible argumento de referencia nulo
#pragma warning disable CS8604 // Posible argumento de referencia nulo
#pragma warning disable CS8604 // Posible argumento de referencia nulo
                Dictionary<string, object> functions = new()
                {
                    {"DeckOfPlayer",context.DeckOfPLayer(int.Parse(args[0].Evaluate().ToString()))},
                    {"HandOfPlayer",context.HandOfPlayer(int.Parse(args[0].Evaluate().ToString()))},
                    {"GraveYardOfPlayer",context.GraveYardOfPlayer(int.Parse(args[0].Evaluate().ToString()))},
                    {"FieldOfPlayer",context.FieldOfPlayer(int.Parse(args[0].Evaluate().ToString()))}
                };
#pragma warning restore CS8604 // Posible argumento de referencia nulo
#pragma warning restore CS8604 // Posible argumento de referencia nulo
#pragma warning restore CS8604 // Posible argumento de referencia nulo
#pragma warning restore CS8604 // Posible argumento de referencia nulo
                return functions[functionName];
            }
            else if (l is LenguajeTypes.Delegate d)
            {
                return functionName switch
                {
                    "Invoke" => d.Invoke(args.Select(x => x.Evaluate()).ToArray()),
                    _ => throw new Exception($"{l.GetType} doesn't have a{functionName} function"),
                };

            }
            else if (l is List<Card> cardList)
            {
                return functionName switch
                {
                    "Remove" => cardList.Remove((Card)args[0].Evaluate()),
                    "Push" => cardList.Push((Card)args[0].Evaluate()),
                    "Pop" => cardList.Pop(),
                    _ => throw new Exception($"{l.GetType} doesn't have a{functionName} function"),
                };
            }
            else if (l is List<object> list)
            {
                return functionName switch
                {
                    "Remove" => list.Remove(args[0].Evaluate()),
                    "Push" => list.Push((Card)args[0].Evaluate()),
                    "Pop" => list.Pop(),
                    _ => throw new Exception($"{l.GetType} doesn't have a{functionName} function"),
                };
            }
            else
            {
                throw new Exception($"{l.GetType} type doesn't have a {functionName} method");
            }
            //object left = leftExpression.Evaluate();
            //Type type = left.GetType();
            //MethodInfo? methodInfo = type.GetMethod(functionName);
            //if (methodInfo == null)
            //{
            //    throw new ArgumentException($"Type {type} doesn't have a method {functionName}");
            //}
            //else
            //{
            //    object? result = methodInfo.Invoke(left, args.Select(x => x.Evaluate()).ToArray());
            //    if (result == null)
            //    {
            //        return typeof(void);
            //    }
            //    return left;
            //}

        }
        public void Execute()
        {
            Evaluate();
        }
    }
}
