using System;
using System.Collections.Generic;
using System.Diagnostics;
using theCodeJerk.Tray.Config;

namespace theCodeJerk.Tray
{
    static class Program
    {
        /// <summary>
        /// The filename of the tray icon executable.
        /// </summary>
        private const string TrayExePath = "Tray.Icon.exe";
        /// <summary>
        /// Returns the path of the current executable.
        /// </summary>
        /// <returns></returns>
        private static string GetExePath()
        {
            return (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
        }
        /// <summary>
        /// Returns the path and filespec of the tray icon executable.
        /// This is needed for the command line argument -start which
        /// launches the tray icon executable.
        /// </summary>
        /// <returns></returns>
        private static string GetTrayExeFilespec()
        {
            return (System.IO.Path.Combine(GetExePath(), TrayExePath));
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            // If we're running with command line args
            // then we're in console mode, tray icon
            // can't be started here, it only begins
            // by double-clicking
            Int32 count = args.Length;
            if (count > 0)
            {
                Console.WriteLine("");
                for (int i = 0; i < count; i++)
                {
                    string arg = args[i];
                    // normalize the command so we can reprint
                    // it to the user/console if it is unknown
                    string normalized_arg = arg.Trim().ToLower();
                    switch (normalized_arg)
                    {
                        case "-start":
                            LaunchTrayIcon();
                            break;
                        case "-config":
                            string command = args[i + 1].Trim().ToLower();
                            switch (command)
                            {
                                case "-list":
                                    ListConfigItems();
                                    i += 1;
                                    break;
                                case "-get":
                                    ContentItem item = GetConfigItem(args[i + 2]);
                                    if (item == null)
                                    {
                                        Console.WriteLine(string.Format("There is no item with text {0}", args[i + 2]));
                                    }
                                    else
                                    {
                                        Console.WriteLine(string.Format("Text: {0}\nPath: {1}", item.Text, item.Command));
                                    }
                                    i += 2;
                                    break;
                                case "-add":
                                    AddConfigItem(args[i + 2], args[i + 3]);
                                    i += 3;
                                    break;
                                case "-remove":
                                    RemoveConfigItem(args[i + 2], args[i + 3]);
                                    i += 3;
                                    break;
                                default:
                                    i += 1;
                                    break;
                            }
                            break;
                        default:
                            Console.WriteLine(string.Format("Unknown Command: {0}", arg));
                            break;
                    }
                }
                return 0;
            }
            else
            {
                Console.WriteLine("theCodeJerk.Tray");
                return 0;
            }
        }
        private static void LaunchTrayIcon()
        {
            var proc = new Process();
            proc.StartInfo.FileName = GetTrayExeFilespec();
            proc.Start();
        }
        private static ContentItem GetConfigItem(string text)
        {
            ConfigFile file = new ConfigFile();
            file.LoadFile();
            foreach (ContentItem item in file.Content.Items)
            {
                if (item.Text.Trim().ToLower() == text.Trim().ToLower())
                {
                    return item;
                }
            }
            return null;
        }
        private static void AddConfigItem(string text, string command)
        {
            ContentItem item = new ContentItem();
            item.Text = text;
            item.Command = command;
            ConfigFile file = new ConfigFile();
            file.LoadFile();
            file.Content.Items.Add(item);
            file.SaveFile();
        }
        private static void RemoveConfigItem(string text, string command)
        {
            ContentItem item = new ContentItem();
            item.Text = text;
            item.Command = command;
            ConfigFile file = new ConfigFile();
            file.LoadFile();
            file.Content.Items.Remove(item);
            file.SaveFile();
        }

        private static void ListConfigItems()
        {
            ConfigFile file = new ConfigFile();
            file.LoadFile();
            List<ContentItem> items = file.Content.Items;
            if ((items != null) && (items.Count > 0))
            {
                foreach (ContentItem item in items)
                {
                    Console.WriteLine(string.Format("Text: {0}\nPath: {1}", item.Text, item.Command));
                }
            }
            else
            {
                Console.WriteLine("There are no items in the config file.");
            }
        }
    }
}
