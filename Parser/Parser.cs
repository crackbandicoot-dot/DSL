// Ignore Spelling: lexer DSL
using DSL.Evaluator.Expressions;
using DSL.Evaluator.Instructions.ObjectDeclaration;
using DSL.Evaluator.LenguajeTypes;
using DSL.Evaluator.Scope;
using DSL.Lexer;


namespace DSL.Parser
{

    internal partial class Parser
    {
        //TODO, Arreglar las cosas para que funcionen en cualquier orden
        #region CardDeclaration
        private CardDeclaration ParseCardInfo(Context context)
        {
            stream.Eat(TokenType.Card);
            stream.Eat(TokenType.OpenCurlyBracket);
            CardDeclaration cardDeclaration = ParseCardBody(context);
            stream.Eat(TokenType.ClosedCurlyBracket);
            return cardDeclaration;
        }
        private CardDeclaration ParseCardBody(Context context)
        {
            List<TokenType> takenKeywords = new();
            CardDeclaration cardDeclaration = new(context);
            SetCardProperty(cardDeclaration, takenKeywords);
            while (stream.Match(TokenType.Comma))
            {
                stream.Eat(TokenType.Comma);
                SetCardProperty(cardDeclaration, takenKeywords);
            }
            return cardDeclaration;
        }
        private string[] ParseRange()
        {
            //TODO tal vez checkar la semantica
            stream.Eat(TokenType.Range);
            stream.Eat(TokenType.PropertyAssigment);
            stream.Eat(TokenType.OpenSquareBracket);
            List<string> l = new();
            if (stream.Match(TokenType.ClosedSquareBracket))
            {
                stream.Eat(TokenType.ClosedSquareBracket);
                return l.ToArray();
            }
            else
            {
                l.Add(ParseString());
                while (stream.Match(TokenType.Comma))
                {
                    stream.Eat(TokenType.Comma);
                    l.Add(ParseString());
                }
                stream.Eat(TokenType.ClosedSquareBracket);
                return l.ToArray();
            }
        }
        private double ParsePower()
        {
            stream.Eat(TokenType.Power);
            stream.Eat(TokenType.PropertyAssigment);
            return double.Parse(stream.Eat(TokenType.Number).Value);
        }
        private string ParseFaction()
        {
            stream.Eat(TokenType.Faction);
            stream.Eat(TokenType.PropertyAssigment);
            return ParseString();
        }
        private string ParseType()
        {
            stream.Eat(TokenType.Type);
            stream.Eat(TokenType.PropertyAssigment);
            return stream.Eat(TokenType.Type).Value;
        }
        private void SetCardProperty(CardDeclaration cardDeclaration, List<TokenType> takenKeywords)
        {
            if (takenKeywords.Contains(stream.CurrentToken.Type))
            {
                throw new Exception($"In {stream.CurrentToken.Pos} property {stream.CurrentToken.Type} has been already declared");
            }
            else
            {
                switch (stream.CurrentToken.Type)
                {
                    case TokenType.Name:
                        cardDeclaration.Name = ParseName();
                        takenKeywords.Add(stream.CurrentToken.Type);
                        break;
                    case TokenType.Type:
                        cardDeclaration.Type = ParseType();
                        takenKeywords.Add(stream.CurrentToken.Type);
                        break;
                    case TokenType.Faction:
                        cardDeclaration.Faction = ParseFaction();
                        takenKeywords.Add(stream.CurrentToken.Type);
                        break;
                    case TokenType.Power:
                        cardDeclaration.Power = ParsePower();
                        takenKeywords.Add(stream.CurrentToken.Type);
                        break;
                    case TokenType.Range:
                        cardDeclaration.Range = ParseRange();
                        takenKeywords.Add(stream.CurrentToken.Type);
                        break;
                    case TokenType.OnActivation:
                        cardDeclaration.OnActivation = ParseOnActivation();
                        takenKeywords.Add(stream.CurrentToken.Type);
                        break;
                }
            }
        }
       
