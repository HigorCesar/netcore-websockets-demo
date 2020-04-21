using System;

namespace NetCore.WebSockets.Demo.Client
{
    class NotificationMessage
    {
        public string Body {
            get; set;
        }

        public string Icon {
            get; set;
        }

        public string Summary {
            get; set;
        }

        public override string ToString ()
        {
            return $"{Summary}: {Body}";
        }
    }
}