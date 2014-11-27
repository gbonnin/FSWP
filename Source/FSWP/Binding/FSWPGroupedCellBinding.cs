//
// FSWPGroupedCellBinding.cs
// FSWP Toolkit
//
// The class FSWPGroupedCellBinding is compatible with Windows Phone 7.1 and above
// This class is the binding for a basic cell in grouped list (longListSelector) (cell countains title, description and image)
// It is a member of the namespace FSWP.Binding
//
// Copyright (c) 2014 Guillaume Bonnin
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace FSWP.Binding
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class FSWPGroupedCellBinding
    {
        #region Properties

        /// <summary>
        /// Name of the group of the cell
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Index of the cell
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Image of the cell
        /// </summary>
        public BitmapImage Image { get; set; }

        /// <summary>
        /// Title of the cell
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of the cell
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// BackgroundColor of the cell
        /// </summary>
        public SolidColorBrush BackgroundColor { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a grouped cell binding with only title
        /// </summary>
        /// <param name="groupName">Name or the group of the cell</param>
        /// <param name="index">Index of the cell</param>
        /// <param name="title">Title of the cell</param>
        /// <param name="backgroundColor">Background color of the cell (optional)</param>
        public FSWPGroupedCellBinding(string groupName, int index, string title, SolidColorBrush backgroundColor = null)
        {
            GroupName  = groupName;
            Index = index;
            Title = title;
            if (backgroundColor != null)
                BackgroundColor = backgroundColor;
            else
                BackgroundColor = new SolidColorBrush(Colors.Transparent);
        }

        /// <summary>
        /// Creates a grouped cell binding with title, description and image
        /// </summary>
        /// <param name="groupName">Name or the group of the cell</param>
        /// <param name="index">Index of the cell</param>
        /// <param name="title">Title of the cell</param>
        /// <param name="description">Description of the cell</param>
        /// <param name="image">Image of the cell</param>
        /// <param name="backgroundColor">Background color of the cell (optional)</param>
        public FSWPGroupedCellBinding(string groupName, int index, string title, string description, BitmapImage image, SolidColorBrush backgroundColor = null)
        {
            GroupName = groupName;
            Index = index;
            Title = title;
            Description = description;
            Image = image;
            if (backgroundColor != null)
                BackgroundColor = backgroundColor;
            else
                BackgroundColor = new SolidColorBrush(Colors.Transparent);
        }

        /// <summary>
        /// Creates a grouped cell binding with title, description and image
        /// </summary>
        /// <param name="groupName">Name or the group of the cell</param>
        /// <param name="index">Index of the cell</param>
        /// <param name="title">Title of the cell</param>
        /// <param name="description">Description of the cell</param>
        /// <param name="imageUri">Uri of image of the cell</param>
        /// <param name="imageUriKind">Kind of uri of image</param>
        /// <param name="backgroundColor">Background color of the cell (optional)</param>
        public FSWPGroupedCellBinding(string groupName, int index, string title, string description, string imageUri, UriKind imageUriKind, SolidColorBrush backgroundColor = null)
        {
            GroupName = groupName;
            Index = index;
            Title = title;
            Description = description;
            Image = new BitmapImage(new Uri(imageUri, imageUriKind));
            if (backgroundColor != null)
                BackgroundColor = backgroundColor;
            else
                BackgroundColor = new SolidColorBrush(Colors.Transparent);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Makes a GroupBy of the cells
        /// </summary>
        /// <param name="cells">List of cells</param>
        /// <returns>List of list of cells</returns>
        public static List<List<FSWPGroupedCellBinding>> GroupCell(IEnumerable<FSWPGroupedCellBinding> cells)
        {
            var groups = new List<List<FSWPGroupedCellBinding>>();
            var group = new List<FSWPGroupedCellBinding>();
            string sectionName = "";
            foreach (FSWPGroupedCellBinding cell in cells)
            {
                if (cell.GroupName != sectionName)
                {
                    if (group.Count > 0)
                    {
                        groups.Add(group);
                        group = new List<FSWPGroupedCellBinding>();
                    }
                    sectionName = cell.GroupName;
                }
                group.Add(cell);
            }
            if (group.Count > 0)
                groups.Add(group);

            return groups;
        }

        #endregion
    }
}
