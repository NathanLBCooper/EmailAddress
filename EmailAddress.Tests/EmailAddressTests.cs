using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace EmailAddress.Tests
{
    [TestFixture]
    public class EmailAddressTests
    {
        [Test]
        [TestCaseSource(nameof(ValidEmails))]
        public void ValidEmailsAreValid(string emailStr)
        {
            var emailAddress = new EmailAddress(emailStr);
            emailAddress.IsValid.Should().BeTrue();
        }

        [Test]
        [TestCaseSource(nameof(InvalidEmails))]
        public void InvalidEmailsAreInvalid(string emailStr)
        {
            var emailAddress = new EmailAddress(emailStr);
            emailAddress.IsValid.Should().BeFalse();
        }

        [TestCase("foo", "")]
        [TestCase("bar", null)]
        [TestCase("fiz", "   ")]
        public void NullAddressCanBeCreatedWithParameters(string name, string address)
        {
            var nullAddress = new EmailAddress(string.Empty, string.Empty);

            nullAddress.IsNullAddress.Should().BeTrue();
            nullAddress.IsValid.Should().BeFalse();
        }

        [Test]
        public void NullAddressCanBeCreatedWithoutParameters()
        {
            var nullAddress = new EmailAddress();

            nullAddress.IsNullAddress.Should().BeTrue();
            nullAddress.IsValid.Should().BeFalse();
        }

        [Test]
        public void NullAddressCannotBeTransformedIntoMailAddress()
        {
            var nullAddress = new EmailAddress();

            Action createMailAddressFromNullAddress = () => nullAddress.ToMailAddress();

            createMailAddressFromNullAddress.ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public void ValidAddressCanBeTransformedIntoMailAddress()
        {
            var address = new EmailAddress("foo", TestData.ValidEmails.First());
            var mailAddress = address.ToMailAddress();

            mailAddress.Address.Should().Be(address.Address);
            mailAddress.DisplayName.Should().Be(address.DisplayName);
        }

        [Test]
        public void InvalidAddressCannotBeTransformedIntoMailAddress()
        {
            var address = new EmailAddress("foo", TestData.InvalidEmails.First());
            Action createMailAddressFromInvalidAddress = () => address.ToMailAddress();

            createMailAddressFromInvalidAddress.ShouldThrow<InvalidOperationException>();
        }

        private static IEnumerable<string> ValidEmails
        {
            get { return TestData.ValidEmails; }
        }

        private static IEnumerable<string> InvalidEmails
        {
            get { return TestData.InvalidEmails; }
        }
    }
}