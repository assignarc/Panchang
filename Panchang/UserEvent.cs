using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.Serialization;
using BaseUserEvent = org.transliteral.panchang.UserEvent;
namespace mhora
{

    [Serializable]
    public class UserEvent : BaseUserEvent, ICloneable, ISerializable
    {
        
        protected UserEvent(SerializationInfo info, StreamingContext context) : base(info,context)
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
            ue.EventName = this.EventName;
            ue.EventTime = this.EventTime;
            ue.WorkWithEvent = this.WorkWithEvent;
            ue.EventDesc = this.EventDesc;
            return ue;
        }

    }

}
