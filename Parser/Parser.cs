// Ignore Spelling: lexer DSL
using DSL.Evaluator.AST.Expressions;
using DSL.Evaluator.AST.Instructions;
using DSL.Evaluator.AST.Instructions.ObjectDeclaration;
using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.Scope;
using DSL.Lexer;

namespace DSL.Parser
{

    internal partial class Parser
    {
        private Context _context = new();
        public Context Context { get => _context; }
      
        public IEnumerable<IInstruction> ParseAST()
        {
            while (stream.Match(TokenType.Effect,TokenType.Card))
            {
                if (stream.Match(TokenType.Effect))
                {
                   yield return Effect(_context);
                }
                else
                {
                   yield return Card(_context);
                }
            }
        }

        private CardDeclaration Card(Context context)
        {
            stream.Eat(TokenType.Card);
            IExpression body = AnonimusTypeExpression(null);
            return new CardDeclaration(context, body);
        }

        private EffectDeclaration Effect(Context context)
        {
            stream.Eat(TokenType.Effect);
            IExpression body = AnonimusTypeExpression(null);
            return new EffectDeclaration(context,body);
        }
    }

}
