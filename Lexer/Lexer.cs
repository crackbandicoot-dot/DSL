// Ignore Spelling: Interpeter DSL Lexer

using System;
using System.Collections.Generic;
using System.Text;

namespace DSL.Lexer
{
    internal class Lexer
    {
        private readonly string _text;
        private int _currentCharIndex;
        private int _col;
        private int _line;
        private Stack<Token> groupTokens = new Stack<Token>();
        Position currentPos { get => new Position(_line, _col); }
        private readonly Dictionary<string, TokenType> _keyWordsTokens = new Dictionary<string, TokenType>()
        {
            {"for",TokenType.For},
            {"in",TokenType.In},
            {"while",TokenType.While},
            {"true",TokenType.Bool},
            {"false",TokenType.Bool},
            {"card",TokenType.Card},
            {"effect",TokenType.Effect},
            {"print",TokenType.Print},
            {"if",TokenType.If},
            {"Number",TokenType.NumberType},
            {"String",TokenType.StringType},
            {"Boolean",TokenType.BooleanType }
        };
        private char _currentChar => _currentCharIndex > _text.Length - 1 ? '\0' : _text[_currentCharIndex];
        internal Token CurrentToken { get; set; }

        public Lexer(string text)
        {
            _text = text;
            CurrentToken = new Token(TokenType.SOF, "", new Position(0, -1));
        }
        public void NextToken()
        {
            if (char.IsWhiteSpace(_currentChar))
            {
                SkipWhiteSpaces();
            }
            switch (_currentChar)
            {

                case '\0':
                    CurrentToken = new Token(TokenType.EOF, "", currentPos);
                    if (groupTokens.Count != 0)
                    {
                        throw new Exception($"Unmacthced {groupTokens.Peek().Value} on {groupTokens.Peek().Pos}");
                    }
                    AdvanceChar();
                    break;
                case '+':
                    CurrentToken = WithPlusToken();
                    break;
                case '-':
                    CurrentToken = WithMinusToken();
                    break;
                case '@':
                    CurrentToken = ConcatenationToken();
                    break;
                case '(':
                    CurrentToken = new Token(TokenType.OpenParenthesis, "(", currentPos);
                    CheckBalance(CurrentToken);
                    AdvanceChar();
                    break;
                case ')':
                    CurrentToken = new Token(TokenType.ClosedParenthesis, ")", currentPos);
                    CheckBalance(CurrentToken);
                    AdvanceChar();
                    break;
                case '|':
                    CurrentToken = OrToken();
                    break;
                case '&':
                    CurrentToken = AndToken();
                    break;
                case '{':
                    CurrentToken = new Token(TokenType.OpenCurlyBracket, "{", currentPos);
                    CheckBalance(CurrentToken);
                    AdvanceChar();
                    break;
                case '}':
                    CurrentToken = new Token(TokenType.ClosedCurlyBracket, "}", currentPos);
                    CheckBalance(CurrentToken);
                    AdvanceChar();
                    break;
                case '[':
                    CurrentToken = new Token(TokenType.OpenSquareBracket, "[", currentPos);
                    CheckBalance(CurrentToken);
                    AdvanceChar();
                    break;
                case ']':
                    CurrentToken = new Token(TokenType.ClosedSquareBracket, "]", currentPos);
                    CheckBalance(CurrentToken);
                    AdvanceChar();
                    break;
                case ':':
                    CurrentToken = new Token(TokenType.PropertyAssigment, ":", currentPos);
                    AdvanceChar();
                    break;
                case ',':
                    CurrentToken = new Token(TokenType.Comma, ",", currentPos);
                    AdvanceChar();
                    break;
                case ';':
                    CurrentToken = new Token(TokenType.SemiColon, ";", currentPos);
                    AdvanceChar();
                    break;
                case '"':
                    CurrentToken = StringToken();
                    break;
                case '=':
                    CurrentToken = WithEqualToken();
                    break;
                case '?':
                    CurrentToken = new Token(TokenType.Questioning, "?", currentPos);
                    AdvanceChar();
                    break;
                case '^':
                    CurrentToken = new Token(TokenType.Power, "^", currentPos);
                    AdvanceChar();
                    break;
                case '*':
                    CurrentToken = WithStarToken();
                    break;
                case '/':
                    CurrentToken = new Token(TokenType.Slash, "/", currentPos);
                    AdvanceChar();
                    break;
                case '%':
                    CurrentToken = new Token(TokenType.Modulo, "%", currentPos);
                    AdvanceChar();
                    break;
                case '<':
                    CurrentToken = WithLessToken();
                    break;
                case '>':
                    CurrentToken = WithGreaterToken();
                    break;
                case '.':
                    AdvanceChar();
                    CurrentToken = new Token(TokenType.dot, ".", currentPos);
                    break;
                case '!':
                    CurrentToken = WithExclamationToken();
                    break;
                default:
                    if (char.IsDigit(_currentChar))
                    {
                        CurrentToken = NumberToken();
                    }
                    else if (char.IsLetter(_currentChar))
                    {
                        CurrentToken = WithLetterToken();
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                    break;
            }
            //Aqui hay que checar basado en el caracter acutal
            // Lo que se va a tomar de token
        }
        private void CheckBalance(Token currentToken)
        {
            Dictionary<TokenType, TokenType> couples = new()
            {
                {TokenType.ClosedParenthesis,TokenType.OpenParenthesis},
                { TokenType.ClosedSquareBracket,TokenType.OpenSquareBracket},
                {TokenType.ClosedCurlyBracket,TokenType.OpenCurlyBracket }
            };
            if (couples.ContainsValue(currentToken.Type))
            {
                groupTokens.Push(new Token(currentToken));
            }
            else
            {
                if (groupTokens.Count == 0)
                {
                    throw new Exception($"On {currentToken.Pos} expected {couples[currentToken.Type]}");
                }
                else if (couples[currentToken.Type] != groupTokens.Peek().Type)
                {
                    throw new Exception($"Unmacthced {groupTokens.Peek().Value} on {groupTokens.Peek().Pos}");
                }
                else
                {
                    groupTokens.Pop();
                }
            }
        }
        private void SkipLineFeed()
        {
            while (_currentChar == '\n')
            {

                AdvanceChar();
                AdvanceLine();
            }
        }
        private Token WithStarToken()
        {
            AdvanceChar();
            if (_currentChar == '=')
            {
                AdvanceChar();
                return new Token(TokenType.StarAssigment, "*=", currentPos);
            }
            else
            {
                return new Token(TokenType.Star, "*", currentPos);
            }
        }
        private Token WithExclamationToken()
        {
            AdvanceChar();
            if (_currentChar == '=')
            {
                AdvanceChar();
                return new Token(TokenType.NotEqual, "!=", currentPos);
            }
            return new Token(TokenType.Not, "!", currentPos);
        }
        private void AdvanceLine()
        {
            _line++;
            _col = 0;
        }
        private Token WithGreaterToken()
        {
            AdvanceChar();
            if (_currentChar == '=')
            {
                AdvanceChar();
                return new Token(TokenType.GreaterOrEqual, ">=", currentPos);
            }
            else
            {
                return new Token(TokenType.Greater, ">", currentPos);
            }
        }
        private Token WithLessToken()
        {
            AdvanceChar();
            if (_currentChar == '=')
            {
                AdvanceChar();
                return new Token(TokenType.LessOrEqual, "<=", currentPos);

            }
            else
            {
                return new Token(TokenType.Less, "<", currentPos);
            }
        }
        private void SkipWhiteSpaces()
        {
            while (char.IsWhiteSpace(_currentChar))
            {
                if (_currentChar == '\n')
                {
                    SkipLineFeed();
                }
                else
                {
                    AdvanceChar();
                }
            }
        }
        private Token WithLetterToken()
        {
            StringBuilder sb = new StringBuilder();
            while (char.IsLetter(_currentChar) || char.IsDigit(_currentChar))
            {
                sb.Append(_currentChar);
                AdvanceChar();
            }
            string tokenString = sb.ToString();
            if (_keyWordsTokens.ContainsKey(tokenString))
            {
                return new Token(_keyWordsTokens[tokenString], tokenString, currentPos);
            }
            else
            {
                return new Token(TokenType.Identifier, tokenString, currentPos);
            }
        }
        private Token WithMinusToken()
        {
            AdvanceChar();
            if (_currentChar == '-')
            {
                AdvanceChar();
                return new Token(TokenType.Decrement, "--", currentPos);
            }
            else if (_currentChar == '=')
            {
                AdvanceChar();
                return new Token(TokenType.MinusAssigment, "-=", currentPos);
            }
            else
            {
                return new Token(TokenType.Minus, "-", currentPos);
            }
        }
        private Token WithPlusToken()
        {
            AdvanceChar();
            if (_currentChar == '+')
            {
                AdvanceChar();
                return new Token(TokenType.Increment, "++", currentPos);
            }
            else if (_currentChar == '=')
            {
                AdvanceChar();
                return new Token(TokenType.SumAssigment, "+=", currentPos);
            }
            else
            {
                return new Token(TokenType.Sum, "+", currentPos);
            }
        }
        private void TakeDigits(StringBuilder sb)
        {
            while (char.IsDigit(_currentChar))
            {
                sb.Append(_currentChar);
                AdvanceChar();
            }
        }
        private Token NumberToken()
        {
            StringBuilder sb = new StringBuilder();
            TakeDigits(sb);
            if (_currentChar == '.')
            {
                sb.Append(_currentChar);
                AdvanceChar();
                if (char.IsDigit(_currentChar))
                {
                    TakeDigits(sb);
                    return new Token(TokenType.Number, sb.ToString(), currentPos);
                }
                else
                {
                    throw new Exception("Lexical error, expected digit after .");
                }
            }
            else
            {
                return new Token(TokenType.Number, sb.ToString(), currentPos);
            }
        }
        private Token WithEqualToken()
        {
            AdvanceChar();
            if (_currentChar == '=')
            {
                AdvanceChar();
                return new Token(TokenType.Equal, "==", currentPos);
            }
            else if (_currentChar == '>')
            {
                AdvanceChar();
                return new Token(TokenType.FunctionAssigment, "=>", currentPos);
            }
            else
            {
                return new Token(TokenType.VariableAssigmnet, "=", currentPos);
            }
        }
        private Token StringToken()
        {
            StringBuilder sb = new();
            AdvanceChar();
            while (_currentChar != '"' && _currentChar != '\0')
            {
                sb.Append(_currentChar);
                AdvanceChar();
            }
            if (_currentChar == '"')
            {
                AdvanceChar();
                return new Token(TokenType.String, sb.ToString(), currentPos);
            }
            else
            {
                throw new Exception("Lexical error, expected \" ");
            }

        }
        private Token AndToken()
        {
            AdvanceChar();
            if (_currentChar == '&')
            {
                AdvanceChar();
                return new Token(TokenType.And, "&&", currentPos);
            }
            else
            {
                throw new Exception($"Lexical error,expected & in {currentPos}");
            }
        }
        private Token OrToken(
            )
        {
            AdvanceChar();
            if (_currentChar == '|')
            {
                AdvanceChar();
                return new Token(TokenType.Or, "||", currentPos);
            }
            else
            {
                throw new Exception($"Lexical error,expected | in {currentPos}");
            }
        }
        private Token ConcatenationToken()
        {
            AdvanceChar();
            if (_currentChar == '@')
            {
                AdvanceChar();
                return new Token(TokenType.ConcatenationWithSpaces, "@@", currentPos);
            }
            else
            {
                return new Token(TokenType.Concatenation, "@", currentPos);
            }
        }
        public void AdvanceChar()
        {
            _currentCharIndex++;
            _col++;
        }
    }
}
