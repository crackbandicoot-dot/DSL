// Ignore Spelling: DSL Lexer

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public override string ToString() => $"Line: {Line} Column: {Column}";
    }
}
