// Ignore Spelling: Lexer

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DSL.Lexer
{
    internal class LexerStream : IEnumerable<Token>
    {

        private readonly List<Token> _baseList = new();
        private int _position = 0;
        internal Token CurrentToken { get => Peek(0); }
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
        public Token Eat(params TokenType[] types)
        {
            if (Match(types))
            {
                Token result = new(CurrentToken);
                Advance();
                return result;
            }
            throw new NotImplementedException($"Sintax error, expected {string.Join("or", types)} token in {CurrentToken.Pos}");
        }
        public bool Match(params TokenType[] types) => types.Any(t => t == CurrentToken.Type);
        public bool MatchPrefix(params TokenType[] prefix)
        {
            for (int i = 0; i < prefix.Length; i++)
            {
                if (Peek(i).Type != prefix[i]) return false;
            }
            return true;
        }
        public override string ToString()
        {
            return string.Join(',', _baseList);
        }
        public IEnumerator<Token> GetEnumerator()
        {
            return ((IEnumerable<Token>)_baseList).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_baseList).GetEnumerator();
        }
    }
}
