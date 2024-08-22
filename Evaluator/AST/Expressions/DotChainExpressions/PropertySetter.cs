using DSL.Evaluator.AST.Instructions;
using DSL.Evaluator.LenguajeTypes;
using DSL.Interfaces;
using System;
using System.Collections.Generic;

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
            switch (l)
            {
                case ICard card:
                    switch (propertyName)
                    {
                        case "Power": card.Power = Convert.ToDouble(value.Evaluate()); break;
                        default: throw new Exception($"Propery {propertyName} is not a setable card property");
                    };
                    break;
                case IList<object> objList:
                    switch (propertyName)
                    {
                        case "Indexer": objList[Convert.ToInt32(args[0].Evaluate())] = value.Evaluate(); break;
                        default: throw new Exception($"Propery {propertyName} is not a setable list property");
                    }

                    break;
                case IList<ICard> list:
                    switch (propertyName)
                    {
                        case "Indexer": list[Convert.ToInt32(args[0].Evaluate())] = value.Evaluate() as ICard; break;
                        default: throw new Exception($"Propery {propertyName} is not a setable list property");
                    }
                    break;
            }
        }
    }
}
