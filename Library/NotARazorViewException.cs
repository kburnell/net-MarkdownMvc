namespace MarkdownMvc
{
    using System;

    /// <summary>
    /// Exception thrown when a view which isn't a Razor view is used with the helper.
    /// </summary>
    [Serializable]
    public class NotARazorViewException : Exception
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        internal NotARazorViewException() : base("Only Razor views are supported!") { }
    }
}
