﻿// Ignore Spelling: lexer DSL
using DSL.Evaluator.AST.Expressions;
using DSL.Evaluator.AST.Instructions;
using DSL.Evaluator.AST.Instructions.ObjectDeclaration;
using DSL.Evaluator.AST.Instructions.ObjectDeclaration.EffectDeclaration;
using DSL.Evaluator.AST.Instructions.ObjectDeclaration.NewFolder;
using DSL.Lexer;


namespace DSL.Parser
{

    internal partial class Parser
    {
        // public IInstruction ParseAST() => GwentProgram();
        public GwentProgram GwentProgram()
        {
            Context context = new();
            List<IInstruction> instructions = new();
            while (stream.Match(TokenType.Effect, TokenType.Card))
            {
                if (stream.Match(TokenType.Effect))
                {
                    instructions.Add(Effect(context));
                }
                else
                {
                    instructions.Add(Card(context));
                }
            }
            return new GwentProgram(instructions, context);
        }
        private CardDeclaration Card(Context context)
        {
            stream.Eat(TokenType.Card);
#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
            IExpression body = AnonimusTypeExpression(null);
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
            return new CardDeclaration(context, body);
        }
        private EffectDeclaration Effect(Context context)
        {
            stream.Eat(TokenType.Effect);
#pragma warning disable CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
            IExpression body = AnonimusTypeExpression(null);
#pragma warning restore CS8625 // No se puede convertir un literal NULL en un tipo de referencia que no acepta valores NULL.
            return new EffectDeclaration(context, body);
        }
    }

}
