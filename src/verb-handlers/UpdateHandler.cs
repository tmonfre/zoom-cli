using System;
using System.IO;
using System.Collections.Generic;

namespace zoom {
    public class UpdateHandler {
        public static int Run(UpdateOptions options, string[] args) {
            // parse through args
            if (options.Name.Length == 0) {
                List<string> trimmedArgs = new List<string>(args);
                trimmedArgs.RemoveAll(s => s.StartsWith("-") || s == "update");

                options.Name = trimmedArgs.Count > 0 ? trimmedArgs[0] : options.Name;
            }
                
            // if user didn't supply name of object to change
            while (options.Name.Length == 0) {
                Console.Write("Name of meeting to update: ");
                options.Name = Console.ReadLine();
            }

            Dictionary<string, MeetingFile.Meeting> dict = MeetingFile.toObject(
                File.ReadAllText(Program.MEETING_FILE_PATH)
            );

            MeetingFile.Meeting meeting;

            // try to update
            if (dict.TryGetValue(options.Name, out meeting)) {
                // store in new values user supplied
                meeting.ID = options.MeetingID.Length > 0 ? options.MeetingID : meeting.ID;
                meeting.Password = options.Password.Length > 0 ? options.Password : meeting.Password;

                // if no change, prompt user
                if (meeting.ID != options.MeetingID) {
                    meeting.ID = IOCommands.ReadInputWithDefault("Meeting ID: ", meeting.ID);
                }

                // if no change, prompt user
                if (meeting.Password != options.Password || meeting.Password.Length == 0) {
                    meeting.Password = IOCommands.ReadInputWithDefault("Meeting password: ", meeting.Password);
                }

                // attempt to update
                dict.Remove(options.Name);
                return Convert.ToInt32(MeetingFile.AddMeeting(options.Name, meeting, dict));
            } else {
                ConsoleColor defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not find meeting with name " + options.Name);
                Console.ForegroundColor = defaultColor;

                return 1;
            }
        }

        public static int PrintHelpMenu() {
            ErrorHandler.printVersionInfo();
            
            Console.WriteLine("\n Command update:");
            Console.WriteLine("\n  --id\t\t\tmeeting ID");
            Console.WriteLine("\n  -p, --password\tmeeting password");
            Console.WriteLine("\n  -n, --name\t\tname used to launch meeting\n");

            return 0;
        }
    }
}
