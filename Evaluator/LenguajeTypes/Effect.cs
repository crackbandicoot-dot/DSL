

using System.Collections.Generic;

namespace DSL.Evaluator.LenguajeTypes
{
    internal class Effect
    {
       public Effect()
       {
            Params = new();
       }
        public string Name { get; internal set; }
        public Action Action { get; internal set; }
        public Dictionary<string, TypeRestriction> Params { get; internal set; }
    }
}
