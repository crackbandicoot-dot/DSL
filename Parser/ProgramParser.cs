// Ignore Spelling: lexer DSL
using DSL.Evaluator.AST.Expressions;
using DSL.Evaluator.AST.Instructions;
using DSL.Evaluator.AST.Instructions.ObjectDeclaration;
using DSL.Evaluator.AST.Instructions.ObjectDeclaration.EffectDeclaration;
using DSL.Evaluator.AST.Instructions.ObjectDeclaration.NewFolder;
using DSL.Lexer;
using System.Collections.Generic;


namespace DSL.Parser
{
    internal partial class ProgramParser
    {
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
            var cardToken =  stream.Eat(TokenType.Card);
            IExpression body = AnonimusTypeExpression(null);
            return new CardDeclaration(cardToken,context, body);
        }
        private EffectDeclaration Effect(Context context)
        {
            var effectToken = stream.Eat(TokenType.Effect);
            IExpression body = AnonimusTypeExpression(null);
            return new EffectDeclaration(effectToken,context, body);
        }
    }

}
