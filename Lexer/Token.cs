// Ignore Spelling: DSL Lexer

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
        public override string ToString() => $"( Token of type {Type} and value {Value} on {Pos})";

    }
}
