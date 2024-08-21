using DSL.Lexer;
namespace DSL.Errors
{
    internal class Error
    {
        public string Message { get; }
        public Position Position { get; }

        public Error(string message, Position position)
        {
            Message = message;
            Position = position;
        }
    }
}
