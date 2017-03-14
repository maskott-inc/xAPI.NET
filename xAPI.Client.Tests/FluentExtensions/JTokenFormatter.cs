using FluentAssertions.Formatting;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace xAPI.Client.Tests.FluentExtensions
{
    public class JTokenFormatter : IValueFormatter
    {
        bool IValueFormatter.CanHandle(object value)
        {
            return (value is JToken);
        }

        string IValueFormatter.ToString(object value, bool useLineBreaks, IList<object> processedObjects, int nestedPropertyLevel)
        {
            return value.ToString();
        }
    }
}
