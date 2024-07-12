using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.LenguajeTypes
{
    internal class AnonimusObject : IDSLType
    {
        public AnonimusObject(Dictionary<string,IDSLType> properties)
        {
            Properties = properties;
        }
        public Dictionary<string, IDSLType> Properties { get; }
        public IDSLType UseDotNotation(string dotNotation)
        {
            
            var splitedDotNotation = dotNotation.Split(".");
            var current = Properties[splitedDotNotation[0]];
            for (int i = 1; i < splitedDotNotation.Length; i++)
            {

                if (current is AnonimusObject obj)
                {
                   current = obj.Properties[splitedDotNotation[i]];  
                }
            }
            return current;
        }
        public bool Equals(IDSLType? other)
        {
            throw new NotImplementedException();
        }
    }
}
