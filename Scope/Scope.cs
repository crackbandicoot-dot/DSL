using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Scope
{
    internal class Scope<T>
    {
        private readonly Dictionary<string, T> _variables = new();
        public readonly Scope<T> _parentScope;
        public Scope(Scope<T> parentScope)
        {
            _parentScope = parentScope;
        }
        public void Declare(string identifier, T value)
        {
            _variables[identifier] = value;
        }
        public T GetFromHierarchy(string identifier)
        {
            if (_variables.TryGetValue(identifier,out T value))
            {
                return value;
            }
            else if (_parentScope!=null)
            {
                return _parentScope.GetFromHierarchy(identifier);
            }
            throw new Exception($"Variable'{identifier}' not found");
        }
    }
}
