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
            ///////////////////

            if(list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    item.Children = list.Where(x=>x.ParentID == item.ID).ToList();
                }
            }

            ///////////////////
            var mainDiv = new TagBuilder("div");
            mainDiv.GenerateId(id);
            mainDiv.MergeAttribute("title", title);

            var ul = new TagBuilder("ul");

            var liHtml = string.Empty;

            foreach (var listItem in list.Where(item=>item.ParentID == 0))
            {
                var li = new TagBuilder("li");
                li.GenerateId(string.Format("li_{0}", listItem.ID));
                
                var liChildHtml = string.Empty; 

                if(listItem.Children.Count > 0)
                {
                    li.AddCssClass("hasChilds");
                    liChildHtml = List(listItem.Children).ToString();
                }

                li.InnerHtml = listItem.Name + liChildHtml;

                liHtml += li;
            }

            ul.InnerHtml = liHtml;

            mainDiv.InnerHtml = ul.ToString();

            return MvcHtmlString.Create(mainDiv.ToString());
        }

        public static MvcHtmlString List(List<HierarchicalItem> list)
        {
            var ul = new TagBuilder("ul");
            ul.MergeAttribute("style", "display:none");

            var liHtml = string.Empty;

            foreach (var listItem in list)
            {
                var li = new TagBuilder("li");
                li.GenerateId(string.Format("li_{0}", listItem.ID));

                var liChildHtml = string.Empty;

                if (listItem.Children.Count > 0)
                {
                    li.AddCssClass("hasChilds");
                    liChildHtml = List(listItem.Children).ToString();
                }

                li.InnerHtml = listItem.Name + liChildHtml;

                liHtml += li;
            }

            ul.InnerHtml = liHtml;

            return MvcHtmlString.Create(ul.ToString());
        }
    }
}