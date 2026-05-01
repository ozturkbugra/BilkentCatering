namespace BilkentCatering.UI.Extensions
{
    public static class StringExtensions
    {
        public static string FormatPhone(this string phone)
        {
            if (string.IsNullOrEmpty(phone)) return "-";
            var digits = new string(phone.Where(char.IsDigit).ToArray());
            if (digits.Length == 11)
                return $"{digits[0]}{digits[1]}{digits[2]}{digits[3]} {digits[4]}{digits[5]}{digits[6]} {digits[7]}{digits[8]} {digits[9]}{digits[10]}";
            return phone;
        }
    }
}