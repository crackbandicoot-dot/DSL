// Ignore Spelling: DSL

namespace DSL.Lexer
{
    internal enum TokenType
    {
        //KeyWords

        //si    si   Si  Si  Si
        Effect, Card, For, In, While,


        //Types
        //Si   Si      Si    No     No
        Number, String, Bool, List, Array,


        //Si
        Identifier,

        // Operators

        //Si      //Si
        Increment, Decrement,
        //Aritmeticos
        //Si si    si   si
        Sum, Minus, Star, Slash,

        //Logicos
        //si si
        And, Or,
        //Comparacion
        //si  no      no     no          no
        Equal, Less, Greater, LessOrEqual, GreaterOrEqual,
        //Concatenacion
        //si             si
        Concatenation, ConcatenationWithSpaces,

        //Asignacion
        //si               si                    si
        PropertyAssigment, VariableAssigmnet, FunctionAssigment,

        //Puntuaction
        //si      si    no
        SemiColon, Comma, dot,

        //si              si                   si            si                si
        OpenParenthesis, ClosedParenthesis, OpenCurlyBracket, ClosedCurlyBracket, OpenSquareBracket,
        //si
        ClosedSquareBracket,
        //si  si
        SOF, EOF,
        If,
        Not,
        NotEqual,
        Print,
        Power,
        Action,
        Params,
        Name,
        BooleanType,
        StringType,
        NumberType,
        Type,
        Faction,
        Range,
        OnActivation,
        EffectInstanciation,
        Selector,
        Source,
        Single,
        Predicate,
        PostAction,
        Questioning,
        Modulo,
        SumAssigment,
        MinusAssigment,
        StarAssigment
    }
}
