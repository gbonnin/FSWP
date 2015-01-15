//
// FSWPWebRequest.cs
// FSWP Toolkit
//
// The class WebRequestCallback is compatible with Windows Phone 7.1 and above
// It is an abstraction of the HttpWebRequest class
// It is a member of the namespace FSWP.Net
// This class requires that the capacity ID_CAP_NETWORKING is enabled
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

namespace FSWP.Net
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// Define different types of method to send params (get, post)
    /// </summary>
    public enum eSendingMethod
    {
        GET,
        POST
    }

    /// <summary>
    /// Abstraction of the class HttpWebRequest
    /// </summary>
    public class FSWPWebRequest
    {

        /// <summary>
        /// Defines type of delegate for request callback
        /// The param response is a string that contains the response to the request
        /// </summary>
        /// <param name="response"></param>
        public delegate void WebRequestCallback(string response);


        /// <summary>
        /// Defines type of delegate for serialization of params
        /// This callback is called to override the serialization of parameters before sending
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public delegate string WebRequestSerializer(Dictionary<string, object> parameters);

        #region Properties

        /// <summary>
        /// Method called when the web request success
        /// </summary>
        public WebRequestCallback Callback { get; set; }

        /// <summary>
        /// Method called when the web request fails
        /// </summary>
        public WebRequestCallback CallbackError { get; set; }

        /// <summary>
        /// Method called to serialize params
        /// </summary>
        public WebRequestSerializer Serializer { get; set; }

        /// <summary>
        /// Content type of the request
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Request caching is enabled
        /// </summary>
        public bool CacheEnabled { get; set; }

        private string _url;
        private eSendingMethod _method;
        private Dictionary<string, object> _parameters;
        private Dictionary<string, string> _headersString;
        private Dictionary<HttpRequestHeader, string> _headers;


        #endregion

        #region Constructors

        /// <summary>
        /// Creates request with parameters
        /// </summary>
        /// <param name="url">Url requested</param>
        /// <param name="method">Method of the request</param>
        /// <param name="parameters">Parameters to send</param>
        /// <param name="contentType">Type of content send</param>
        /// <param name="callback">Method called if web request success</param>
        /// <param name="callbackError">Method called if web request fails</param>
        public FSWPWebRequest(string url, eSendingMethod method, Dictionary<string, object> parameters, string contentType, WebRequestCallback callback = null, WebRequestCallback callbackError = null)
        {
            _url = url;
            Callback = callback;
            CallbackError = callbackError;
            _method = method;
            _parameters = parameters;
            ContentType = contentType;
            CacheEnabled = false;
        }

        /// <summary>
        /// Creates request without parameters
        /// </summary>
        /// <param name="url">Url requested</param>
        /// <param name="callback">Method called if web request success</param>
        /// <param name="callbackError">Method called if web request fails</param>
        /// <param name="contentType">Type of content send</param>
        public FSWPWebRequest(string url, eSendingMethod method, WebRequestCallback callback = null, WebRequestCallback callbackError = null)
        {
            _url = url;
            Callback = callback;
            CallbackError = callbackError;
            _method = method;
            _parameters = new Dictionary<string, object>();
            ContentType = "";
            CacheEnabled = false;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Launches the request
        /// </summary>
        public void Begin()
        {
            if (_method == eSendingMethod.GET)
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(_url + PrepareData());
                request.Method = "GET";
                PrepareHeader(request);
                request.BeginGetResponse(EndResponse, request);
            }
            else
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(_url);
                request.Method = "POST";
                PrepareHeader(request);
                if (ContentType != null && ContentType.Length > 0)
                    request.ContentType = ContentType;
                request.BeginGetRequestStream(PrepareDataAsync, request);
            }
        }

        /// <summary>
        /// Add a parameter in the request
        /// </summary>
        /// <param name="key">Name of the parameter</param>
        /// <param name="value">Value of the parameter</param>
        public void AddParameter(string key, object value)
        {
            _parameters.Add(key, value);
        }

        /// <summary>
        /// Clear the parameters of the request
        /// </summary>
        public void ClearParameters()
        {
            _parameters.Clear();
        }

        /// <summary>
        /// Set an header param to the request
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetHeaderParam(string key, string value)
        {
            if (_headersString == null)
                _headersString = new Dictionary<string, string>();
            _headersString.Add(key, value);
        }

        /// <summary>
        /// Set an header param to the request
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetHeaderParam(HttpRequestHeader key, string value)
        {
            if (_headers == null)
                _headers = new Dictionary<HttpRequestHeader, string>();
            _headers.Add(key, value);
        }

        #endregion

        #region Private

        private void PrepareHeader(HttpWebRequest request)
        {
            if (request.Headers == null)
                request.Headers = new WebHeaderCollection();
            if (_headersString != null)
            {
                foreach (KeyValuePair<string, string> param in _headersString)
                    request.Headers[param.Key] = param.Value;
            }
            if (_headers != null)
            {
                foreach (KeyValuePair<HttpRequestHeader, string> param in _headers)
                    request.Headers[param.Key] = param.Value;
            }
            if (!CacheEnabled)
            {
                request.Headers[HttpRequestHeader.CacheControl] = "no-cache";
                request.Headers[HttpRequestHeader.IfModifiedSince] = DateTime.UtcNow.ToString();
            }
        }

        private string PrepareData()
        {
            string dataToSend = "";
            if (_parameters != null && _parameters.Count > 0)
            {
                if (Serializer != null)
                    dataToSend = Serializer(_parameters);
                else
                {
                    bool first = true;
                    foreach (KeyValuePair<string, object> pair in _parameters)
                    {
                        dataToSend += (first) ? "?" : "&";
                        dataToSend += pair.Key + "=" + pair.Value;
                        first = false;
                    }
                }
            }
            return dataToSend;
        }

        private void PrepareDataAsync(IAsyncResult asyncResult)
        {
            HttpWebRequest request = (HttpWebRequest)asyncResult.AsyncState;
            if (_parameters != null && _parameters.Count > 0)
            {
                Stream requestStream = request.EndGetRequestStream(asyncResult);
                string dataToSend = "";
                if (Serializer != null)
                    dataToSend = Serializer(_parameters);
                else
                {
                    bool first = true;
                    foreach (KeyValuePair<string, object> pair in _parameters)
                    {
                        if (!first)
                            dataToSend += "&";
                        dataToSend += pair.Key + "=" + pair.Value;
                        first = false;
                    }
                }

                byte[] array = Encoding.UTF8.GetBytes(dataToSend);
                requestStream.Write(array, 0, array.Length);
                requestStream.Close();

            }
            request.BeginGetResponse(EndResponse, request);
        }

        private void EndResponse(IAsyncResult asyncResult)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)asyncResult.AsyncState;
                try
                {
                    WebResponse webResponse = request.EndGetResponse(asyncResult);

                    if (Callback != null)
                    {
                        Stream stream = webResponse.GetResponseStream();
                        StreamReader streamReader = new StreamReader(stream);
                        string response = streamReader.ReadToEnd();
                        stream.Close();
                        streamReader.Close();
                        webResponse.Close();

                        Callback(response);
                    }
                }
                catch (WebException e)
                {
                    if (CallbackError != null)
                    {
                        Stream stream = e.Response.GetResponseStream();
                        StreamReader streamReader = new StreamReader(stream);
                        string response = streamReader.ReadToEnd();
                        stream.Close();
                        streamReader.Close();
                        e.Response.Close();

                        CallbackError(response);
                    }
                }
            }
            catch
            {
                if (CallbackError != null)
                    CallbackError("");
            }
        }
        
        #endregion
    }
}
