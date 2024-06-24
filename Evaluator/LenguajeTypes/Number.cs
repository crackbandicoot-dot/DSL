// Ignore Spelling: DSL Lenguaje

namespace DSL.Evaluator.LenguajeTypes
{
    public struct Number : IDSLType
    {
        private double value;

        public Number(int value)
        {
            this.value = value;
        }

        public Number(float value)
        {
            this.value = value;
        }

        public Number(double value)
        {
            this.value = value;
        }

        public static implicit operator Number(int value)
        {
            return new Number(value);
        }

        public static implicit operator Number(float value)
        {
            return new Number(value);
        }

        public static implicit operator Number(double value)
        {
            return new Number(value);
        }

        public static explicit operator int(Number number)
        {
            return (int)number.value;
        }

        public static explicit operator float(Number number)
        {
            return (float)number.value;
        }

        public static explicit operator double(Number number)
        {
            return number.value;
        }
        public static Number operator ^(Number num1, Number num2)
        {
            return Math.Pow(num1.value, num2.value);
        }
        public static Number operator +(Number num1, Number num2)
        {
            return new Number(num1.value + num2.value);
        }
        public static bool operator ==(Number num1, Number num2)
        {
            return num1.Equals(num2);
        }
        public static bool operator !=(Number num1, Number num2)
        {
            return !num1.Equals(num2);
        }
        public static Number operator -(Number num)
        {
            return new Number(-num.value);
        }
        public static Number operator -(Number num1, Number num2)
        {
            return new Number(num1.value - num2.value);
        }
        public static Number operator %(Number num1, Number num2)
        {
            return num1.value % num2.value;
        }
        public static Number operator *(Number num1, Number num2)
        {
            return new Number(num1.value * num2.value);
        }
        public static bool operator <(Number num1, Number num2)
        {
            return num1.value < num2.value;
        }
        public static bool operator <=(Number num1, Number num2)
        {
            return num1.value <= num2.value;
        }
        public static bool operator >=(Number num1, Number num2)
        {
            return num1.value >= num2.value;
        }
        public static bool operator >(Number num1, Number num2)
        {
            return num1.value > num2.value;
        }
        public static Number operator /(Number num1, Number num2)
        {
            // Perform integer division if both numbers are integers
            if (num1.IsInteger() && num2.IsInteger())
            {
                return new Number((int)num1.value / (int)num2.value);
            }
            // Otherwise, perform regular division
            return new Number(num1.value / num2.value);
        }

        // Helper method to check if the stored value is an integer
        public bool IsInteger()
        {
            return value == (int)value;
        }
        public int ToInteger()
        {
            if (IsInteger())
            {
                return (int)value;
            }
            throw new Exception("The current number is not an integer");
        }

        // Existing explicit conversion operators

        // Override ToString() to provide a string representation of the Number
        public override string ToString()
        {
            return value.ToString();
        }

        bool IEquatable<IDSLType>.Equals(IDSLType? other)
        {
            if (other is Number n)
            {
                if (this.IsInteger() && n.IsInteger())
                {
                    return (int)(this.value) == (int)(n.value);
                }
                else
                {
                    return this.value == n.value;
                }
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
