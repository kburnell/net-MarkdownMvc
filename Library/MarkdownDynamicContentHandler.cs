using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkdownMvcLibrary {

    public class MarkdownDynamicContentHandler {

        private const string DynamicDelimeter = "||";
        private const int DynamicDelimeterLength = 2;
        private const string ConditionalSequenceStart = "||?";
        private const string PositiveConditionalSequence = "||??";
        private const string NegativeConditionalSequence = "||?!";
        private const int ConditionalSequenceLength = 4;

        public string ProcessMarkdown(string unparsedMarkdown, Dictionary<string, string> dynamicValues) {
            if (string.IsNullOrWhiteSpace(unparsedMarkdown) || dynamicValues == null || dynamicValues.Count == 0) {
                return unparsedMarkdown;
            }
            var processedMarkdown = unparsedMarkdown;
            var startPos = processedMarkdown.IndexOf(DynamicDelimeter, StringComparison.CurrentCulture);
            while (startPos != -1) {
                var endPos = processedMarkdown.IndexOf(DynamicDelimeter, startPos + DynamicDelimeterLength, StringComparison.CurrentCulture);
                if (endPos != -1) {
                    endPos += DynamicDelimeterLength;
                    var keyWithDelimeters = processedMarkdown.Substring(startPos, (endPos - startPos));
                    //We currently have a piece of dynamic content '||*******||'
                    //Determine what type of dynamic content we are dealing with
                    if (keyWithDelimeters.StartsWith(ConditionalSequenceStart)) {
                        //We have the start of conditional content '||?'
                        var keyWithDelimetersLength = keyWithDelimeters.Length;
                        string conditionalContentWithDelimeters = null;
                        //Get the entire conditional statement including delimeters
                        var conditionalEndPos = processedMarkdown.IndexOf(keyWithDelimeters, startPos + keyWithDelimetersLength);
                        if (conditionalEndPos != -1) {
                            conditionalContentWithDelimeters = processedMarkdown.Substring(startPos, (conditionalEndPos + keyWithDelimetersLength) - startPos);
                        }
                        if (!string.IsNullOrEmpty(conditionalContentWithDelimeters)) {
                            //Get the dynamic conditional to check
                            var key = keyWithDelimeters.Substring(ConditionalSequenceLength, (keyWithDelimetersLength - (ConditionalSequenceLength*2)));
                            var conditionalValue = bool.Parse(dynamicValues.FirstOrDefault(dv => dv.Key.Equals(key, StringComparison.CurrentCultureIgnoreCase)).Value);
                            //Determine if negated conditional
                            var isNegatedConditional = keyWithDelimeters.StartsWith(NegativeConditionalSequence);
                            var shouldShow = (!isNegatedConditional && conditionalValue) || (isNegatedConditional && !conditionalValue);
                            if (shouldShow) {
                                //Should show so trim off the delimeters from the conditional content and replace the delimited content with un-delimited content in the markdown
                                var conditionalContent = conditionalContentWithDelimeters.Substring(keyWithDelimetersLength, conditionalContentWithDelimeters.Length - (keyWithDelimetersLength*2));
                                processedMarkdown = processedMarkdown.Replace(conditionalContentWithDelimeters, conditionalContent);
                            }
                            else {
                                //Should not show so completely remove the conditional content
                                processedMarkdown = processedMarkdown.Replace(conditionalContentWithDelimeters, string.Empty);
                            }
                        }
                    }
                    else {
                        //We have basic dynamic variable that needs to be replaced
                        var key = keyWithDelimeters.Replace(DynamicDelimeter, string.Empty);
                        var dynamicValue = dynamicValues.FirstOrDefault(dv => dv.Key.Equals(key, StringComparison.CurrentCultureIgnoreCase)).Value;
                        processedMarkdown = processedMarkdown.Replace(keyWithDelimeters, dynamicValue);
                    }
                    
                }
                startPos = processedMarkdown.IndexOf(DynamicDelimeter, StringComparison.CurrentCulture);
            }
            return processedMarkdown;
        }

    }

}