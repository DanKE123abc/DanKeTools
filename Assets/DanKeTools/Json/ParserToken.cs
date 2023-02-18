namespace DanKeTools.Json
{
    ///<summary>
    /// Developed on the basis of LitJson
    ///脚本名称： ParserToken.cs
    ///修改时间：2023/2/18
    ///脚本功能：
    ///备注：
    ///</summary>

    internal enum ParserToken
    {
        // Lexer tokens (see section A.1.1. of the manual)
        None = System.Char.MaxValue + 1,
        Number,
        True,
        False,
        Null,
        CharSeq,
        // Single char
        Char,

        // Parser Rules (see section A.2.1 of the manual)
        Text,
        Object,
        ObjectPrime,
        Pair,
        PairRest,
        Array,
        ArrayPrime,
        Value,
        ValueRest,
        String,

        // End of input
        End,

        // The empty rule
        Epsilon
    }
}
