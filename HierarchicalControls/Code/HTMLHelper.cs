using HierarchicalControls.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HierarchicalControls.Code
{
    public static class HTMLHelper
    {
        public static MvcHtmlString HierarchicalDropDown(this HtmlHelper helper, object obj, string displayProperty = "Name", string idProperty = "ID", string parentIDProperty = "ParentID", string id = "", string title = "", bool manualFunction = false)
        {
            var mainDiv = new TagBuilder("div");
            mainDiv.GenerateId(id);
            mainDiv.MergeAttribute("title", title);
            mainDiv.AddCssClass("hierarchicalDropDown");

            try
            {
                if (obj.GetType().IsGenericType)
                {
                    var dictList = ((IEnumerable<object>)obj).ToDictionaryList();

                    if (dictList != null && dictList.Count > 0)
                    {
                        foreach (var dict in dictList)
                        {
                            dict["Children"] = dictList.Where(x => x.ContainsKey(parentIDProperty) && x[parentIDProperty].Equals(dict[idProperty])).ToList();
                        }

                        var ul = new TagBuilder("ul");
                        ul.MergeAttribute("style", "list-style: none;");
                        var listHtml = string.Empty;

                        foreach (var listItem in dictList.Where(item => !item.ContainsKey(parentIDProperty) || item[parentIDProperty].Equals("") || item[parentIDProperty].Equals(0)))
                        {
                            listHtml += ListItem(listItem, displayProperty, idProperty, manualFunction);
                        }

                        ul.InnerHtml = listHtml;
                        mainDiv.InnerHtml = ul.ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                mainDiv.InnerHtml = ex.Message.ToString();
            }

            return MvcHtmlString.Create(mainDiv.ToString());
        }

        private static MvcHtmlString ListItem(IDictionary<string, object> listItem, string displayProperty, string idProperty, bool manualFunction)
        {
            var li = new TagBuilder("li");
            li.AddCssClass("hOption");
            li.MergeAttribute("data-id", listItem[idProperty].ToString());
            li.MergeAttribute("data-text", listItem[displayProperty].ToString());

            if (manualFunction)
            {
                li.AddCssClass("manual");
            }

            var children = (IList)listItem["Children"];

            if (children != null && children.Count > 0)
            {
                var iconSpan = new TagBuilder("span");
                iconSpan.AddCssClass("hasChilds hClose fas fa-plus-square mr-1");
                iconSpan.InnerHtml = " ";

                var liDiv = new TagBuilder("div");
                liDiv.AddCssClass("item-holder");
                liDiv.InnerHtml = iconSpan + listItem[displayProperty].ToString();

                var childList = new TagBuilder("ul");
                childList.MergeAttribute("style", "list-style: none; display: none;");

                var childListHtml = string.Empty;
                foreach (var item in children)
                {
                    childListHtml += ListItem((IDictionary<string, object>)item, displayProperty, idProperty, manualFunction).ToString();
                }

                childList.InnerHtml = childListHtml;

                li.InnerHtml = string.Format("{0}{1}", liDiv, childList);
            }
            else
            {
                var liDiv = new TagBuilder("div");
                liDiv.AddCssClass("item-holder");
                liDiv.InnerHtml = listItem[displayProperty].ToString();

                li.InnerHtml = string.Format("{0}", liDiv);
            }

            return MvcHtmlString.Create(li.ToString());
        }

        public static MvcHtmlString HierarchicalTreeView(this HtmlHelper helper, object obj, string displayProperty = "Name", string idProperty = "ID", string parentIDProperty = "ParentID", string id = "", string title = "")
        {
            var mainDiv = new TagBuilder("div");
            mainDiv.GenerateId(id);
            mainDiv.MergeAttribute("title", title);
            mainDiv.AddCssClass("hierarchicalTreeView");

            try
            {
                if (obj.GetType().IsGenericType)
                {
                    var dictList = ((IEnumerable<object>)obj).ToDictionaryList();

                    if (dictList != null && dictList.Count > 0)
                    {
                        foreach (var dict in dictList)
                        {
                            dict["Children"] = dictList.Where(x => x.ContainsKey(parentIDProperty) && x[parentIDProperty].Equals(dict[idProperty])).ToList();
                        }

                        var ul = new TagBuilder("ul");
                        var listHtml = string.Empty;

                        foreach (var listItem in dictList.Where(item => !item.ContainsKey(parentIDProperty) || item[parentIDProperty].Equals("") || item[parentIDProperty].Equals(0)))
                        {
                            listHtml += TreeViewListItem(listItem, displayProperty, idProperty);
                        }

                        ul.InnerHtml = listHtml;
                        mainDiv.InnerHtml = ul.ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                mainDiv.InnerHtml = ex.Message.ToString();
            }

            return MvcHtmlString.Create(mainDiv.ToString());
        }

        private static MvcHtmlString TreeViewListItem(IDictionary<string, object> listItem, string displayProperty, string idProperty)
        {
            var li = new TagBuilder("li");
            li.AddCssClass("hOption");
            li.MergeAttribute("data-id", listItem[idProperty].ToString());
            li.MergeAttribute("data-text", listItem[displayProperty].ToString());

            var children = (IList)listItem["Children"];

            var iconSpan = new TagBuilder("span");
            iconSpan.AddCssClass("check-holder");
            iconSpan.InnerHtml = " ";

            var liDiv = new TagBuilder("div");
            liDiv.AddCssClass("item-holder");
            liDiv.InnerHtml = iconSpan + listItem[displayProperty].ToString();

            if (children != null && children.Count > 0)
            {
                var childList = new TagBuilder("ul");

                var childListHtml = string.Empty;
                foreach (var item in children)
                {
                    childListHtml += TreeViewListItem((IDictionary<string, object>)item, displayProperty, idProperty).ToString();
                }

                childList.InnerHtml = childListHtml;

                li.InnerHtml = string.Format("{0}{1}", liDiv, childList);
            }
            else
            {
                li.InnerHtml = string.Format("{0}", liDiv);
            }

            return MvcHtmlString.Create(li.ToString());
        }

        public static MvcHtmlString HierarchicalAccordions(this HtmlHelper helper, object obj, string displayProperty = "Name", string idProperty = "ID", string parentIDProperty = "ParentID", string id = "", string title = "", string selectedItemID = "")
        {
            var mainDiv = new TagBuilder("div");
            mainDiv.GenerateId(id);
            mainDiv.MergeAttribute("title", title);
            mainDiv.AddCssClass("hierarchicalAccordions");

            try
            {
                if (obj.GetType().IsGenericType)
                {
                    var dictList = ((IEnumerable<object>)obj).ToDictionaryList();

                    if (dictList != null && dictList.Count > 0)
                    {
                        foreach (var dict in dictList)
                        {
                            dict["Children"] = dictList.Where(x => x.ContainsKey(parentIDProperty) && x[parentIDProperty].Equals(dict[idProperty])).ToList();
                        }

                        var accordionsHtml = string.Empty;

                        var i = 0;
                        foreach (var listItem in dictList.Where(item => !item.ContainsKey(parentIDProperty) || item[parentIDProperty].Equals("") || item[parentIDProperty].Equals(0)))
                        {
                            var isFirstItem = i == 0;

                            var accordionDiv = new TagBuilder("div");
                            accordionDiv.AddCssClass("accordion");

                            var cardDiv = new TagBuilder("div");
                            cardDiv.AddCssClass("card");

                            if (isFirstItem)
                            {
                                cardDiv.AddCssClass("open");
                            }

                            var cardHeaderDiv = new TagBuilder("div");
                            cardHeaderDiv.AddCssClass("card-header");
                            cardHeaderDiv.InnerHtml = string.Format("<h5>{0}</h5>", listItem[displayProperty].ToString());

                            var dataHolderDiv = new TagBuilder("div");
                            dataHolderDiv.AddCssClass("data-holder bg--light-gray");

                            if (!isFirstItem)
                            {
                                dataHolderDiv.MergeAttribute("style", "display:none");
                            }

                            dataHolderDiv.InnerHtml = AccordionListItem(listItem, displayProperty, idProperty, selectedItemID).ToString();

                            cardDiv.InnerHtml = string.Format("{0}{1}", cardHeaderDiv, dataHolderDiv);

                            accordionDiv.InnerHtml = cardDiv.ToString();

                            accordionsHtml += accordionDiv.ToString();

                            i++;
                        }

                        mainDiv.InnerHtml = accordionsHtml;
                    }
                }
            }
            catch (Exception ex)
            {
                mainDiv.InnerHtml = ex.Message.ToString();
            }

            return MvcHtmlString.Create(mainDiv.ToString());
        }

        private static MvcHtmlString AccordionListItem(IDictionary<string, object> listItem, string displayProperty, string idProperty, string selectedItemID)
        {
            var ul = new TagBuilder("ul");
            ul.AddCssClass("accordionList");

            var li = new TagBuilder("li");
            li.MergeAttribute("data-id", listItem[idProperty].ToString());

            if (listItem[idProperty].ToString() == selectedItemID)
            {
                li.AddCssClass("am-here");
            }

            var children = (IList)listItem["Children"];
            if (children != null && children.Count > 0)
            {
                var childList = new TagBuilder("ul");

                var childListHtml = string.Empty;
                foreach (var item in children)
                {
                    childListHtml += AccordionListItem((IDictionary<string, object>)item, displayProperty, idProperty, selectedItemID).ToString();
                }

                childList.InnerHtml = childListHtml;

                li.InnerHtml = string.Format("{0}{1}", listItem[displayProperty].ToString(), childList);
            }
            else
            {
                li.InnerHtml = string.Format("{0}", listItem[displayProperty].ToString());
            }

            ul.InnerHtml = li.ToString();

            return MvcHtmlString.Create(ul.ToString());
        }
    }
}