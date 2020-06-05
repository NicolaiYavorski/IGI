using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.AspCore.TagHelpers
{
    public static class ImageHelper
    {
        public static HtmlString WorkingHelper(this IHtmlHelper html, string source)
        {
            return new HtmlString($"Html helper is working - {source}");
        }
    }
}
