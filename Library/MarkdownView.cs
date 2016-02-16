namespace MarkdownMvc
{
    using CommonMark;
    using MarkdownMvc.Properties;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Web.Mvc;

    /// <summary>
    /// A view that renders markdown.
    /// </summary>
    public class MarkdownView : IView
    {
        private readonly string _filepath;

        private readonly string _cssHref;

        internal MarkdownView(string filepath, string cssHref)
        {
            _filepath = filepath;
            _cssHref = cssHref;
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            // Reading the file content
            var text = File.ReadAllText(_filepath);

            // Page doctype
            var doctype = "<!doctype html>";

            // Page title
            var title = "<title>" + Path.GetFileNameWithoutExtension(_filepath) + "</title>";

            // Page CSS (if any)
            var css = string.IsNullOrWhiteSpace(_cssHref) ?
                string.Format(CultureInfo.InvariantCulture, "<style>\n{0}\n</style>", Resources.Style) :
                string.Format(CultureInfo.InvariantCulture, "<link rel=\"stylesheet\" href=\"{0}\">", _cssHref);

            // Parsing the Markdown content
            var markdown = CommonMarkConverter.Convert(text);

            // Joining all up
            var content = string.Join("\n", doctype, css, title, markdown);

            // Writing to the stream
            writer.Write(content);
        }
    }
}