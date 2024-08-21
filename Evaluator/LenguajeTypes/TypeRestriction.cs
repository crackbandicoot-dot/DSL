using System;
using System.Collections.Generic;

namespace DSL.Evaluator.LenguajeTypes
{
    internal class TypeRestriction
    {
        private static Dictionary<string, Predicate<object>> checkRestriction =
            new()
            {
                {"Number",x=>x is double||x is int },
                {"Boolean",x =>x is bool },
                {"String", x => x is string },
            };
        private readonly string typeToRestrict;

        public TypeRestriction(string typeToRestrict)
        {
            this.typeToRestrict = typeToRestrict;
        }
        internal void Check(object obj)
        {
            if (!checkRestriction[typeToRestrict].Invoke(obj))
            {
                throw new Exception("Incorrect type");
            }
        }
    }
}
