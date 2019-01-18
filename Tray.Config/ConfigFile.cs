namespace theCodeJerk.Tray.Config
{
    /// <summary>
    /// Read/write the configuration file for the tray icon.
    /// </summary>
    public class ConfigFile
    {
        private const string ConfigFile_Filename = "thecodejerk.tray.config.xml";
        private string GetExePath()
        {
            return (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
        }
        private string GetFilespec()
        {
            return (System.IO.Path.Combine(GetExePath(), ConfigFile_Filename));
        }
        private ConfigFileContent _Content = new ConfigFileContent();
        /// <summary>
        /// The content of the configuration file.
        /// </summary>
        public ConfigFileContent Content
        {
            get
            {
                return _Content;
            }
            set
            {
                _Content = value;
            }
        }
        /// <summary>
        /// Load the configuration file into memory.
        /// </summary>
        public void LoadFile()
        {
            Content = FileHandler.LoadObjectFile(GetFilespec());
        }
        public void LoadFile(string filespec)
        {
            Content = FileHandler.LoadObjectFile(filespec);
        }
        /// <summary>
        /// Save the configuration file to disk.
        /// </summary>
        public void SaveFile()
        {
            FileHandler.SaveObjectFile(Content, GetFilespec());
        }
        public void SaveFile(string filespec)
        {
            FileHandler.SaveObjectFile(Content, filespec);
        }
    }
}
