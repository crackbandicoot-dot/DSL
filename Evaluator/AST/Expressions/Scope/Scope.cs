using System;
using System.Collections.Generic;

namespace DSL.Evaluator.AST.Expressions.Scope
{
    internal class Scope
    {
        private readonly Dictionary<string, object> _variables = new();
        public readonly Scope? _parentScope;
        public Scope(Scope? parentScope)
        {
            _parentScope = parentScope;
        }
        public Scope? ScopeInWhichIsDeclared(string identifier)
        {
            for (var currentScope = this; currentScope != null; currentScope = currentScope._parentScope)
            {
                if (currentScope._variables.ContainsKey(identifier)) return currentScope;
            }
            return null;
        }
        public void Declare(string identifier, object value)
        {
            Scope? scopeInWichIsDeclared = ScopeInWhichIsDeclared(identifier);
            if (scopeInWichIsDeclared != null)
            {
                scopeInWichIsDeclared._variables[identifier] = value;
            }
            else
            {
                _variables[identifier] = value;
            }
        }
        public object GetFromHierarchy(string identifier)
        {
            if (_variables.TryGetValue(identifier, out object? value))
            {
                return value;
            }
            else if (_parentScope != null)
            {
                return _parentScope.GetFromHierarchy(identifier);
            }
            throw new Exception($"Variable '{identifier}' not found");
        }
    }
}
