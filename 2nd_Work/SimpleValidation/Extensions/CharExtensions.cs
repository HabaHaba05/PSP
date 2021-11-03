namespace SimpleValidation.Extensions
{
    public static class CharExtensions
    {
        public static bool IsLetter(this char character)
        {
            return (character >= 'A' && character <= 'Z') || (character >= 'a' && character <= 'z');
        }
    }
}
