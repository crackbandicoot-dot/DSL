using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.LenguajeTypes
{
    internal class Effect : IDSLObject
    {
        public AnonimusObject EffectBody { get; }
        public String Name => (String)EffectBody.Properties["Name"];
        public Action Action => (Action)EffectBody.Properties["Action"];
        public Effect(AnonimusObject effectBody)
        {
            EffectBody = effectBody;
        }
        
        public bool Equals(IDSLType? other)
        {
            throw new NotImplementedException();
        }
    }
}
