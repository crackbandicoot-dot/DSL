using DSL.Extensor_Methods;
using DSL.Lexer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DSL.Evaluator.AST.Instructions.ObjectDeclaration
{
    class AnonimusObject : IDictionary<string, object>
    {
        private readonly Dictionary<Token, object> _properties;
        private readonly Dictionary<string, Token> _propertiesNames;

        public AnonimusObject(Dictionary<Token, object> properties)
        {
            _properties = properties;
            _propertiesNames = new();
            _properties.Keys.ForEach(
                key => _propertiesNames.Add(key.Value, key));
        }
        public Token GetAssociatedToken(string id)
        {
            return _propertiesNames[id];
        }
        public object this[string key] {
            get => _properties[_propertiesNames[key]];
            set => _properties[_propertiesNames[key]] = value; }

        public ICollection<string> Keys => _propertiesNames.Keys;

        public ICollection<object> Values => _properties.Values;

        public int Count => _propertiesNames.Count;

        public bool IsReadOnly => true;

        public void Add(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return _propertiesNames.ContainsKey(item.Key) &&
                _properties.ContainsValue(item.Value);
        }

        public bool ContainsKey(string key)
        {
            return _propertiesNames.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (var key in _propertiesNames.Keys)
            {
                yield return new KeyValuePair<string, object>(key, _properties[_propertiesNames[key]]);
            }
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out object value)
        {
            if (_propertiesNames.TryGetValue(key, out Token? k))
            {
                if (_properties.TryGetValue(k, out object? v))
                {
                    value = v;
                    return true;
                }
                else
                {
                    value = null;
                    return false;
                }
            }
            else
            {
                value = null;
                return false;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
