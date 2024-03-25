using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace org.transliteral.panchang
{

    [Serializable]
    public class UserEvent : MhoraSerializableOptions, ICloneable, ISerializable
    {
        void ISerializable.GetObjectData(
            SerializationInfo info, StreamingContext context)
        {
            this.GetObjectData(this.GetType(), info, context);
        }

        protected UserEvent(SerializationInfo info, StreamingContext context) : this()
        {
            this.Constructor(this.GetType(), info, context);
        }

        protected string mEventName;
        protected string mEventDesc;
        protected Moment mEventTime;
        protected bool mWorkWithEvent;

        public string EventName
        {
            get { return mEventName; }
            set { mEventName = value; }
        }

      

        public Moment EventTime
        {
            get { return mEventTime; }
            set { mEventTime = value; }
        }

        public bool WorkWithEvent
        {
            get { return mWorkWithEvent; }
            set { mWorkWithEvent = value; }
        }

        public override string ToString()
        {
            string ret = "";

            if (this.WorkWithEvent)
                ret += "* ";
            ret += this.EventName + ": " + this.EventTime.ToString();
            return ret;
        }

        public UserEvent()
        {
            this.EventName = "Some Event";
            this.EventTime = new Moment();
            this.WorkWithEvent = true;
        }

        public object Clone()
        {
            UserEvent ue = new UserEvent();
            ue.EventName = this.EventName;
            ue.EventTime = this.EventTime;
            ue.WorkWithEvent = this.WorkWithEvent;
            return ue;
        }
    }

}
