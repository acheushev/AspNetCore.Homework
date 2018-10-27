using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCore.Homework.Helpers
{
    [HtmlTargetElement("image",Attributes = "image-id")]
    public class CategoryImageTagHelper:TagHelper
    {
        public int ImageId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            var address = "/images/" + ImageId;
            output.Attributes.SetAttribute("href", address);
        }
    }
}
