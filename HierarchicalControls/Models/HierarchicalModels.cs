using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HierarchicalControls.Models
{

    public class TreeViewModel
    {
        public List<TreeViewHierarchicalData> TableHierarchicalData { get; set; }
        public string Title { get; set; }
        public string TableName { get; set; }
        public string PrimaryKey { get; set; }
        public bool MultiSelect { get; set; }
    }

    public class TreeViewHierarchicalData
    {
        public TreeViewHierarchicalData()
        {
            this.Children = new List<TreeViewHierarchicalData>();
        }

        public Dictionary<string, object> TableData { get; set; }
        public List<TreeViewHierarchicalData> Children { get; set; }
    }
}