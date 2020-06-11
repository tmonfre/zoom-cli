using System;
using System.IO;
using System.Collections.Generic;

namespace zoom {
    public class ListHandler {
        public static int Run(ListOptions options, string[] args) {
            Dictionary<string, MeetingFile.Meeting> dict = MeetingFile.toObject(
                File.ReadAllText(Program.MEETING_FILE_PATH)
            );

            if (dict.Count == 0) {
                Console.WriteLine("No saved zoom meetings.");
                Console.WriteLine("Run zoom new to save a meeting.");
            } else {
                Console.WriteLine();

                foreach(string name in dict.Keys) {
                    Console.WriteLine(name + ":");
                    
                    MeetingFile.Meeting meeting;

                    if (dict.TryGetValue(name, out meeting)) {
                        Console.WriteLine("  Meeting ID: " + meeting.ID);
                        Console.WriteLine("  Password ID: " + (meeting.Password.Length > 0 ? meeting.Password : "N/A"));
                        Console.WriteLine();
                    } else {
                        ConsoleColor defaultColor = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Encountered an error reading the stored data.");
                        Console.ForegroundColor = defaultColor;

                        return 1;
                    }
                }
            }

            return 0;
        }

        public static int PrintHelpMenu() {
            ErrorHandler.printVersionInfo();
            
            Console.WriteLine("\n Command list:");
            Console.WriteLine(" No available subcommands\n");

            return 0;
        }
    }
}
