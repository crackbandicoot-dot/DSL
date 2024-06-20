using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Evaluator.LenguajeTypes
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    namespace DSL.Evaluator.LenguajeTypes
    {
        [TypeConverter(typeof(StringConverter))]
        public struct String : IDSLType
        {
            private string value;

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

        [TypeConverter(typeof(StringConverter))]
        public class StringConverter : TypeConverter
        {
            // Override CanConvertFrom to return true for string-to-String conversions
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                {
                    return true;
                }
                return base.CanConvertFrom(context, sourceType);
            }

            // Override ConvertFrom to handle the conversion from a string to a String
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (value is string stringValue)
                {
                    return new String(stringValue);
                }
                return base.ConvertFrom(context, culture, value);
            }
        }
    }

}
