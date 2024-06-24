// Ignore Spelling: Lexer

namespace DSL.Lexer
{
    internal class LexerStream
    {

        private List<Token> _baseList = new List<Token>();
        private int _position = 0;
        public Token CurrentToken { get => Peek(0); }
        public LexerStream(string input)
        {
            FullList(new Lexer(input), _baseList);
        }
        private static void FullList(Lexer lexer, List<Token> baseList)
        {
            lexer.NextToken();
            while (lexer.CurrentToken.Type != TokenType.EOF)
            {
                baseList.Add(lexer.CurrentToken);
                lexer.NextToken();
            }
        }
        public Token LookNextToken() => Peek(1);
        public Token LookBackToken() => Peek(-1);
        public Token Peek(int amountOfSteps) => _position + amountOfSteps < _baseList.Count ? _baseList[_position + amountOfSteps] : new Token(TokenType.EOF, "", new Position(0, 0));
        public void Advance(int numberOfSteps = 1) => _position += numberOfSteps;
        public void Close(int numberOfSteps = 1) => _position -= numberOfSteps;
        public Token Match(TokenType tokenType)
        {
            if (CurrentToken.Type == tokenType)
            {
                Token result = new Token(CurrentToken);
                Advance();
                return result;
            }
            throw new NotImplementedException($"Sintax error {tokenType} expected");
        }
    }
}
