using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace web.extensions
{
    public static class StringExtensions
    {
        public static string NormalizeName(this string value)
        {
            if (!value.Contains('-'))
                return InnerNormalizeName(value);

            return value
                .Split(new[] { '-' })
                .Select(x => InnerNormalizeName(value))
                .Aggregate(
                    new StringBuilder(),
                    (acc, item) => acc.Append($"{item}-"),
                    acc =>
                    {
                        if (acc.Length > 0)
                            acc.Length--;

                        return acc.ToString();
                    });
        }

        public static DateTime ToDateTime(this string value)
        {
            return DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public static string ToYesNo(this bool value)
        {
            return value ? "Oui" : "Non";
        }

        private static string InnerNormalizeName(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return value.Length == 1 
                ? value.ToUpper() 
                : $"{value[0].ToString().ToUpper()}{value.Substring(1, value.Length - 1).ToLower()}";
        }
    }
}
