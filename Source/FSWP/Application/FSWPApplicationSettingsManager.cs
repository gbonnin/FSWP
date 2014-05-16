//
// FSWPApplicationSettingsManager.cs
// FSWP Library
//
// The class FSWPApplicationSettingsManager is compatible with Windows Phone 7.1 and above
// This class is used to manage application settings (add, get...)
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
    using System;
    using System.IO.IsolatedStorage;

    public class FSWPApplicationSettingsManager
    {
        #region Write

        /// <summary>
        /// Set an object in the application settings of the application
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="key">Name of the object</param>
        /// <param name="value">Value of the object</param>
        public static void Set<T>(string key, T value)
        {
            if (Contains(key))
                IsolatedStorageSettings.ApplicationSettings[key] = value;
            else
                IsolatedStorageSettings.ApplicationSettings.Add(key, value);
        }

        /// <summary>
        /// Remove an object from the application settings
        /// </summary>
        /// <param name="key">Name of the object</param>
        public static void Remove(string key)
        {
            if (Contains(key))
                IsolatedStorageSettings.ApplicationSettings.Remove(key);
        }

        /// <summary>
        /// Save application settings (Deprecated)
        /// </summary>
        public static void Save()
        {
                IsolatedStorageSettings.ApplicationSettings.Save();
        }

        #endregion

        #region Read

        /// <summary>
        /// Is app settings contain the object ?
        /// </summary>
        /// <param name="key">Name of the object</param>
        /// <returns></returns>
        public static bool Contains(string key)
        {
            return IsolatedStorageSettings.ApplicationSettings.Contains(key);
        }

        /// <summary>
        /// Get an object from the application settings (with conversion to the good type)
        /// </summary>
        /// <typeparam name="T">Type of the object return</typeparam>
        /// <param name="key">Name of the object</param>
        /// <returns>Default value if the object doesn't exist</returns>
        public static T Get<T>(string key)
        {
            if (!Contains(key))
                return default(T);
            return (T)Convert.ChangeType(IsolatedStorageSettings.ApplicationSettings[key], typeof(T), null);
        }

        /// <summary>
        /// Get an object from the application settings (without convertion)
        /// </summary>
        /// <param name="key">Name of the object</param>
        /// <returns>Default value is the object doesn't exist</returns>
        public static object Get(string key)
        {
            if (!Contains(key))
                return default(object);
            return IsolatedStorageSettings.ApplicationSettings[key];
        }

        #endregion
    }
}
