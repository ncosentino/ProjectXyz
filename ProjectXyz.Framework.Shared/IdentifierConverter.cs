using System;
using System.Globalization;

using ProjectXyz.Api.Framework;

namespace ProjectXyz.Shared.Framework
{
    public sealed class IdentifierConverter : IIdentifierConverter
    {
        public IIdentifier Convert(object input)
        {
            if (TryConvert(input, out var output))
            {
                return output;
            }

            throw new NotSupportedException(
                $"Could not convert '{input}' to type '{typeof(IIdentifier)}'.");
        }

        public IIdentifier Convert<T>(T input)
        {
            if (TryConvert(input, out var output))
            {
                return output;
            }

            throw new NotSupportedException(
                $"Could not convert '{input}' to type '{typeof(IIdentifier)}'.");
        }

        public bool TryConvert(object input, out IIdentifier output) =>
            TryConvert(input.GetType(), out output);

        public bool TryConvert<T>(T input, out IIdentifier output)
        {
            if (typeof(IIdentifier).IsAssignableFrom(typeof(T)))
            {
                output = (IIdentifier)input;
                return true;
            }

            if (typeof(string).IsAssignableFrom(typeof(T)))
            {
                var stringInput = input is string
                    ? (string)(object)input
                    : System.Convert.ToString(input, CultureInfo.InvariantCulture);
                output = int.TryParse(stringInput, NumberStyles.Integer, CultureInfo.InvariantCulture, out var numericId)
                    ? new IntIdentifier(numericId)
                    : (IIdentifier)new StringIdentifier(stringInput);
                return true;
            }

            if (typeof(int).IsAssignableFrom(typeof(T)))
            {
                output = new IntIdentifier((int)(object)input);
                return true;
            }

            output = null;
            return false;
        }
    }
}