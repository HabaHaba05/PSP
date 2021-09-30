using SimpleValidation.Extensions;
using SimpleValidation.Interfaces;
using System.Linq;

namespace SimpleValidation
{
    public class EmailValidator : IEmailValidator
    {
        private static readonly string AllowedCharsInLocalPart = "!#$%&'*+-/=?^_`{|}~.";
        private static readonly string AllowedChars = AllowedCharsInLocalPart + "\"@";

        public bool Validate(string email)
        {
            email = email.Trim();

            return ContainsOnlyOneAtSymbol(email) 
                && !ContainsInvalidSymbols(email)
                && ValidDomainAndTLD(email) 
                && ValidLocalPart(email);
        }

        private static bool ContainsOnlyOneAtSymbol(string email)
        {
            return email.Where(x => x == '@').Count() == 1;
        }

        private static bool ContainsInvalidSymbols(string email)
        {
            foreach (var character in email)
            {
                if ((!character.IsLetter() || char.IsDigit(character)) && !AllowedChars.Contains(character))
                {
                    return true;
                }
            }

            return false;
        }

        // uppercase and lowercase Latin letters A to Z and a to z
        // digits 0 to 9
        // printable characters !#$%&'*+-/=?^_`{|}~.
        // dot., provided that it is not the first or last character and provided also that it does not appear consecutively(e.g., John..Doe @example.com is not allowed)
        private static bool ValidLocalPart(string email)
        {
            var localPart = email.Substring(0, email.IndexOf('@'));

            if (string.IsNullOrEmpty(localPart))
            {
                return false;
            }

            if (localPart.First() == '"' && localPart.Last() == '"')
            {
                return true;
            }

            if (localPart.First() == '.' || localPart.Last() == '.' || localPart.Contains(".."))
            {
                return false;
            }

            foreach(var character in localPart)
            {
                if (!char.IsLetterOrDigit(character))
                {
                    if (!AllowedCharsInLocalPart.Contains(character))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // Domain rules:
        //      uppercase and lowercase Latin letters A to Z and a to z;
        //      hyphen -, provided that it is not the first or last character.
        // TLD rules:
        //      uppercase and lowercase Latin letters A to Z and a to z;
        //      MUST be at least 2 characters long and MAY be as long as 63 characters
        private static bool ValidDomainAndTLD(string email)
        {
            var allDomain = email.Remove(0, email.IndexOf('@') + 1);
            var positionOfDot = allDomain.IndexOf('.');

            if(positionOfDot < 1)
            {
                return false;
            }

            var domain = allDomain.Substring(0,positionOfDot);
            var tld = allDomain.Substring(positionOfDot+1,allDomain.Length - positionOfDot -1);

            // Domain
            foreach (var character in domain)
            {
                if (!char.IsLetter(character) && character != '-')
                {
                    return false;
                }
            }

            if(domain.First() == '-' || domain.Last() == '-')
            {
                return false;
            }

            // TLD
            foreach (var character in tld)
            {
                if (!char.IsLetter(character))
                {
                    return false;
                }
            }

            if(tld.Length < 2 || tld.Length > 63)
            {
                return false;
            }


            return true;
        }

    }
}