using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkdownMvcLibrary {

    public class MarkdownDynamicValueInjector {

        public string InjectDynamicValues(string unparsedMarkdown, Dictionary<string, string> dynamicValues, string delimeter) {
            if (string.IsNullOrWhiteSpace(unparsedMarkdown) || string.IsNullOrWhiteSpace(delimeter) || dynamicValues == null || dynamicValues.Count == 0) {
                return unparsedMarkdown;
            }
            var delimeterLength = delimeter.Length;
            var startPos = unparsedMarkdown.IndexOf(delimeter, StringComparison.CurrentCulture);
            while (startPos != -1) {
                var endPos = unparsedMarkdown.IndexOf(delimeter, startPos + delimeterLength, StringComparison.CurrentCulture);
                if (endPos != -1) {
                    endPos += delimeterLength;
                    var keyWithDelimeters = unparsedMarkdown.Substring(startPos, (endPos - startPos));
                    var key = keyWithDelimeters.Replace(delimeter, string.Empty);
                    var dynamicValue = dynamicValues.FirstOrDefault(dv => dv.Key.Equals(key, StringComparison.CurrentCultureIgnoreCase)).Value;
                    unparsedMarkdown = unparsedMarkdown.Replace(keyWithDelimeters, dynamicValue);
                }
                startPos = unparsedMarkdown.IndexOf(delimeter, StringComparison.CurrentCulture);
            }
            return unparsedMarkdown;
        }

    }

}