using System;

namespace theCodeJerk.Tray.Config
{
    [Serializable]
    public class ContentItem
    {
        public string Text { get; set; }
        public string Command { get; set; }
    }
}
