using System.Text.RegularExpressions;

namespace WeekFirstConsoleApp.Validators
{
    public class NameValidator
    {
        public bool Validate(string text)
        {
            return char.IsUpper(text[0]);
        }
    }
}
