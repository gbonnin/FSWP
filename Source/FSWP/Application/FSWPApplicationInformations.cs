//
// FSWPDeviceInformations.cs
// FSWP Toolkit
//
// The class FSWPApplicationInformations is compatible with Windows Phone 7.1 and above
// This class is used to get informations about the current application
// It is a member of the namespace FSWP.Application
// Some functions of this class require that the capacity ID_CAP_IDENTITY_DEVICE is enabled
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
    using System.Xml.Linq;

    public class FSWPApplicationInformations
    {
        #region Properties

        /// <summary>
        /// Name of the application
        /// </summary>
        /// <returns></returns>
        public static string Name
        {
            get { return GetAppManifestValue("Title"); }
        }

        /// <summary>
        /// Version number of the current installed application
        /// </summary>
        /// <returns></returns>
        public static string VersionNumber
        {
            get { return GetAppManifestValue("Version"); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the value of an attribute in application manifest
        /// </summary>
        /// <param name="attributeName">Name of the attribute required</param>
        /// <returns></returns>
        public static string GetAppManifestValue(string attributeName)
        {
            try
            {
                var manifest = XElement.Load("WMAppManifest.xml");
                return manifest.Element("App").Attribute(attributeName).Value;
            }
            catch
            {
                return "";
            }
        }

        #endregion
    }
}
