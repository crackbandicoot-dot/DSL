using DSL.Evaluator.Expressions;
using DSL.Evaluator.LenguajeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Instructions.ObjectDeclaration
{
    internal class CardDeclaration : ObjectDeclaration
    {
        public CardDeclaration(Context context) : base(context)
        {
        }
        public string Name { get; internal set; }
        public string Type { get; internal set; }
        public string Faction { get; internal set; }
        public double Power { get; internal set; }
        public string[] Range { get; internal set; }
        public Effect[] OnActivation{ get; internal set; }
        protected override IDSLObject CreateObject(Dictionary<string, IExpression> properties)
        {
            throw new NotImplementedException();
        }
    }
}

