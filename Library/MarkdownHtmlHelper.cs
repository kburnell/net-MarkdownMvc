using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using CommonMark;

namespace MarkdownMvcLibrary {

    /// <summary>
    ///     An HTML Helper that renders markdown.
    /// </summary>
    public static class MarkdownHelper {

        /// <summary>
        ///     Reads the content of the given file path, interprets as markdown and renders the equivalent HTML.
        /// </summary>
        /// <param name="helper">Html helper</param>
        /// <param name="path">The file with the markdown content to be rendered</param>
        /// <returns>The rendered HTML</returns>
        public static IHtmlString Markdown(this HtmlHelper helper, string path) {
            return Markdown(helper, path, null);
        }


        /// <summary>
        ///     Reads the content of the given file path, interprets as markdown and renders the equivalent HTML.
        /// </summary>
        /// <param name="helper">Html helper</param>
        /// <param name="path">The file with the markdown content to be rendered</param>
        /// <param name="dynamicValues">Dictionary of dynamic values to inject</param>
        /// <returns>The rendered HTML</returns>
        public static IHtmlString Markdown(this HtmlHelper helper, string path, Dictionary<string, string> dynamicValues)
        {
            if (helper == null)
            {
                throw new ArgumentNullException(nameof(helper));
            }
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            var view = helper.ViewContext.View as RazorView;
            if (view == null)
            {
                throw new NotARazorViewException();
            }
            var viewPath = helper.ViewContext.HttpContext.Server.MapPath(view.ViewPath);
            var dir = Path.GetDirectoryName(viewPath) ?? string.Empty;
            var fullpath = Path.Combine(dir, path);
            var unparsed = File.ReadAllText(fullpath);
            var dynamicValueInjector = new MarkdownDynamicContentHandler();
            unparsed = dynamicValueInjector.ProcessMarkdown(unparsed, dynamicValues);
            var parsed = CommonMarkConverter.Convert(unparsed);
            return new MvcHtmlString(parsed);
        }

    }

}