using HierarchicalControls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HierarchicalControls.Code
{
    public static class HTMLHelper
    {
        public static MvcHtmlString HierarchicalDropDown(this HtmlHelper helper, string id, string title, List<HierarchicalItem> list)
        {
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    item.Children = list.Where(x => x.ParentID == item.ID).ToList();
                }
            }
            
            var mainDiv = new TagBuilder("div");
            mainDiv.GenerateId(id);
            mainDiv.MergeAttribute("title", title);
            mainDiv.AddCssClass("hierarchicalDropDown");

            var ul = new TagBuilder("ul");
            ul.MergeAttribute("style", "list-style: none;");
            var listHtml = string.Empty;

            foreach (var listItem in list.Where(item => item.ParentID == 0))
            {
                listHtml += ListItem(listItem);
            }

            ul.InnerHtml = listHtml;
            mainDiv.InnerHtml = ul.ToString();

            return MvcHtmlString.Create(mainDiv.ToString());
        }

        private static MvcHtmlString ListItem(HierarchicalItem listItem)
        {
            var li = new TagBuilder("li");
            li.MergeAttribute("data-id", listItem.ID.ToString());
            
            if (listItem.Children != null && listItem.Children.Count > 0)
            {
                var iconSpan = new TagBuilder("span");
                iconSpan.AddCssClass("badge badge-secondary hasChilds hClose fas fa-plus-square mr-1");
                iconSpan.InnerHtml = " ";

                var childList = new TagBuilder("ul");
                childList.MergeAttribute("style", "list-style: none; display: none;");

                var childListHtml = string.Empty;
                foreach (var item in listItem.Children)
                {
                    childListHtml += ListItem(item).ToString();
                }

                childList.InnerHtml = childListHtml;

                li.InnerHtml = iconSpan + listItem.Name + childList;
            }
            else
            {
                var iconSpan = new TagBuilder("span");
                iconSpan.AddCssClass("fas fa-minus mr-1");
                iconSpan.InnerHtml = " ";

                li.InnerHtml = iconSpan + listItem.Name;
            }
            
            return MvcHtmlString.Create(li.ToString());
        }
    }
}