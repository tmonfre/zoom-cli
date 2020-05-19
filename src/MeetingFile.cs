using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace zoom {
    public class MeetingFile {
        
        public static Dictionary<string, Meeting> toObject(string json) {
            return JsonConvert.DeserializeObject<Dictionary<string, Meeting>>(json);
        }

        public static string toJSON(Dictionary<string, Meeting> dict) {
            return JsonConvert.SerializeObject(dict, Formatting.Indented);
        }

        public static void saveToFile(string json) {
            File.WriteAllText(Program.MEETING_FILE_PATH, json);
        }

        public static void saveToFile(Dictionary<string,Meeting> dict) {
            saveToFile(toJSON(dict));
        }

        public static bool AddMeeting(string name, Meeting meeting, Dictionary<string,Meeting> dict) {
            if (dict.ContainsKey(name)) {
                return false;
            }

            dict.Add(name, meeting);
            saveToFile(dict);

            return true;
        }

        public static bool AddMeeting(string name, Meeting meeting) {
            Dictionary<string,Meeting> dict = toObject(File.ReadAllText(Program.MEETING_FILE_PATH));

            if (dict.ContainsKey(name)) {
                return false;
            }

            dict.Add(name, meeting);
            saveToFile(dict);

            return true;
        }

        public static void RemoveMeeting(string name) {
            Dictionary<string,Meeting> dict = toObject(File.ReadAllText(Program.MEETING_FILE_PATH));
            dict.Remove(name);
            saveToFile(dict);
        }

        public static string GenerateEmptyFileContents() {
            return "{}";
        }

        public class Meeting {
            public string ID;
            public string Password;

            public static Meeting toObject(string json) {
                return JsonConvert.DeserializeObject<Meeting>(json);
            }

            public static string toJSON(Meeting obj) {
                return JsonConvert.SerializeObject(obj);
            }

            public string toJSON() {
                return toJSON(this);
            }
        }
    }
}