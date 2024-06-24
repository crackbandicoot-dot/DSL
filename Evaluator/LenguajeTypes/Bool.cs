// Ignore Spelling: DSL Lenguaje

namespace DSL.Evaluator.LenguajeTypes
{
    using System;
    namespace DSL.Evaluator.LenguajeTypes
    {
        public readonly struct Bool : IDSLType
        {
            private readonly bool value;
            public Bool(bool value)
            {
                this.value = value;
            }
            public static implicit operator Bool(bool value)
            {
                return new Bool(value);
            }
            public static implicit operator bool(Bool boolean)
            {
                return boolean.value;
            }
            // Logical operators
            public static Bool operator !(Bool boolean)
            {
                return new Bool(!boolean.value);
            }
            public static Bool operator &(Bool bool1, Bool bool2)
            {
                return new Bool(bool1.value & bool2.value);
            }
            public static Bool operator |(Bool bool1, Bool bool2)
            {
                return new Bool(bool1.value | bool2.value);
            }

            // Override ToString() to provide a string representation of the Boolean
            public override string ToString()
            {
                return value.ToString();
            }
            bool IEquatable<IDSLType>.Equals(IDSLType? other)
            {
                if (other is Bool b)
                {
                    return b.value == value;
                }
                return false;
            }
        }
    }
}