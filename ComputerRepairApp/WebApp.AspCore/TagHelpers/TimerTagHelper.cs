using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace WebApp.AspCore.TagHelpers
{
    public class TimerTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Content.SetContent($"Current time: {DateTime.Now.ToString("HH:mm:ss")}");
        }
    }
}
