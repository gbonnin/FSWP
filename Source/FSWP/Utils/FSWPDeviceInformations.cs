//
// FSWPDeviceInformations.cs
// FSWP Toolkit
//
// The class FSWPDeviceInformations is compatible with Windows Phone 7.1 and above
// This class is used to get informations about device
// It is a member of the namespace FSWP.Utils
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

namespace FSWP.Utils
{
    using Microsoft.Phone.Info;
    using System;
    using System.Windows;

    public class FSWPDeviceInformations
    {
        /// <summary>
        /// Unique id of the device
        /// </summary>
        /// <returns></returns>
        public static string UniqueId
        {
            get {
                byte[] deviceId = (byte[])DeviceExtendedProperties.GetValue("DeviceUniqueId");
                return Convert.ToBase64String(deviceId);
            }
        }

        /// <summary>
        /// Current OS version installed on the device
        /// </summary>
        public static string OSVersion
        {
            get { return Environment.OSVersion.ToString(); }
        }

        /// <summary>
        /// Height screen of the device
        /// </summary>
        /// <returns></returns>
        public static double HeightScreen
        {
            get { return Application.Current.Host.Content.ActualHeight; }
        }

        /// <summary>
        /// Width screen of the device
        /// </summary>
        /// <returns></returns>
        public static double WidthScreen
        {
            get { return Application.Current.Host.Content.ActualWidth; }
        }

        /// <summary>
        /// Device has a 720p (or 1080p) resolution ?
        /// </summary>
        /// <returns></returns>
        public static bool Is720P
        {
            get { return (HeightScreen > 800); }
        }
    }
}
