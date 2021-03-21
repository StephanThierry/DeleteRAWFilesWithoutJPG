using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DeleteRAWFilesWithoutJPG
{
    class Program
    {
        private static string appFolder = Directory.GetCurrentDirectory();
        public static Settings settings = null;

        private static void InitConfig()
        {
            string settingsPath = Path.Combine(appFolder, "settings.json");
            string settings_json = "";
            try
            {
                settings_json = System.IO.File.ReadAllText(settingsPath);
            }
            catch
            {
                // Presumably settings.json does not exist - set default values and save
                Console.WriteLine("Creating 'settings.json' - edit settings and run again.");
                settings = new Settings();
                settings.jpgFolder = "J:\\DCIM\\100CANON";
                settings.rawFolder = "J:\\DCIM\\100CANON\\RAW";
                File.WriteAllText(settingsPath, lameJSONBeautifier(JsonSerializer.Serialize(settings)));
            }

            if (settings == null) settings = JsonSerializer.Deserialize<Settings>(settings_json);
        }
        private static string lameJSONBeautifier(string json)
        {
            string[] postChars = { "{", "," };
            string[] preChars = { "}" };
            foreach (string c in postChars) json = json.Replace(c, c + Environment.NewLine);
            foreach (string c in preChars) json = json.Replace(c, Environment.NewLine + c);

            return (json);
        }

        static List<string> Compare()
        {
            string[] jpgFiles = Directory.GetFiles(settings.jpgFolder);
            string[] rawFiles = Directory.GetFiles(settings.rawFolder);
            List<string> noMatch = new List<string>();

            for (int i = 0; i < rawFiles.Length; i++)
            {
                string raw = Path.GetFileNameWithoutExtension(rawFiles[i]);
                if (!jpgFiles.Any(f=>Path.GetFileNameWithoutExtension(f)==raw)){
                    noMatch.Add(rawFiles[i]);
                }
            }
            return noMatch;
        }
        
        public static void generateBatchFile(List<string> mediaList)
        {
            string filepath = Path.Combine(settings.jpgFolder, "delete_raws_without_jpg.bat");
            string filetext = "chcp 65001" + Environment.NewLine;
            foreach (string item in mediaList)
            {
                // % needs to be escaped to %% to work in a .bat file
                filetext += "DEL \"" + item.Replace("%", "%%") + "\"" + Environment.NewLine;
            }
            File.WriteAllText(filepath, filetext);
            Console.WriteLine("Delete batchfile: " + filepath);
        }
        static void Main(string[] args)
        {
            InitConfig();
            generateBatchFile(Compare());;
        }
    }
}
