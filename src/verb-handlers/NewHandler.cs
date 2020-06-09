using System;

namespace zoom {
    public class NewHandler {
        public static int Run(NewOptions options, string[] args) {
            MeetingFile.Meeting meeting = new MeetingFile.Meeting();
            meeting.ID = options.MeetingID;
            meeting.Password = options.Password;

            string name = options.Name;

             while (name.Length == 0) {
                Console.Write("Name for this meeting: ");
                name = Console.ReadLine();
            }

            while (meeting.ID.Length == 0) {
                Console.Write("Meeting ID: ");
                meeting.ID = Console.ReadLine();
            }

            Console.Write("Password: ");
            meeting.Password = Console.ReadLine();

            return Convert.ToInt32(MeetingFile.AddMeeting(name, meeting));
        }

        public static int PrintHelpMenu() {
            ErrorHandler.printVersionInfo();
            
            Console.WriteLine("\n Command new:");
            Console.WriteLine("\n  --id\t\t\tmeeting ID");
            Console.WriteLine("\n  -p, --password\tmeeting password");
            Console.WriteLine("\n  -n, --name\t\tname used to launch meeting\n");

            return 0;
        }
    }
}
