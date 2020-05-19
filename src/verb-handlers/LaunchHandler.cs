using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace zoom {
    public class LaunchHandler {
        public static int Run(LaunchOptions options, string[] args) {
            // figure out name to launch
            List<string> trimmedArgs = new List<string>(args);
            trimmedArgs.RemoveAll(s => s.StartsWith("-") || s == "launch");

            string nameToLaunch = trimmedArgs.Count > 0 ? trimmedArgs[0] : "";

            // if user didn't supply name of object to launch
            while (nameToLaunch.Length == 0) {
                Console.Write("Please provide the name of the meeting you'd like to launch: ");
                nameToLaunch = Console.ReadLine();
            }

            Dictionary<string, MeetingFile.Meeting> dict = MeetingFile.toObject(
                File.ReadAllText(Program.MEETING_FILE_PATH)
            );

            MeetingFile.Meeting meeting;

            // try to update
            if (dict.TryGetValue(nameToLaunch, out meeting)) {
                // zoommtg://zoom.us/join?confno=7731206470&zc=0&browser=chrome
                String url = "zoommtg://zoom.us/join?zc=0&stype=100&confno=" + meeting.ID;
                
                if (meeting.Password.Length > 0) {
                    url += "&pwd=" + meeting.Password;
                }

                Process.Start("open", url);
            } else {
                ConsoleColor defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not find meeting with name " + nameToLaunch);
                Console.ForegroundColor = defaultColor;

                return 1;
            }

            return 0;
        }
    }
}
