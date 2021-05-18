using System;
using System.Linq;

namespace Cowin.Watch.Core
{
    public class Pincode
    {
        private readonly string pincode;

        private Pincode(string pincode)
        {
            this.pincode = pincode;
        }

        public override string ToString() => pincode;

        public static Pincode FromString(string pincode)
        {
            ValidatePincode(pincode);
            return new Pincode(pincode);
        }

        private static void ValidatePincode(string pincode)
        {
            if (String.IsNullOrWhiteSpace(pincode)) {
                throw new ArgumentException($"'{nameof(pincode)}' cannot be null or whitespace", nameof(pincode));
            }
            if (pincode.Length != 6 || pincode.Any(digit => !Char.IsDigit(digit))) {
                throw new ArgumentException($"'{nameof(pincode)}' should be 6 numerical digits.", nameof(pincode));
            }
        }
    }
}