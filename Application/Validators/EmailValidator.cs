using System;

namespace Validators
{
    public class EmailValidator
    {
        // Cannot start or end with @
        // Appears only once
        public bool IsValidAtSign(string email)
        {
            throw new NotImplementedException();
        }

        // uppercase and lowercase Latin letters A to Z and a to z
        // digits 0 to 9
        // printable characters !#$%&'*+-/=?^_`{|}~
        // dot., provided that it is not the first or last character and provided also that it does not appear consecutively(e.g., John..Doe @example.com is not allowed)
        // after it @ should follow
        public bool IsValidLocalPart(string email)
        {
            throw new NotImplementedException();
        }

        // uppercase and lowercase Latin letters A to Z and a to z;
        // hyphen -, provided that it is not the first or last character.
        // should appear after @
        public bool IsValidDomain(string email)
        {
            throw new NotImplementedException();
        }

        // uppercase and lowercase Latin letters A to Z and a to z;
        // MUST be at least 2 characters long and MAY be as long as 63 characters
        public bool IsValidTopLevelDomain(string email)
        {
            throw new NotImplementedException();
        }
    }
}
