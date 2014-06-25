//
// FSWPSocketClientTCP.cs
// FSWP Library
//
// The class SocketClientTCP is compatible with Windows Phone 7.1 and above
// It is an abstraction of the Socket class to connect a client in a TCP connection
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
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    public class FSWPSocketClientTCP
    {
        #region Constants

        /// <summary>
        /// Default duration of the timeout in milliseconds
        /// </summary>
        private static readonly int DEFAULT_TIMEOUT_DURATION = 5000;

        /// <summary>
        /// The default maximum size of the data buffer to use with the asynchronous socket methods
        /// </summary>
        private static readonly int DEFAULT_MAX_BUFFER_SIZE = 2048;

        /// <summary>
        /// String returned when an asynchronous call timeout
        /// </summary>
        public static readonly string ERROR_TIMEOUT = "Operation Timeout";

        /// <summary>
        /// String returned for an action with an unitialized socket
        /// </summary>
        public static readonly string ERROR_UNITIALIZED_SOCKET = "Socket is not initialized";

        #endregion

        #region Properties

        /// <summary>
        /// Cached Socket object that will be used by each call for the lifetime of this class
        /// </summary>
        private Socket _socket = null;

        /// <summary>
        /// Signaling object used to notify when an asynchronous operation is completed
        /// </summary>
        private static ManualResetEvent _clientDone = new ManualResetEvent(false);

        /// <summary>
        /// Duration of the timeout of an asynchronous call in milliseconds
        /// </summary>
        private int _timeoutDuration = DEFAULT_TIMEOUT_DURATION;

        /// <summary>
        /// Maximum size of the data buffer to use with the asynchronous socket methods
        /// </summary>
        private int _maxBufferSize = DEFAULT_MAX_BUFFER_SIZE;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a socket client with default values
        /// </summary>
        public FSWPSocketClientTCP() { }

        /// <summary>
        /// Creates a socket with custom values for timeout and buffer size
        /// </summary>
        /// <param name="timeoutDuration">Duration of the timeout of an asynchronous call in milliseconds</param>
        /// <param name="maxBufferSize">Maximum size of the data buffer to use with the asynchronous socket methods</param>
        public FSWPSocketClientTCP(int timeoutDuration, int maxBufferSize)
        {
            _timeoutDuration = timeoutDuration;
            _maxBufferSize = maxBufferSize;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Attempt a TCP socket connection to the given host over the given port
        /// </summary>
        /// <param name="hostName">The name of the host</param>
        /// <param name="portNumber">The port number to connect</param>
        /// <returns>A string representing the result of this connection attempt</returns>
        public string Connect(string hostName, int portNumber)
        {
            string result = string.Empty;

            DnsEndPoint hostEntry = new DnsEndPoint(hostName, portNumber);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            socketEventArg.RemoteEndPoint = hostEntry;
            socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
            {
                result = e.SocketError.ToString();
                _clientDone.Set();
            });

            _clientDone.Reset();
            _socket.ConnectAsync(socketEventArg);
            _clientDone.WaitOne(_timeoutDuration);

            return result;
        }

        /// <summary>
        /// Send the given data to the server using the established connection
        /// </summary>
        /// <param name="data">The data to send to the server</param>
        /// <returns>The result of the Send request</returns>
        public string Send(string data)
        {
            string response = ERROR_TIMEOUT;

            if (_socket != null)
            {
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
                socketEventArg.RemoteEndPoint = _socket.RemoteEndPoint;
                socketEventArg.UserToken = null;
                socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
                {
                    response = e.SocketError.ToString();
                    _clientDone.Set();
                });

                byte[] payload = Encoding.UTF8.GetBytes(data);
                socketEventArg.SetBuffer(payload, 0, payload.Length);

                _clientDone.Reset();
                _socket.SendAsync(socketEventArg);
                _clientDone.WaitOne(_timeoutDuration);
            }
            else
                response = ERROR_UNITIALIZED_SOCKET;

            return response;
        }

        /// <summary>
        /// Receive data from the server using the established socket connection
        /// </summary>
        /// <param name="timeout">timeout is enable ?</param>
        /// <returns>The data received from the server</returns>
        public string Receive(bool timeout = true)
        {
            string response = ERROR_TIMEOUT;

            if (_socket != null)
            {
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
                socketEventArg.RemoteEndPoint = _socket.RemoteEndPoint;
                socketEventArg.SetBuffer(new Byte[_maxBufferSize], 0, _maxBufferSize);
                socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
                {
                    if (e.SocketError == SocketError.Success)
                    {
                        response = Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred);
                        response = response.Replace("\0", string.Empty);
                    }
                    else
                        response = e.SocketError.ToString();

                    _clientDone.Set();
                });

                _clientDone.Reset();
                _socket.ReceiveAsync(socketEventArg);
                if (timeout)
                    _clientDone.WaitOne(_timeoutDuration);
                else
                    _clientDone.WaitOne();
            }
            else
                response = ERROR_UNITIALIZED_SOCKET;

            return response;
        }

        /// <summary>
        /// Closes the Socket connection and releases all associated resources
        /// </summary>
        public void Close()
        {
            if (_socket != null)
                _socket.Close();
        }

        #endregion
    }
}
