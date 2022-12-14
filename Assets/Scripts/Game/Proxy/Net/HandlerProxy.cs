using System;
using Google.Protobuf;
using PureMVC.Patterns.Proxy;
using Net;
using System.Collections.Generic;

namespace Game
{

    public delegate void Handler(object data);
    public class HandlerProxy : Proxy
    {
        public new static string NAME = typeof(HandlerProxy).FullName;

        private Dictionary<Type, Handler> handlers;

        public HandlerProxy() : base(NAME) { }

        public override void OnRegister()
        {
            base.OnRegister();
            handlers = new Dictionary<Type, Handler>();
        }

        public override void OnRemove()
        {
        }

        public void RegisterHandler(Type type, Handler handler)
        {
            //one type may have more than one handler
            if (handlers.ContainsKey(type))
            {
                handlers[type] += handler;
            }
            else
            {
                handlers.Add(type, handler);
            }
        }

        public void RemoveHandler(Type type, Handler handler)
        {
            if (handlers != null && handlers.ContainsKey(type))
            {
                handlers[type] -= handler;
            }
        }



        public void DispatchProto(uint protoId, byte[] buff)
        {
            if (!ProtoUtils.ContainProtoId(protoId))
                return;

            Type protoType = ProtoUtils.GetProtoTypeByProtoId(protoId);
            try
            {
                MessageParser messageParser = ProtoUtils.GetMessageParser(protoType.TypeHandle);
                object toc = messageParser.ParseFrom(buff);

                if (handlers.TryGetValue(protoType, out Handler handler))
                {
                    handler(toc);
                }
            }
            catch
            {
                Console.WriteLine("DispatchProto Error:" + protoType.ToString());
            }
        }
    }
}