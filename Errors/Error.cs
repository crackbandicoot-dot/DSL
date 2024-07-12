using DSL.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Errors
{
    internal class Error
    {
        public string Message { get; }
        public Position Position { get; }

        public Error( string message, Position position)
        {
            Message = message;
            Position = position;
        }
    }
}
