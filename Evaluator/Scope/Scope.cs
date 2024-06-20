using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.Scope
{
    internal class Scope<T>
    {
        private readonly Dictionary<string, T> _variables = new();
        public readonly Scope<T> _parentScope;
        public Scope(Scope<T> parentScope)
        {
            _parentScope = parentScope;
        }
        public Scope<T>? ScopeInWhichIsDeclared(string identifier)
        {
            for (var currentScope = this;currentScope!=null; currentScope = currentScope._parentScope)
            {
                if (currentScope._variables.ContainsKey(identifier)) return currentScope;
            }
            return null;
        }
        public void Declare(string identifier, T value)
        {
            Scope<T>? scopeInWichIsDeclared = ScopeInWhichIsDeclared(identifier);
            if (scopeInWichIsDeclared!=null)
            {
                scopeInWichIsDeclared._variables[identifier] = value;
            }
            else
            {
                _variables[identifier] = value;
            }   
        }
        public T GetFromHierarchy(string identifier)
        {
            if (_variables.TryGetValue(identifier, out T value))
            {
                return value;
            }
            else if (_parentScope != null)
            {
                return _parentScope.GetFromHierarchy(identifier);
            }
            throw new Exception($"Variable'{identifier}' not found");
        }
    }
}
