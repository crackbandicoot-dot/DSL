using DSL.Evaluator.AST.Instructions;
using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.AST.Expressions.DotChainExpressions
{
    internal class PropertySetter : IExpression, IInstruction
    {
        private readonly IExpression left;
        private readonly string propertyName;
        private readonly IExpression value;
        private readonly List<IExpression>? args;

        public PropertySetter(IExpression left, string propertyName, IExpression value, List<IExpression>? args = null)
        {
            this.left = left;
            this.propertyName = propertyName;
            this.value = value;
            this.args = args;
        }
        public object Evaluate()
        {
            return typeof(void);
        }
        public void Execute()
        {
            object l = left.Evaluate();
            if (l is CardInfo card)
            {
                Dictionary<string, System.Action> propertieSeter = new()
                {
                    {"Power",() => card.Power=(double)value.Evaluate()},
                };
                propertieSeter[propertyName].Invoke();
            }
            else if (l is List<object> objList)
            {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
                Dictionary<string, System.Action> propertieSeter = new()
                {
                    {"Indexer",() => objList[(int)Math.Floor((double)args[0].Evaluate())] = value.Evaluate()},
                };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
                propertieSeter[propertyName].Invoke();
            }
            else if (l is List<CardInfo> list)
            {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
                Dictionary<string, System.Action> propertieSeter = new()
                {
                    {"Indexer",() => list[(int)args[0].Evaluate()] = (CardInfo)value.Evaluate()},
                };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
                propertieSeter[propertyName].Invoke();
            }
        }
    }
}
