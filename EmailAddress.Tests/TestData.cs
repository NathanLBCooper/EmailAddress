using System.Collections.Generic;

namespace EmailAddress.Tests
{
    public static class TestData
    {
        // https://en.wikipedia.org/wiki/Email_address#Valid_email_addresses

        public static readonly IEnumerable<string> ValidEmails = new List<string>()
        {
            "prettyandsimple@example.com",
            "very.common@example.com",
            "disposable.style.email.with+symbol@example.com",
            "other.email-with-dash@example.com",
            "\"much.more unusual\"@example.com",
            "\"very.unusual.@.unusual.com\"@example.com",
            @"""very.(),:;<>[]\"".VERY.\""very@\\ \""very\"".unusual""@strange.example.com",
            "admin@mailserver1",
            "#!$%&'*+-/=?^_`{}|~@example.org",
            @"""()<>[]:,;@\\\""!#$%&'*+-/=?^_`{}| ~.a""@example.org",
            "\" \"@example.org",
            "example@localhost",
            "example@s.solutions",
            "user@com",
            "user@localserver",
            "user@[IPv6: 2001:db8::1]",
        };

        public static readonly IEnumerable<string> InvalidEmails = new List<string>()
        {
            "Abc.example.com",
            "A@b@c@example.com",
            @"a""b(c)d,e:f;g<h>i[j\k]l@example.com",
            "just\"not\"right@example.com",
            @"this is""not\allowed@example.com",
            @"this\ still\""not\\allowed@example.com",

            // While strictly not valid, MailAddress accepts these.
            //"john..doe@example.com",
            //"john.doe@example..com",

            // Mail Address actually accepts these because it parses the address from this strings
            // That doesn't make them valid email addresses, however
            "   leadingSpace@example.com",
            "trailingSpace@example.com     ",
            "displayname email@address.com",
            "<foo@bar.com>",
        };
    }
}
