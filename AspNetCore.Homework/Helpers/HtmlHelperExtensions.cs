using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace AspNetCore.Homework.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString Image(this IHtmlHelper htmlHelper, int imageId, string text)
        {
            var a = new TagBuilder("a");

            a.MergeAttribute("href", "/images/" + imageId);
            a.InnerHtml.Append(text);
            using (var stringWriter=new StringWriter())
            {
                a.WriteTo(stringWriter, HtmlEncoder.Default);
                return new HtmlString(stringWriter.ToString());
            }            
        }
    }
}