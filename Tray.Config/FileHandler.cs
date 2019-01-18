using System;
using System.IO;
using System.Xml.Serialization;

namespace theCodeJerk.Tray.Config
{
    static class FileHandler
    {
        public static ConfigFileContent LoadObjectFile(string filespec)
        {
            ConfigFileContent thisObject = null;
            try
            {
                using (StreamReader stream = new StreamReader(@filespec))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ConfigFileContent));
                    thisObject = (ConfigFileContent)serializer.Deserialize(stream);
                    stream.Close();
                }
            }
            catch (Exception)
            {
                thisObject = new ConfigFileContent();
            }
            return thisObject;
        }
        public static void SaveObjectFile(ConfigFileContent thisObject, string filespec)
        {
            try
            {
                using (StreamWriter stream = new StreamWriter(filespec))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ConfigFileContent));
                    serializer.Serialize(stream, thisObject);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
