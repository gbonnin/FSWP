//
// FSWPGroupedCellsToGroupName.cs
// FSWP Library
//
// The class FSWPGroupedCellsToGroupName is compatible with Windows Phone 7.1 and above
// This class converts a list of GroupedCellBinding to a group name
// It is a member of the namespace FSWP.Binding.Converters (The FSWPConverters are used to convert values for binding)
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

namespace FSWP.Binding.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Data;

    public class FSWPGroupedCellsToGroupName : IValueConverter
    {
        /// <summary>
        /// Convert the groupedCell to the groupName
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cells = (IEnumerable<FSWPGroupedCellBinding>)value;
            var enumerator = cells.GetEnumerator();
            if (!enumerator.MoveNext())
                return "";
            var cell = enumerator.Current;
            return cell.GroupName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
