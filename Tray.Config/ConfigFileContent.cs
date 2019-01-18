using System;
using System.Collections.Generic;

namespace theCodeJerk.Tray.Config
{
    [Serializable]
    public class ConfigFileContent
    {
        public List<ContentItem> Items { get; set; }
        public ConfigFileContent()
        {
            Items = new List<ContentItem>();
        }
    }
}
