using DSL.Evaluator.Expressions;
using DSL.Evaluator.Instructions.Statements.ConditionalStatements;
using DSL.Evaluator.Instructions.Statements.LoopStatements;
using DSL.Evaluator.Instructions.Statements.SimpleStatements;
using DSL.Evaluator.Instructions.Statements;
using DSL.Evaluator.Instructions;
using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.Scope;
using DSL.Lexer;

namespace DSL.Parser
{
    internal partial class Parser
    {
        private VariableDeclarationStatement ParseVariableDeclaration(Scope<IDSLType> scope)
        {
            string id = stream.Eat(TokenType.Identifier).Value;
            stream.Eat(TokenType.VariableAssigmnet);
            IExpression exp = Exp(scope);
            stream.Eat(TokenType.SemiColon);
            return new VariableDeclarationStatement(scope, id, exp);
        }
        private PrintStatement ParsePRINT(Scope<IDSLType> scope)
        {
            stream.Eat(TokenType.Print);
            stream.Eat(TokenType.OpenParenthesis);
            IExpression str = Exp(scope);
            stream.Eat(TokenType.ClosedParenthesis);
            stream.Eat(TokenType.SemiColon);
            return new PrintStatement(str);
        }
        private ForStatement ParseFor(Scope<IDSLType> scope)
        {
            stream.Eat(TokenType.For);
            stream.Eat(TokenType.OpenParenthesis);
            string forVariable = stream.Eat(TokenType.Identifier).Value;
            stream.Eat(TokenType.In);
            IExpression list = Exp(scope);
            stream.Eat(TokenType.ClosedParenthesis);
            stream.Eat(TokenType.OpenCurlyBracket);
            InstructionBlock instructionBlock = ParseInstructionBlock(scope);
            stream.Eat(TokenType.ClosedCurlyBracket);
            return new ForStatement(forVariable, list, instructionBlock, scope);
        }
        private WhileStatement ParseWHILE(Scope<IDSLType> scope)
        {
            stream.Eat(TokenType.While);
            stream.Eat(TokenType.OpenParenthesis);
            IExpression condition = Exp(scope);
            stream.Eat(TokenType.ClosedParenthesis);
            stream.Eat(TokenType.OpenCurlyBracket);
            InstructionBlock block = ParseInstructionBlock(scope);
            stream.Eat(TokenType.ClosedCurlyBracket);
            return new WhileStatement(condition, block);
        }
        private IfStatement ParseIF(Scope<IDSLType> scope)
        {
            stream.Eat(TokenType.If);
            stream.Eat(TokenType.OpenParenthesis);
            IExpression condition = Exp(scope);
            stream.Eat(TokenType.ClosedParenthesis);
            stream.Eat(TokenType.OpenCurlyBracket);
            InstructionBlock block = ParseInstructionBlock(scope);
            stream.Eat(TokenType.ClosedCurlyBracket);
            return new IfStatement(condition, block);
        }
        private InstructionBlock ParseInstructionBlock(Scope<IDSLType>? parentScope)
        {
            List<IInstruction> instructions = new();
            Scope<IDSLType> scope = new(parentScope);
            TokenType[] instructionBlockAllowedTokens = new TokenType[]
            {
                TokenType.If,
                TokenType.While,
                TokenType.Print,
                TokenType.Identifier,
                TokenType.For,
                TokenType.Number,
                TokenType.String,
                TokenType.OpenSquareBracket,
                TokenType.Bool,
                TokenType.OpenParenthesis,
                TokenType.Not,
                TokenType.Minus
            };
            while (stream.Match(instructionBlockAllowedTokens))
            {
                switch (stream.CurrentToken.Type)
                {
                    case TokenType.Print:
                        instructions.Add(ParsePRINT(scope));
                        break;
                    case TokenType.If:
                        instructions.Add(ParseIF(scope));
                        break;
                    case TokenType.While:
                        instructions.Add(ParseWHILE(scope));
                        break;
                    case TokenType.For:
                        instructions.Add(ParseFor(scope));
                        break;
                    case TokenType.Identifier:
                        switch (stream.LookNextToken().Type)
                        {
                            case TokenType.VariableAssigmnet:
                                instructions.Add(ParseVariableDeclaration(scope));
                                break;
                            case TokenType.dot or TokenType.OpenSquareBracket:
                                IExpression exp = Exp(scope);
                                if (exp is IInstruction I)
                                {
                                    instructions.Add(I);
                                    stream.Eat(TokenType.SemiColon);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        throw new Exception("Just statements, calls, asigments, increasing and decrasing operators are valid instructions");
                }
            }
            return new InstructionBlock(instructions, scope);
        }
    }
}
