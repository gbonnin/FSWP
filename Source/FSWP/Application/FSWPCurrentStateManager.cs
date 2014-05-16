//
// FSWPCurrentStateManager.cs
// FSWP Library
//
// The class FSWPCurrentStateManager is compatible with Windows Phone 7.1 and above
// This class is used to manage the current state of the application (add, get...)
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
    using Microsoft.Phone.Shell;
    using System;

    public class FSWPCurrentStateManager
    {
        #region Write

        /// <summary>
        /// Set an object in the current state of the application
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="key">Name of the object</param>
        /// <param name="value">Value of the object</param>
        public static void Set<T>(string key, T value)
        {
            if (Contains(key))
                PhoneApplicationService.Current.State[key] = value;
            else
                PhoneApplicationService.Current.State.Add(key, value);
        }

        /// <summary>
        /// Remove an object from the current state of the application
        /// </summary>
        /// <param name="key">Name of the object</param>
        public static void Remove(string key)
        {
            if (Contains(key))
                PhoneApplicationService.Current.State.Remove(key);
        }

        #endregion

        #region Read

        /// <summary>
        /// Is current state contain the object ?
        /// </summary>
        /// <param name="key">Name of the object</param>
        /// <returns></returns>
        public static bool Contains(string key)
        {
            return PhoneApplicationService.Current.State.ContainsKey(key);
        }

        /// <summary>
        /// Get an object from the current state (with conversion to the good type)
        /// </summary>
        /// <typeparam name="T">Type of the object return</typeparam>
        /// <param name="key">Name of the object</param>
        /// <returns>Default value if the object doesn't exist</returns>
        public static T Get<T>(string key)
        {
            if (!Contains(key))
                return default(T);
            return (T)Convert.ChangeType(PhoneApplicationService.Current.State[key], typeof(T), null);
        }

        /// <summary>
        /// Get an object from the current state (without convertion)
        /// </summary>
        /// <param name="key">Name of the object</param>
        /// <returns>Default value is the object doesn't exist</returns>
        public static object Get(string key)
        {
            if (!Contains(key))
                return default(object);
            return PhoneApplicationService.Current.State[key];
        }

        #endregion
    }
}
