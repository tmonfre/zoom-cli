using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace zoom {
    public class LaunchHandler {
        public static int Run(LaunchOptions options, string[] args) {
            if (options.MeetingID.Length > 0) {
                String url = "zoommtg://zoom.us/join?zc=0&stype=100&confno=" + options.MeetingID;
                
                if (options.Password.Length > 0) {
                    url += "&pwd=" + options.Password;
                }

                Process.Start("open", url);
            } else {
                string nameToLaunch = options.Name;

                // figure out name to launch if user didn't supply one
                if (nameToLaunch.Length == 0) {
                    // parse through args
                    List<string> trimmedArgs = new List<string>(args);
                    trimmedArgs.RemoveAll(s => s.StartsWith("-") || s == "launch");

                    nameToLaunch = trimmedArgs.Count > 0 ? trimmedArgs[0] : "";

                    // if user still didn't supply name of object to launch, ask
                    while (nameToLaunch.Length == 0) {
                        Console.Write("Name of meeting to launch: ");
                        nameToLaunch = Console.ReadLine();
                    }
                }

                // find meeting in meeting file
                Dictionary<string, MeetingFile.Meeting> dict = MeetingFile.toObject(
                    File.ReadAllText(Program.MEETING_FILE_PATH)
                );

                MeetingFile.Meeting meeting;

                // try to grab and launch
                if (dict.TryGetValue(nameToLaunch, out meeting)) {
                    // zoommtg://zoom.us/join?confno=7731206470&zc=0&browser=chrome
                    String url = "zoommtg://zoom.us/join?zc=0&stype=100&confno=" + meeting.ID;
                    
                    if (meeting.Password.Length > 0) {
                        url += "&pwd=" + meeting.Password;
                    }

                    Process.Start("open", url);
                } else {
                    // print out error message
                    ConsoleColor defaultColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Could not find meeting with name " + nameToLaunch);
                    Console.ForegroundColor = defaultColor;

                    return 1;
                }
            }

            return 0;
        }

        // attempt to launch a meeting from either the saved name, the URL, or the meeting id
        public static int TryLaunchFromString(string s) {
            // read in meeting file
            Dictionary<string, MeetingFile.Meeting> dict = MeetingFile.toObject(
                File.ReadAllText(Program.MEETING_FILE_PATH)
            );

            MeetingFile.Meeting meeting;

            // try to launch by name
            if (dict.TryGetValue(s, out meeting)) {
                // zoommtg://zoom.us/join?confno=7731206470&zc=0&browser=chrome
                String url = "zoommtg://zoom.us/join?zc=0&stype=100&confno=" + meeting.ID;
                
                if (meeting.Password.Length > 0) {
                    url += "&pwd=" + meeting.Password;
                }

                Process.Start("open", url);
            } 
            // try to launch by URL or meeting id
            else {
                String url = null;

                // check if URL
                if (s.Contains("https://") && s.Contains("zoom.us") && s.Contains("/j/")) {
                    url = "zoommtg://zoom.us/join?zc=0&stype=100&confno=" + s.Substring(s.IndexOf("/j/") + 3);
                } 
                // check if meeting ID
                else if (s.All(char.IsDigit)) {
                    url = "zoommtg://zoom.us/join?zc=0&stype=100&confno=" + s;
                }

                // if passed checks and have a URL, try it out, otherwise fail back to menu
                if (url != null) {
                    Process.Start("open", url);
                } else return -1;
            }

            return 0;
        }

        public static int PrintHelpMenu() {
            ErrorHandler.printVersionInfo();
            
            Console.WriteLine("\n Command launch:");

            Console.WriteLine("\n Example commands:");
            Console.WriteLine("   zoom [url]");
            Console.WriteLine("   zoom [meeting id]");
            Console.WriteLine("   zoom [name]");
            Console.WriteLine("   zoom launch [url]");
            Console.WriteLine("   zoom launch [meeting id]");
            Console.WriteLine("   zoom launch [name]");

            Console.WriteLine("\n Additional options:");
            Console.WriteLine("\n  --id\t\t\tmeeting ID");
            Console.WriteLine("\n  -p, --password\tmeeting password\n");

            return 0;
        }
    }
}
