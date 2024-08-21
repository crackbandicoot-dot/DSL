using DSL.Evaluator.AST.Expressions;
using DSL.Evaluator.AST.Expressions.Scope;
using DSL.Evaluator.AST.Instructions;
using DSL.Evaluator.AST.Instructions.Statements;
using DSL.Evaluator.AST.Instructions.Statements.ConditionalStatements;
using DSL.Evaluator.AST.Instructions.Statements.LoopStatements;
using DSL.Evaluator.AST.Instructions.Statements.SimpleStatements;
using DSL.Lexer;
using System;
using System.Collections.Generic;

namespace DSL.Parser
{
    internal partial class ProgramParser
    {
        private InstructionBlock InstructionBlock(Scope parentScope)
        {
            List<IInstruction> instructions = new();
            Scope scope = new(parentScope);
            if (stream.Match(TokenType.OpenCurlyBracket))
            {
                stream.Eat(TokenType.OpenCurlyBracket);
                while (!stream.Match(TokenType.ClosedCurlyBracket, TokenType.EOF))
                {
                    instructions.Add(Statement(scope));
                }
                stream.Eat(TokenType.ClosedCurlyBracket);
            }
            else
            {
                instructions.Add(Statement(scope));
            }
            return new InstructionBlock(instructions, scope);
        }
        private IInstruction Statement(Scope scope)
        {
            return stream.CurrentToken.Type switch
            {
                TokenType.OpenCurlyBracket => InstructionBlock(scope),
                TokenType.Print => Print(scope),
                TokenType.If => If(scope),
                TokenType.While => While(scope),
                TokenType.For => For(scope),
                _ => ExpressionStatement(scope),
            };
        }
        private PrintStatement Print(Scope scope)
        {
            stream.Eat(TokenType.Print);
            stream.Eat(TokenType.OpenParenthesis);
            IExpression str = Exp(scope);
            stream.Eat(TokenType.ClosedParenthesis);
            stream.Eat(TokenType.SemiColon);
            return new PrintStatement(str);
        }
        private ForStatement For(Scope scope)
        {
            stream.Eat(TokenType.For);
            string forVariable = stream.Eat(TokenType.Identifier).Value;
            stream.Eat(TokenType.In);
            IExpression list = Exp(scope);
            InstructionBlock instructionBlock = InstructionBlock(scope);
            return new ForStatement(forVariable, list, instructionBlock, scope);
        }
        private WhileStatement While(Scope scope)
        {
            stream.Eat(TokenType.While);
            stream.Eat(TokenType.OpenParenthesis);
            IExpression condition = Exp(scope);
            stream.Eat(TokenType.ClosedParenthesis);
            InstructionBlock block = InstructionBlock(scope);
            return new WhileStatement(condition, block);
        }
        private IfStatement If(Scope scope)
        {
            stream.Eat(TokenType.If);
            stream.Eat(TokenType.OpenParenthesis);
            IExpression condition = Exp(scope);
            stream.Eat(TokenType.ClosedParenthesis);
            InstructionBlock block = InstructionBlock(scope);
            return new IfStatement(condition, block);
        }
        private IInstruction ExpressionStatement(Scope scope)
        {
            var exp = Exp(scope);
            if (exp is IInstruction instruction)
            {
                stream.Eat(TokenType.SemiColon);
                return instruction;
            }
            else
            {
                throw new Exception($"The expression {exp} is not a valid statement");
            }
        }
    }
}
