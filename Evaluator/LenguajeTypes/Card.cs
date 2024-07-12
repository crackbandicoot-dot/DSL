// Ignore Spelling: lexer DSL
using DSL.Evaluator.Instructions.ObjectDeclaration;

namespace DSL.Evaluator.LenguajeTypes
{
    internal class Card : IDSLObject
    {
       private AnonimusObject CardBody { get; }

       public String Name => (String)CardBody.Properties["Name"];
       public String Faction => (String)CardBody.Properties["Faction"];
       public List Range => (List)CardBody.Properties["Range"];
       public Number Power => (Number)CardBody.Properties["Power"];
       public List OnActivation => (List)CardBody.Properties["OnActivation"];
       
       public void ActivateEffect(IContext context, Context declaredEffects)
       {
            foreach (var effectInfo in OnActivation)
            {
                var info = effectInfo as AnonimusObject;
                String effectName = (String)info.UseDotNotation("Effect.Name");
                if(declaredEffects.Acced(effectName) is Effect e)
                {
                    e.Action.Invoke(context);
                }
                else
                {
                    
                }
            }
        }
       public Card(AnonimusObject cardBody)
       {
            CardBody = cardBody;
       }
       public bool Equals(IDSLType? other)
       {
           throw new NotImplementedException();
       }
    }

}