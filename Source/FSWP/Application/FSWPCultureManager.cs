//
// FSWPCultureManager.cs
// FSWP Library
//
// The class FSWPCultureManager is compatible with Windows Phone 7.1 and above
// This class is used to manage culture and language of the application
// It is a member of the namespace FSWP.Application
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

namespace FSWP.Application
{
    using System.Globalization;
    using System.Threading;

    public class FSWPCultureManager
    {
        /// <summary>
        /// Return the name of the current culture
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentCultureName()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }

        /// <summary>
        /// Change the current culture of the application
        /// </summary>
        /// <param name="cultureName">The name of the new culture</param>
        /// <returns>True if success, else false</returns>
        public static bool ChangeCurrentCulture(string cultureName)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
