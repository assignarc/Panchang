using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.Serialization;
using BaseUserEvent = org.transliteral.panchang.UserEvent;
namespace org.transliteral.panchang.app
{

    [Serializable]
    public class UserEvent : BaseUserEvent, ICloneable, ISerializable
    {

        protected UserEvent(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }


        [Editor(typeof(UIStringTypeEditor), typeof(UITypeEditor))]
        public string EventDesc
        {
            get { return mEventDesc; }
            set { mEventDesc = value; }
        }


        public UserEvent() : base()
        {

        }
        public new object Clone()
        {
            UserEvent ue = new UserEvent();
            ue.EventName = EventName;
            ue.EventTime = EventTime;
            ue.WorkWithEvent = WorkWithEvent;
            ue.EventDesc = EventDesc;
            return ue;
        }

    }

}
