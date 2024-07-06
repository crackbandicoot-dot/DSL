using DSL.Evaluator.Expressions;
using DSL.Evaluator.LenguajeTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Instructions.ObjectDeclaration
{
    internal class EffectDeclaration : ObjectDeclaration
    {
        public EffectDeclaration(Context context) : base(context)
        {
        }

        public string Name { get; internal set; }
        public LenguajeTypes.Action Action { get; internal set; }
        public Dictionary<string, string> Params { get; internal set; }

        protected override IDSLObject CreateObject(Dictionary<string, IExpression> properties)
        {
            throw new NotImplementedException();
        }
    }
}
