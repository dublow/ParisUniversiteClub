using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace web.extensions
{
    public static class EnumerableExtensions
    {
        public static string Inline<T, U>(this IEnumerable<T> models, Func<T, U> mapper)
        {
            return models
                .Aggregate(
                    new StringBuilder(),
                    (builder, model) => builder.Append($"{mapper(model)},"),
                    builder =>
                    {
                        if (builder.Length > 0)
                            builder.Length--;
                        return builder.ToString();
                    });
        }

        public static void Do<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (var value in values)
            {
                action(value);
            }
        }
    }
}
