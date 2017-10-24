﻿using System;

using GameDevWare.Serialization;
using GameDevWare.Serialization.MessagePack;

namespace Colyseus
{
	/// <summary>
	/// Representation of a message received from the server.
	/// </summary>
	public class ErrorEventArgs : EventArgs
	{
		/// <summary>
		/// The error message
		/// </summary>
		public string message = null;

		/// <summary>
		/// </summary>
		public ErrorEventArgs (string message)
		{
			this.message = message;
		}
	}

	/// <summary>
	/// Representation of a message received from the server.
	/// </summary>
	public class MessageEventArgs : EventArgs
	{
		/// <summary>
		/// Data coming from the server.
		/// </summary>
		public object data = null;

		/// <summary>
		/// </summary>
		public MessageEventArgs (object data)
		{
			this.data = data;
		}
	}

	/// <summary>
	/// Room Update Message
	/// </summary>
	public class RoomUpdateEventArgs : EventArgs
	{
        /// <summary>
        /// Deserialize the specified state.
        /// </summary>
        /// <returns>The deserialize.</returns>
        /// <param name="state">State.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T Deserialize<T>(IndexedDictionary<string, object> state = null){
            return (T)Deserialize(typeof(T), state);
        }


        /// <summary>
        /// Deserialize the specified objectType and state.
        /// </summary>
        /// <returns>The deserialize.</returns>
        /// <param name="objectType">Object type.</param>
        /// <param name="state">State.</param>
        public static object Deserialize(Type objectType, IndexedDictionary<string, object> state = null){

			var outputStream = new System.IO.MemoryStream();

            MsgPack.Serialize(state, outputStream);
			outputStream.Position = 0;

            var room = MsgPack.Deserialize(objectType, outputStream);

            return room;
        }

		/// <summary>
		/// New state of the <see cref="Room" />
		/// </summary>
		public IndexedDictionary<string, object> state;

		/// <summary>
		/// Boolean representing if the event is setting the state of the <see cref="Room" /> for the first time.
		/// </summary>
		public bool isFirstState;

        public object room;

        public Type deserializeType;

		/// <summary>
		/// </summary>
		public RoomUpdateEventArgs (IndexedDictionary<string, object> state, bool isFirstState = false, Type deserializeType = null)
		{
			this.state = state;
			this.isFirstState = isFirstState;
            this.deserializeType = deserializeType;

            room = Deserialize(deserializeType, state);
		}
	}
}

