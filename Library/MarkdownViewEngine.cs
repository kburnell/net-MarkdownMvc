namespace MarkdownMvcLibrary
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;

    /// <summary>
    /// A view engine for markdown views.
    /// </summary>
    public class MarkdownViewEngine : IViewEngine
    {
        private const string _filepathFormat = "~/Views/{0}/{1}.md";

        private readonly string _cssHref;

        /// <summary>
        /// Ctor.
        /// </summary>
        public MarkdownViewEngine() : this(null) { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="cssHref">Href attribute value of the CSS in the generated HTML</param>
        public MarkdownViewEngine(string cssHref)
        {
            _cssHref = cssHref;
        }

        public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName,
            bool useCache)
        {
            return new ViewEngineResult(Enumerable.Empty<string>());
        }

        public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName,
            bool useCache)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");

            if (viewName == null)
                throw new ArgumentNullException("viewName");

            // Getting the controller name
            var controller = controllerContext.RouteData.Values["controller"].ToString();

            // Getting the relative path (~/View/Controller/Action.md)
            var filepath = string.Format(CultureInfo.InvariantCulture, _filepathFormat, controller, viewName);

            // Getting the actual file system path of the file (C:\...)
            var completeFilepath = controllerContext.HttpContext.Server.MapPath(filepath);

            if (File.Exists(completeFilepath))
            {
                // If the file exists we create our view and return it
                var view = new MarkdownView(completeFilepath, _cssHref);
                return new ViewEngineResult(view, this);
            }
            else
            {
                // If not we just tell that we searched for it
                return new ViewEngineResult(new string[] { filepath });
            }
        }

        public void ReleaseView(ControllerContext controllerContext, IView view) { }
    }
}