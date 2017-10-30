using System;

using GameDevWare.Serialization;
using GameDevWare.Serialization.MessagePack;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;

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
        public ErrorEventArgs(string message)
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
        public MessageEventArgs(object data)
        {
            this.data = data;
        }
    }

    public class JoinEventArgs : EventArgs
    {

        public static object Deserialize(Type objectType, IndexedDictionary<string, object> state = null)
        {

            //Not sure what this part is suppose to do with state, just copied from issue#24 to show maybe it should work like this.
            var outputStream = new System.IO.MemoryStream();
            MsgPack.Serialize(state, outputStream);
            outputStream.Position = 0;
            var room = MsgPack.Deserialize(objectType, outputStream);

            return room;
        }

        public string roomname;

        public JoinEventArgs(string name)
        {

        }
    }



    /// <summary>
    /// Room Update Message
    /// </summary>
    public class RoomUpdateEventArgs : EventArgs
    {
        public static object Deserialize(Type objectType, IndexedDictionary<string, object> state = null)
        {
            foreach (KeyValuePair<string, object> s in state)
            {
                Debug.Log("Key: " + s.Key + " : value: " + s.Value);

                //if (s.Key == "roomname")
                //{
                    //Debug.Log("ROOM NAME");

                    //from the debug log above - Key: roomname : value: System.Collections.Generic.List`1[System.Object]
                    // To use a generic.list with system object, this is how its done.ß
                    //  https://stackoverflow.com/questions/694154/cast-an-object-into-generic-list
                    IEnumerable enumerable = s.Value as IEnumerable;

                    if (enumerable != null)
                    {
                        foreach (object item in enumerable)
                        {
                            Debug.Log(item.ToString());

                        }
                    }
                //}
             
            }




            //IndexedDictionary<string, object> name = (IndexedDictionary<string, object>)state["roomname"];
            //foreach (KeyValuePair<string, object> n in name)
            //{
            //	Debug.Log("OnUpdateHandler: key: " + n.Key + " : value : " + n.Value.ToString());

            //}

            var outputStream = new System.IO.MemoryStream();


            MsgPack.Serialize(state, outputStream);

            outputStream.Position = 0;

            //  var room = MsgPack.Deserialize(objectType, outputStream);

            var room = MsgPack.Deserialize(objectType, outputStream);

            outputStream.Position = 0;


            //TODO further work on this. We know the type. We can retrieve from it.q
            var test = MsgPack.Deserialize(state.GetType(), outputStream);
            Debug.Log("TEST: " + test.GetType());

            IndexedDictionary<string, object> dic1 = test as IndexedDictionary<string, object>;

              IEnumerable enumerable1 = dic1 as IEnumerable;

            if (dic1 != null)
            {
                foreach (KeyValuePair<string, object> obj in dic1)
                {
                    Debug.Log(obj.ToString() + " : dic1");

                    IEnumerable enumerable = obj.Value as IEnumerable;

                    if (enumerable != null)
                    {
                            foreach (object item in enumerable)
                            {
                                Debug.Log(item.ToString() + " : MsgPack deserialized");
                            }
                    }
                }
                    
            }

            //Debug.Log("ROOM " + room.ToString());
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
        public RoomUpdateEventArgs(IndexedDictionary<string, object> state, bool isFirstState = false, Type deserializeType = null)
        {

            this.state = state;
            this.isFirstState = isFirstState;
            this.deserializeType = deserializeType;

            room = Deserialize(deserializeType, state);
        }
    }



}

//[TypeSerializerAttribute(typeof(GameRoom))]
//public sealed class GameRoomSerializer : TypeSerializer
//{
//	public override Type SerializedType { get { return typeof(GameRoom); } }

//	public override object Deserialize(IJsonReader reader)
//	{
//		// General rule of 'Deserialize' is to leave reader on 
//		// last token of deserialized value. It is EndOfObject or EndOfArray, or Value.

//		// 'nextToken: true' will call 'reader.NextToken()' AFTER 'ReadString()'.
//		// Since it is last token on de-serialized value we set 'nextToken: false'.
//		var guidStr = reader.ReadString(nextToken: false);
//		//		var value = new Guid(guidStr);

//        Debug.Log("GameRoomSerializer: " + "Constructing game room");
//        var value = new GameRoom(guidStr);

//		return value;
//	}

//	public override void Serialize(IJsonWriter writer, object valueObj)
//	{
//        var value = (GameRoom)valueObj; // valueObj is not null
//		var guidStr = value.ToString();
//		writer.Write(guidStr);
//	}
//}

