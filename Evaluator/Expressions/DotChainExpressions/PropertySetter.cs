using DSL.Evaluator.Instructions;
using DSL.Evaluator.LenguajeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Expressions.DotChainExpressions
{
    internal class PropertySetter : IExpression, IInstruction
    {
        private readonly IExpression left;
        private readonly string propertyName;
        private readonly IExpression value;
        private readonly List<IExpression>? args;


        public PropertySetter(IExpression left, string propertyName,IExpression value, List<IExpression> args)
        {
            this.left = left;
            this.propertyName = propertyName;
            this.value =  value;
            this.args = args;
        }
        public PropertySetter(IExpression left, string propertyName, IExpression value)
        {
            this.left = left;
            this.propertyName = propertyName;
            this.value = value;
        }

        public IDSLType Evaluate()
        {
            return new None();
        }
        public void Execute()
        {
            IDSLType l = left.Evaluate();
            Type type = l.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            if (propertyInfo != null)
            {
                if (args is null)
                {
                    propertyInfo.SetValue(l, value.Evaluate());
                }
                else
                {
                    propertyInfo.SetValue(l, value.Evaluate(), args.Select(x => x.Evaluate()).ToArray());
                }
            }
            else
            {
                throw new Exception($"Type {type} does not have a definition for property {propertyName}");
            }

        }
    }
}
