using System;
using System.Collections.Generic;
using DSL.Evaluator.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DSL.Evaluator.LenguajeTypes;

namespace DSL.Evaluator.Expressions
{
    internal class PropertyAccess : IExpression
    {
        private readonly IExpression left;
        private readonly string propertyName;

        public PropertyAccess(IExpression left,string propertyName)
        {
            this.left = left;
            this.propertyName = propertyName;
        }

        public IDSLType Evaluate()
        {
            IDSLType l = left.Evaluate();
            Type expType = l.GetType();
            PropertyInfo? propertyInfo = expType.GetProperty(propertyName);
            if (propertyInfo != null)
            {
                return (IDSLType)propertyInfo.GetValue(l);
            }
            else
            {
                throw new Exception($"Type{expType} does not have a property{propertyName}");
            }
        }
    }
}
