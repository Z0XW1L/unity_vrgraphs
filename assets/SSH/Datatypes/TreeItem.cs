/** 
* (c) 2022 Z0XW1L
* This code is licensed under MIT license (see LICENSE file for details) 
*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.SSH
{
    /// <summary>
    /// This class represents a file or folder, e.g. as returned by the 'tree' command.
    /// </summary>
    [JsonObject]
    public class TreeItem
    {
        #region Private attributes

        #endregion

        #region Public attributes
        public string Type { get; set; }
        public string Name { get; set; }
        public string Target { get; set; }
        public List<TreeItem> Contents { get; set; } = new List<TreeItem>();
        public TreeItem Parent { set; get; }
        #endregion

        #region Constructor

        #endregion

        #region Public members
        /// <summary>
        /// Adds additional attributes after deserialization.
        /// </summary>
        /// <param name="items"></param>
        public static void PostProcessTreeItems(List<TreeItem> items)
        {
            List<TreeItem> nodes = new List<TreeItem>(items);
            while(nodes.Count > 0)
            {
                TreeItem item = nodes[0];
                nodes.RemoveAt(0);
                foreach(TreeItem child in item.Contents)
                {
                    child.Parent = item;
                }
                nodes.AddRange(item.Contents);
            }
        }
        /// <summary>
        /// Adds additional attributes after deserialization.
        /// </summary>
        /// <param name="items"></param>
        public static void PostProcessTreeItems(TreeItem item)
        {
            item.Contents.ForEach(it => it.Parent = item);
            PostProcessTreeItems(item.Contents);
        }
        #endregion

        #region Private members

        #endregion
        }
}
