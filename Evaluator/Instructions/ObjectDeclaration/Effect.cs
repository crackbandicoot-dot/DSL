// Ignore Spelling: lexer DSL
namespace DSL.Evaluator.Instructions.ObjectDeclaration
{
    internal class Effect
    {
        public EffectInstantation EffectInstanciation { get; internal set; }
        public Selector Selector { get; internal set; }
        public Effect PostAction { get; internal set; }
    }
}