using System;
using System.Net.Mail;

namespace EmailAddress
{
    public class EmailAddress : IEquatable<EmailAddress>
    {
        private bool? _isValid = null;

        public string DisplayName { get; }
        public string Address { get; }

        public bool IsValid
        {
            get
            {
                if (!_isValid.HasValue)
                {
                    _isValid = IsAddressValid(Address);
                }

                return _isValid.Value;
            }
        }

        public bool IsNullAddress
        {
            get { return string.IsNullOrEmpty(Address); }
        }

        public EmailAddress()
            : this(null, null)
        {
        }

        public EmailAddress(string emailAddress) : this(null, emailAddress)
        {
        }

        public EmailAddress(string name, string address)
        {
            DisplayName = !string.IsNullOrWhiteSpace(name) ? name : null;
            Address = !string.IsNullOrWhiteSpace(address) ? address : null;
        }


        public MailAddress ToMailAddress()
        {
            if (IsValid && !IsNullAddress)
                return new MailAddress(Address, DisplayName);

            throw new InvalidOperationException(
                "A MailAddress cannot be created from a invalid address or the null address." +
                " Call the IsValid and IsNullAddress properties first to avoid this exception."
            );
        }

        public static bool IsAddressValid(string mailAddress)
        {
            if (string.IsNullOrWhiteSpace(mailAddress))
            {
                return false;
            }

            try
            {
                var address = new MailAddress(mailAddress);

                // Check MailAddress didn't adjust/parse the value to create a valid MailAddress
                return string.Equals(address.Address, mailAddress, StringComparison.InvariantCultureIgnoreCase);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Equals(EmailAddress other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(DisplayName, other.DisplayName, StringComparison.InvariantCultureIgnoreCase) &&
                   string.Equals(Address, other.Address, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EmailAddress)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((DisplayName != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(DisplayName) : 0)*
                        397) ^ (Address != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(Address) : 0);
            }
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(DisplayName))
            {
                return $"{DisplayName} <{Address}>";
            }

            if (!IsNullAddress)
            {
                return Address;
            }

            return "NULL Address <>";
        }
    }
}
