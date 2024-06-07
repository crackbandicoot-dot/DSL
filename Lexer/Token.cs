// Ignore Spelling: DSL Lexer

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Lexer
{
    internal class Token
    {
        public TokenType Type { get; }
        public string Value { get; }
        public Position Pos { get; }

        public Token(TokenType type, string value, Position pos)
        {
            Type = type;
            Value = value;
            Pos = pos;
        }
        public Token(Token tokenToCopy)
        {
            Type = tokenToCopy.Type;
            Value = (string)tokenToCopy.Value.Clone();
            Pos = tokenToCopy.Pos;
        }
        public override string ToString() => $"({Type},{Value})";

    }
}
