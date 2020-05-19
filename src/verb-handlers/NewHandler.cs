using System;

namespace zoom {
    public class NewHandler {
        public static int Run(NewOptions options, string[] args) {
            MeetingFile.Meeting meeting = new MeetingFile.Meeting();
            meeting.ID = options.MeetingID;
            meeting.Password = options.Password;

            string name = options.Name;

            while (meeting.ID.Length == 0) {
                Console.Write("Please provide a meeting ID: ");
                meeting.ID = Console.ReadLine();
            }

            while (name.Length == 0) {
                Console.Write("Please provide a name for this meeting: ");
                name = Console.ReadLine();
            }

            return Convert.ToInt32(MeetingFile.AddMeeting(name, meeting));
        }
    }
}
