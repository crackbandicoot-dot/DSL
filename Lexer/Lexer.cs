// Ignore Spelling: Interpeter DSL Lexer

using System.Text;

namespace DSL.Lexer
{
    internal class Lexer
    {
        private readonly string _text;
        private int _col;
        private int _line;

        private readonly Dictionary<string, TokenType> _keyWordsTokens = new Dictionary<string, TokenType>()
        {
            //TODO
            //Agregar las palabras claves que faltan
            {"for",TokenType.For},
            {"in",TokenType.In},
            {"while",TokenType.While},
            {"true",TokenType.Bool},
            {"false",TokenType.Bool},
            {"card",TokenType.Card},
            {"effect",TokenType.Effect},
            {"print",TokenType.Print},
            {"if",TokenType.If},
            {"Effect",TokenType.EffectInstanciation},
            {"Action",TokenType.Action},
            {"Params",TokenType.Params },
            {"Name",TokenType.Name},
            {"Type",TokenType.Type},
            {"Faction",TokenType.Faction},
            {"Power",TokenType.Power},
            {"Range",TokenType.Range},
            {"OnActivation",TokenType.OnActivation},
            {"Number",TokenType.NumberType},
            {"String",TokenType.StringType},
            {"Boolean",TokenType.BooleanType }
        };
        private char _currentChar => _col > _text.Length - 1 ? '\0' : _text[_col];

        public Token CurrentToken { get; private set; }

        public Lexer(string text)
        {
            _text = text;
            CurrentToken = new Token(TokenType.SOF, "", new Position(0, -1));
        }
        public void NextToken()
        {
            //TODO 
            //Poner una pila
            //Hanilitar la fila y columna adecuadamente
            Position currentPos = new Position(_line, _col);
            if (char.IsWhiteSpace(_currentChar))
            {
                SkipWhiteSpaces();
            }
            if (_currentChar == '\n')
            {
                SkipLineFeed();
            }
            switch (_currentChar)
            {

                case '\0':
                    CurrentToken = new Token(TokenType.EOF, "", currentPos);
                    AdvanceChar();
                    break;
                case '+':
                    CurrentToken = WithPlusToken(currentPos);
                    break;
                case '-':
                    CurrentToken = WithMinuslToken(currentPos);
                    break;
                case '@':
                    CurrentToken = ConcatenationToken(currentPos);
                    break;
                case '(':
                    CurrentToken = new Token(TokenType.OpenParenthesis, "(", currentPos);
                    AdvanceChar();
                    break;
                case ')':
                    CurrentToken = new Token(TokenType.ClosedParenthesis, ")", currentPos);
                    AdvanceChar();
                    break;
                case '|':
                    CurrentToken = OrToken(currentPos);
                    break;
                case '&':
                    CurrentToken = AndToken(currentPos);
                    break;
                case '{':
                    CurrentToken = new Token(TokenType.OpenCurlyBracket, "{", currentPos);
                    AdvanceChar();
                    break;
                case '}':
                    CurrentToken = new Token(TokenType.ClosedCurlyBracket, "}", currentPos);
                    AdvanceChar();
                    break;
                case '[':
                    CurrentToken = new Token(TokenType.OpenSquareBracket, "[", currentPos);
                    AdvanceChar();
                    break;
                case ']':
                    CurrentToken = new Token(TokenType.ClosedSquareBracket, "]", currentPos);
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
                    CurrentToken = StringToken(currentPos);
                    break;
                case '=':
                    CurrentToken = WithEqualToken(currentPos);
                    break;
                case '<':
                    CurrentToken = WithLessToken(currentPos);
                    break;
                case '>':
                    CurrentToken = WithGreaterToken(currentPos);
                    break;
                case '.':
                    AdvanceChar();
                    CurrentToken = new Token(TokenType.dot, ".", currentPos);
                    break;
                case '!':
                    CurrentToken = WithExclamationToken(currentPos);
                    break;
                default:
                    if (char.IsDigit(_currentChar))
                    {
                        CurrentToken = NumberToken(currentPos);
                    }
                    else if (char.IsLetter(_currentChar))
                    {
                        CurrentToken = WithLetterToken(currentPos);
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

        private void SkipLineFeed()
        {
            while (_currentChar == '\n')
            {
                AdvanceChar();
                AdvanceLine();
            }
        }
        private Token WithExclamationToken(Position currentPos)
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
        }
        private Token WithGreaterToken(Position currentPos)
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

        private Token WithLessToken(Position currentPos)
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
                AdvanceChar();
            }
        }

        private Token WithLetterToken(Position pos)
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
                return new Token(_keyWordsTokens[tokenString], tokenString, pos);
            }
            else
            {
                return new Token(TokenType.Identifier, tokenString, pos);
            }
        }

        private Token WithMinuslToken(Position currentPos)
        {
            AdvanceChar();
            if (_currentChar == '-')
            {
                AdvanceChar();
                return new Token(TokenType.Decrement, "--", currentPos);
            }
            else
            {
                return new Token(TokenType.Minus, "-", currentPos);
            }
        }

        private Token WithPlusToken(Position position)
        {
            AdvanceChar();
            if (_currentChar == '+')
            {
                AdvanceChar();
                return new Token(TokenType.Increment, "++", position);
            }
            else
            {
                return new Token(TokenType.Sum, "+", position);
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
        private Token NumberToken(Position position)
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
                    return new Token(TokenType.Number, sb.ToString(), position);
                }
                else
                {
                    throw new Exception("Lexical error, expected digit after .");
                }
            }
            else
            {
                return new Token(TokenType.Number, sb.ToString(), position);
            }
        }

        private Token WithEqualToken(Position position)
        {
            AdvanceChar();
            if (_currentChar == '=')
            {
                AdvanceChar();
                return new Token(TokenType.Equal, "==", position);
            }
            else if (_currentChar == '>')
            {
                AdvanceChar();
                return new Token(TokenType.FunctionAssigment, "=>", position);
            }
            else
            {
                return new Token(TokenType.VariableAssigmnet, "=", position);
            }
        }

        private Token StringToken(Position position)
        {
            StringBuilder sb = new();
            sb.Append(_currentChar);
            AdvanceChar();
            while (_currentChar != '"' && _currentChar != '\0')
            {
                if (_currentChar == '"')
                {
                    Console.WriteLine("C# ES PINGA");
                }
                sb.Append(_currentChar);
                AdvanceChar();
            }
            if (_currentChar == '"')
            {
                sb.Append('"');
                AdvanceChar();
                return new Token(TokenType.String, sb.ToString(), position);
            }
            else
            {
                throw new Exception("Lexical error, expected \" ");
            }

        }

        private Token AndToken(Position position)
        {
            AdvanceChar();
            if (_currentChar == '&')
            {
                AdvanceChar();
                return new Token(TokenType.And, "&&", position);
            }
            else
            {
                throw new Exception($"Lexical error,expected & in {position}");
            }
        }

        private Token OrToken(Position position)
        {
            AdvanceChar();
            if (_currentChar == '|')
            {
                AdvanceChar();
                return new Token(TokenType.Or, "||", position);
            }
            else
            {
                throw new Exception($"Lexical error,expected | in {position}");
            }
        }

        private Token ConcatenationToken(Position position)
        {
            AdvanceChar();
            if (_currentChar == '@')
            {
                AdvanceChar();
                return new Token(TokenType.ConcatenationWithSpaces, "@@", position);
            }
            else
            {
                return new Token(TokenType.Concatenation, "@", position);
            }
        }

        public void AdvanceChar()
        {
            _col++;
        }
    }
}
