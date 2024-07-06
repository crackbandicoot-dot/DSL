using DSL.Evaluator.Expressions;
using DSL.Evaluator.LenguajeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Instructions.ObjectDeclaration
{
    internal abstract class ObjectDeclaration : IInstruction
    {
        private readonly Context context;
        private Dictionary<string, IExpression> properties;
        public ObjectDeclaration(Context context)
        {
            properties = new Dictionary<string, IExpression>();
            this.context = context;
        }
        public void DeclareProperty(string propertyID, IExpression expression)
        {
            properties.Add(propertyID, expression);
        }
        protected abstract IDSLObject CreateObject(Dictionary<string, IExpression> properties);
        public void Execute()
        {
            IDSLObject obj = CreateObject(properties);
            context.Declare(obj);
        }
    }
}
