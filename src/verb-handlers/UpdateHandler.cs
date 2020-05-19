using System;
using System.IO;
using System.Collections.Generic;

namespace zoom {
    public class UpdateHandler {
        public static int Run(UpdateOptions options, string[] args) {
            // if user didn't supply something to change
            if (options.MeetingID.Length == 0 && options.Password.Length == 0) {
                ConsoleColor defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Didn't supply a meeting id or password to change");
                Console.WriteLine("Use --id or --password to denote the field to update");
                Console.ForegroundColor = defaultColor;

                return 1;
            }

            // if user didn't supply name of object to change
            while (options.Name.Length == 0) {
                Console.Write("Please provide the name of the meeting you'd like to update: ");
                options.Name = Console.ReadLine();
            }

            Dictionary<string, MeetingFile.Meeting> dict = MeetingFile.toObject(
                File.ReadAllText(Program.MEETING_FILE_PATH)
            );

            MeetingFile.Meeting meeting;

            // try to update
            if (dict.TryGetValue(options.Name, out meeting)) {
                meeting.ID = options.MeetingID.Length > 0 ? options.MeetingID : meeting.ID;
                meeting.Password = options.Password.Length > 0 ? options.Password : meeting.Password;

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
    }
}
