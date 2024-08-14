using DSL.Extensor_Methods;
using DSL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.LenguajeTypes
{
    internal class DynamicEffect : IEffect
    {
        private readonly IEnumerable<OnActivationObject> onActivationObjects;

        public DynamicEffect(IEnumerable<OnActivationObject> onActivationObjects)
        {
            this.onActivationObjects = onActivationObjects;
        }
        public void Activate(IContext context)
        {
            onActivationObjects.ForEach(obj => obj.Activate(context));
        }
    }
}
