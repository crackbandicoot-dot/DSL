using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.LenguajeTypes
{
    [TypeConverter(typeof(NumberConverter))]
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
        public static Number operator +(Number num1,Number num2)
        {
            return new Number(num1.value+num2.value);  
        }
        public static Number operator -(Number num1,Number num2)
        {
            return new Number(num1.value-num2.value);
        }
        public static Number operator %(Number num1,Number num2)
        {
            return num1.value%num2.value;
        }
        public static Number operator *(Number num1, Number num2)
        {
            return new Number(num1.value * num2.value);
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
        private bool IsInteger()
        {
            return value == (int)value;
        }

        // Existing explicit conversion operators
    
        // Override ToString() to provide a string representation of the Number
        public override string ToString()
        {
            return value.ToString();
        }
    }

    public class NumberConverter : TypeConverter
    {
        // Override CanConvertFrom to return true for string-to-Number conversions
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        // Override ConvertFrom to handle the conversion from a string to a Number
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                string stringValue = (string)value;
                if (int.TryParse(stringValue, out int intValue))
                {
                    return new Number(intValue);
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }

}
