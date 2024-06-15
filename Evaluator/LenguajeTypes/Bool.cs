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
        [TypeConverter(typeof(BooleanConverter))]
        public struct Bool : IDSLType
        {
            private bool value;

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
                throw new NotImplementedException();
            }
        }

        [TypeConverter(typeof(BooleanConverter))]
        public class BooleanConverter : TypeConverter
        {
            // Override CanConvertFrom to return true for string-to-Boolean conversions
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                {
                    return true;
                }
                return base.CanConvertFrom(context, sourceType);
            }

            // Override ConvertFrom to handle the conversion from a string to a Boolean
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (value is string stringValue)
                {
                    if (bool.TryParse(stringValue, out bool boolValue))
                    {
                        return new Bool(boolValue);
                    }
                    throw new ArgumentException("The input string is not a valid boolean.");
                }
                return base.ConvertFrom(context, culture, value);
            }
        }
    }

}
