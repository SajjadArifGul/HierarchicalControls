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
            List<HierarchicalItem> list = new List<HierarchicalItem>();

            list.Add(new HierarchicalItem() { Name = "Item 1", ID = 1, ParentID = 0 });
            list.Add(new HierarchicalItem() { Name = "Item 2", ID = 2, ParentID = 1 });
            list.Add(new HierarchicalItem() { Name = "Item 3", ID = 3, ParentID = 1 });
            list.Add(new HierarchicalItem() { Name = "Item 4", ID = 4, ParentID = 1 });
            list.Add(new HierarchicalItem() { Name = "Item 5", ID = 5, ParentID = 1 });
            list.Add(new HierarchicalItem() { Name = "Item 6", ID = 6, ParentID = 5 });
            list.Add(new HierarchicalItem() { Name = "Item 7", ID = 7, ParentID = 5 });
            list.Add(new HierarchicalItem() { Name = "Item 8", ID = 8, ParentID = 5 });
            list.Add(new HierarchicalItem() { Name = "Item 9", ID = 9, ParentID = 5 });
            list.Add(new HierarchicalItem() { Name = "Item 10", ID = 10, ParentID = 9 });

            list.Add(new HierarchicalItem() { Name = "Item 11", ID = 11, ParentID = 0 });
            list.Add(new HierarchicalItem() { Name = "Item 12", ID = 12, ParentID = 11 });

            list.Add(new HierarchicalItem() { Name = "Item 13", ID = 13, ParentID = 0 });
            list.Add(new HierarchicalItem() { Name = "Item 14", ID = 14, ParentID = 0 });

            list.Add(new HierarchicalItem() { Name = "Item 15", ID = 15, ParentID = 0 });
            list.Add(new HierarchicalItem() { Name = "Item 16", ID = 16, ParentID = 15 });
            list.Add(new HierarchicalItem() { Name = "Item 17", ID = 17, ParentID = 15 });

            list.Add(new HierarchicalItem() { Name = "Item 18", ID = 18, ParentID = 0 });
            list.Add(new HierarchicalItem() { Name = "Item 19", ID = 19, ParentID = 18 });
            list.Add(new HierarchicalItem() { Name = "Item 20", ID = 20, ParentID = 18 });
            list.Add(new HierarchicalItem() { Name = "Item 21", ID = 21, ParentID = 18 });
            list.Add(new HierarchicalItem() { Name = "Item 22", ID = 22, ParentID = 21 });
            list.Add(new HierarchicalItem() { Name = "Item 23", ID = 23, ParentID = 22 });
            list.Add(new HierarchicalItem() { Name = "Item 24", ID = 24, ParentID = 23 });
            list.Add(new HierarchicalItem() { Name = "Item 25", ID = 25, ParentID = 23 });

            list.Add(new HierarchicalItem() { Name = "Item 26", ID = 26, ParentID = 0 });

            return View(list);
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


        public ActionResult StudentsList()
        {
            List<Student> list = new List<Student>();

            list.Add(new Student() { Name = "Student 1", ID = 1, ParentID = 0 });
            list.Add(new Student() { Name = "Student 2", ID = 2, ParentID = 1 });
            list.Add(new Student() { Name = "Student 3", ID = 3, ParentID = 1 });
            list.Add(new Student() { Name = "Student 4", ID = 4, ParentID = 1 });
            list.Add(new Student() { Name = "Student 5", ID = 5, ParentID = 1 });
            list.Add(new Student() { Name = "Student 6", ID = 6, ParentID = 5 });
            list.Add(new Student() { Name = "Student 7", ID = 7, ParentID = 5 });
            list.Add(new Student() { Name = "Student 8", ID = 8, ParentID = 5 });
            list.Add(new Student() { Name = "Student 9", ID = 9, ParentID = 5 });
            list.Add(new Student() { Name = "Student 10", ID = 10, ParentID = 9 });

            list.Add(new Student() { Name = "Student 11", ID = 11, ParentID = 0 });
            list.Add(new Student() { Name = "Student 12", ID = 12, ParentID = 11 });

            list.Add(new Student() { Name = "Student 13", ID = 13, ParentID = 0 });
            list.Add(new Student() { Name = "Student 14", ID = 14, ParentID = 0 });

            list.Add(new Student() { Name = "Student 15", ID = 15, ParentID = 0 });
            list.Add(new Student() { Name = "Student 16", ID = 16, ParentID = 15 });
            list.Add(new Student() { Name = "Student 17", ID = 17, ParentID = 15 });

            list.Add(new Student() { Name = "Student 18", ID = 18, ParentID = 0 });
            list.Add(new Student() { Name = "Student 19", ID = 19, ParentID = 18 });
            list.Add(new Student() { Name = "Student 20", ID = 20, ParentID = 18 });
            list.Add(new Student() { Name = "Student 21", ID = 21, ParentID = 18 });
            list.Add(new Student() { Name = "Student 22", ID = 22, ParentID = 21 });
            list.Add(new Student() { Name = "Student 23", ID = 23, ParentID = 22 });
            list.Add(new Student() { Name = "Student 24", ID = 24, ParentID = 23 });
            list.Add(new Student() { Name = "Student 25", ID = 25, ParentID = 23 });

            list.Add(new Student() { Name = "Student 26", ID = 26, ParentID = 0 });

            return View(list);
        }

    }
}