        //OnActivation
        private Effect[] ParseOnActivation()
        {
            List<Effect> r = new();
            stream.Eat(TokenType.OpenSquareBracket);
            if (stream.Match(TokenType.ClosedSquareBracket))
            {
                stream.Eat(TokenType.ClosedSquareBracket);
            }
            else
            {
                r.Add(ParseEffect());
                while (stream.Match(TokenType.Comma))
                {
                    r.Add(ParseEffect());
                }
            }
            return r.ToArray();
        }
        private Effect ParseEffect()
        {
            Effect e = new();
            stream.Eat(TokenType.OpenCurlyBracket);
            e.EffectInstanciation = ParseEffectInstanciation();
            stream.Eat(TokenType.Comma);
            e.Selector = ParseSelector();
            stream.Eat(TokenType.Comma);
            if (stream.Match(TokenType.PostAction))
            {
                e.PostAction = ParseEffect();
            }
            else
            {
                e.PostAction = null;
            }
            stream.Eat(TokenType.ClosedCurlyBracket);
            throw new NotImplementedException();
        }
        private EffectInstantation ParseEffectInstanciation()
        {
            EffectInstantation ei = new();
            stream.Eat(TokenType.EffectInstanciation);
            stream.Eat(TokenType.PropertyAssigment);
            stream.Eat(TokenType.OpenCurlyBracket);
            ei.Name = ParseName();
            while (stream.Match(TokenType.Comma))
            {
                ParseParmetrsAsignation(ei.Parmeters);
            }
            stream.Eat(TokenType.ClosedCurlyBracket);
            return ei;
        }
        private void ParseParmetrsAsignation(Dictionary<string, object> parmeters)
        {
            string id = stream.Eat(TokenType.Identifier).Value;
            stream.Eat(TokenType.PropertyAssigment);
            object value = ParseToken(stream.Eat(TokenType.Bool, TokenType.Number, TokenType.String));
            parmeters.Add(id, value);
        }
        private Selector ParseSelector()
        {
            Selector selector = new();
            stream.Eat(TokenType.Selector);
            stream.Eat(TokenType.PropertyAssigment);
            stream.Eat(TokenType.OpenCurlyBracket);
            ParseSelectorProperty(selector);
            while (stream.Match(TokenType.Comma))
            {
                ParseSelectorProperty(selector);
            }
            stream.Eat(TokenType.ClosedCurlyBracket);
            return selector;
        }
        private void ParseSelectorProperty(Selector selector)
        {
            switch (stream.CurrentToken.Type)
            {
                case TokenType.Source:
                    selector.Source = ParseSoure();
                    break;
                case TokenType.Single:
                    ParseSingle();
                    selector.Single = ParseSingle();
                    break;
                case TokenType.Predicate:
                    ParsePredicate();
                    selector.Predicate = ParsePredicate();
                    break;
            }
        }
        private string ParseSoure()
        {
            stream.Eat(TokenType.Source);
            stream.Eat(TokenType.PropertyAssigment);
            return stream.Eat(TokenType.String).Value;
        }
        private bool ParseSingle()
        {
            stream.Eat(TokenType.Single);
            stream.Eat(TokenType.PropertyAssigment);
            return bool.Parse(stream.Eat(TokenType.Bool).Value);
        }
        private IExpression ParsePredicate()
        {
            stream.Eat(TokenType.Predicate);
            stream.Eat(TokenType.PropertyAssigment);
            return Exp(new Scope<IDSLType>(null));
        }
        #endregion
        #region EffectDeclaration
        private EffectDeclaration ParseEffectInfo(Context context)
        {
            stream.Eat(TokenType.Effect);
            stream.Eat(TokenType.OpenCurlyBracket);
            EffectDeclaration effectInfo = ParseEffectBody(context);
            stream.Eat(TokenType.ClosedCurlyBracket);
            return effectInfo;
        }
        private EffectDeclaration ParseEffectBody(Context context)
        {
            List<TokenType> takenKeywords = new();
            EffectDeclaration effectInfo = new(context);
            SetEffectProperty(effectInfo, takenKeywords);
            while (stream.Match(TokenType.Comma))
            {
                stream.Eat(TokenType.Comma);
                SetEffectProperty(effectInfo, takenKeywords);
            }
            return effectInfo;
        }
        public void SetEffectProperty(EffectDeclaration effectInfo, List<TokenType> takenKeywords)
        {
            if (takenKeywords.Contains(stream.CurrentToken.Type))
            {
                throw new Exception($"In {stream.CurrentToken.Pos} property {stream.CurrentToken.Type} has been already declared");
            }
            else
            {
                switch (stream.CurrentToken.Type)
                {
                    case TokenType.Name:
                        effectInfo.Name = ParseName();
                        break;
                    case TokenType.Action:
                        effectInfo.Action = ParseAction();
                        break;
                    case TokenType.Params:
                        effectInfo.Params = ParseParams();
                        break;
                }
            }
        }
        private Dictionary<string, string> ParseParams()
        {
            stream.Eat(TokenType.Params);
            stream.Eat(TokenType.OpenCurlyBracket);
            Dictionary<string, string> parameters = new();
            ParseParam(parameters);
            while (stream.Match(TokenType.Comma))
            {
                stream.Eat(TokenType.Comma);
                ParseParam(parameters);
            }
            stream.Eat(TokenType.ClosedCurlyBracket);
            return parameters;
        }
        private void ParseParam(Dictionary<string, string> parameters)
        {
            string id = stream.Eat(TokenType.Identifier).Value;
            stream.Eat(TokenType.PropertyAssigment);
            string type = stream.Eat(TokenType.BooleanType, TokenType.NumberType, TokenType.StringType).Value;
            parameters.Add(id, type);
        }
        private Evaluator.LenguajeTypes.Action ParseAction()
        {
            stream.Eat(TokenType.Action);
            stream.Eat(TokenType.PropertyAssigment);
            stream.Eat(TokenType.OpenParenthesis);
            string firstId = stream.Eat(TokenType.Identifier).Value;
            stream.Eat(TokenType.Comma);
            string secondId = stream.Eat(TokenType.Identifier).Value;
            stream.Eat(TokenType.ClosedParenthesis);
            stream.Eat(TokenType.FunctionAssigment);
            stream.Eat(TokenType.OpenCurlyBracket);
            var instructionBlock = ParseInstructionBlock(null);
            stream.Eat(TokenType.ClosedCurlyBracket);
            return new Evaluator.LenguajeTypes.Action(new string[] { firstId, secondId }, instructionBlock);
        }
        private string ParseName()
        {
            stream.Eat(TokenType.Name);
            stream.Eat(TokenType.PropertyAssigment);
            return ParseString();
        }
        private string ParseString() => stream.Eat(TokenType.String).Value;
        # endregion
        public void ParseAST()
        {
            Context context = new();
            while (stream.Match(TokenType.Effect,TokenType.Card))
            {
                if (stream.Match(TokenType.Effect))
                {
                    ParseEffectInfo(context);
                }
                else
                {
                    ParseCardInfo(context);
                }
            }
        }
        public void NextInstruction()
        {
            CurrentInstruction = ParseEffectInfo(new Context());
        }

    }

}
