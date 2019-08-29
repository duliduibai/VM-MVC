using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace VM_MVC
{
    public static class HtmlUrlHelperExtensions
    {
        const string StrSpace = "<span>&nbsp;</span>";
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PagingInfo pagingInfo, Func<int, string> pageUrlAccessor)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.PageCount; i++)
            {
                if (1 != i)
                {
                    result.Append(StrSpace);
                }
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrlAccessor(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.PageIndex || (pagingInfo.PageIndex == 0 && i == 1))
                {
                    tag.AddCssClass("selected");
                }
                result.Append(tag.ToString());
            }
            result.Append($"{StrSpace}{StrSpace}<span>(共{pagingInfo.RecordCount}条)</span>");
            return MvcHtmlString.Create(result.ToString());
        }
        public static MvcHtmlString GenreLinks(this UrlHelper helper,
            IEnumerable<string> genres)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var genre in genres)
            {
                sb.Append(String.Format("<span><a href = '{0}'>{1}</a></span>",
                    helper.RouteUrl("GenreHome", new { Genre = genre }), genre));
                sb.Append(StrSpace);
            }
            return MvcHtmlString.Create(sb.ToString().Remove(sb.Length - StrSpace.Length)); 
        }
        public static MvcHtmlString ActorLinks(this UrlHelper helper,
            IEnumerable<string> acotrs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var actor in acotrs)
            {
                sb.Append(String.Format("<span><a href = '{0}'>{1}</a></span>",
                    helper.RouteUrl("ActorHome", new { Actor = actor }), actor));
                sb.Append(StrSpace);
            }
            return MvcHtmlString.Create(sb.ToString().Remove(sb.Length - StrSpace.Length));
        }
    }


    public class PagingInfo
    {
        public static int PageSize => 4;

        public int RecordCount { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get { return (int)Math.Ceiling((decimal)RecordCount / PageSize); } }
    }

}