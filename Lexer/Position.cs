// Ignore Spelling: DSL Lexer

namespace DSL.Lexer
{
    internal struct Position
    {
        public int Line { get; }
        public int Column { get; }
        public Position(int line, int column)
        {
            Line = line;
            Column = column;
        }
        public override readonly string ToString() => $"Line: {Line} Column: {Column}";
    }
}
