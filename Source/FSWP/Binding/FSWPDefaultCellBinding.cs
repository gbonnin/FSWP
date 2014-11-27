//
// FSWPDefaultCellBinding.cs
// FSWP Toolkit
//
// The class FSWPDefaultCellBinding is compatible with Windows Phone 7.1 and above
// This class is the default binding of a cell (that's countains title, description and image)
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
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class FSWPDefaultCellBinding
    {

        #region Properties

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
        /// Creates a default cell binding with only title
        /// </summary>
        /// <param name="title">Title of the cell</param>
        /// <param name="backgroundColor">Background color of the cell (optional)</param>
        public FSWPDefaultCellBinding(string title, SolidColorBrush backgroundColor = null)
        {
            Title = title;
            if (backgroundColor != null)
                BackgroundColor = backgroundColor;
            else
                BackgroundColor = new SolidColorBrush(Colors.Transparent);
        }

        /// <summary>
        /// Creates a default cell binding with title, description and image
        /// </summary>
        /// <param name="title">Title of the cell</param>
        /// <param name="description">Description of the cell</param>
        /// <param name="image">Image of the cell</param>
        /// <param name="backgroundColor">Background color of the cell (optional)</param>
        public FSWPDefaultCellBinding(string title, string description, BitmapImage image, SolidColorBrush backgroundColor = null)
        {
            Title = title;
            Description = description;
            Image = image;
            if (backgroundColor != null)
                BackgroundColor = backgroundColor;
            else
                BackgroundColor = new SolidColorBrush(Colors.Transparent);
        }

        /// <summary>
        /// Creates a default cell binding with title, description and image
        /// </summary>
        /// <param name="title">Title of the cell</param>
        /// <param name="description">Description of the cell</param>
        /// <param name="imageUri">Uri of image of the cell</param>
        /// <param name="imageUriKind">Kind of uri of image</param>
        /// <param name="backgroundColor">Background color of the cell (optional)</param>
        public FSWPDefaultCellBinding(string title, string description, string imageUri, UriKind imageUriKind, SolidColorBrush backgroundColor = null)
        {
            Title = title;
            Description = description;
            Image = new BitmapImage(new Uri(imageUri, imageUriKind));
            if (backgroundColor != null)
                BackgroundColor = backgroundColor;
            else
                BackgroundColor = new SolidColorBrush(Colors.Transparent);
        }

        #endregion
    }
}
