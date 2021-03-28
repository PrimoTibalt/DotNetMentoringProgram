using System.Text.RegularExpressions;

namespace WeekFirstConsoleApp.Validators
{
    public class NameValidator
    {
        private string pattern = @"\w+";

        public bool Validate(string text)
        {
            var reg = new Regex(pattern);
            return reg.IsMatch(text) && char.IsUpper(text[0]);
        }
    }
}
