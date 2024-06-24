namespace DSL.Evaluator.LenguajeTypes
{
    public readonly struct String : IDSLType
    {
        private readonly string value;
        public String(string value)
        {
            this.value = value;
        }
        public static implicit operator String(string value)
        {
            return new String(value);
        }
        public static implicit operator string(String dslString)
        {
            return dslString.value;
        }
        // Concatenation operator
        public static String operator +(String str1, String str2)
        {
            return new String(str1.value + str2.value);
        }
        // Override ToString() to provide a string representation of the String
        public override string ToString()
        {
            return value;
        }
        bool IEquatable<IDSLType>.Equals(IDSLType? other)
        {
            if (other is String otherString)
            {
                return this.value == otherString.value;
            }
            return false;
        }
    }
}
