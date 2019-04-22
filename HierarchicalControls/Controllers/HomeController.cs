using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HierarchicalControls.Models;
using HierarchicalControls.Code;

namespace HierarchicalControls.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult TreeViewControl(string tableName, string title, string primaryKey, string parentKey, bool multiSelect = false)
        {
            TreeViewModel model = new TreeViewModel();
            model.Title = title;
            List<Dictionary<string, object>> tableData = GetTableData(tableName);
            model.TableHierarchicalData = GetTableHierarchicalData(tableData, primaryKey, parentKey, null);
            model.TableName = tableName;
            model.PrimaryKey = primaryKey;
            model.MultiSelect = multiSelect;
            return PartialView(model);
        }
        private List<TreeViewHierarchicalData> GetTableHierarchicalData(List<Dictionary<string, object>> tableData, string primaryKey, string parentKey, object parentID)
        {
            List<TreeViewHierarchicalData> retVal = new List<TreeViewHierarchicalData>();
            foreach (Dictionary<string, object> item in tableData.Where(x => (x[parentKey] != null && x[parentKey].ToString() != "" && parentID != null) ? int.Parse(x[parentKey].ToString()) == int.Parse(parentID.ToString()) : x[parentKey] == parentID))
            {
                TreeViewHierarchicalData model = new TreeViewHierarchicalData();
                model.TableData = item;
                model.Children = GetTableHierarchicalData(tableData, primaryKey, parentKey, item[primaryKey].GetValue());
                retVal.Add(model);
            }
            return retVal;
        }

        public List<Dictionary<string, object>> GetTableData(string tableName)
        {
            dynamic data = new object();
            List<Dictionary<string, object>> finalList = new List<Dictionary<string, object>>();

            foreach (var row in data)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (var t in row)
                {
                    if (t.Value == null)
                    {
                        dic.Add(t.Key, (int?)null);
                    }
                    else
                    {
                        dic.Add(t.Key, t.Value);
                    }
                }
                finalList.Add(dic);
            }
            return finalList;
        }
    }
}