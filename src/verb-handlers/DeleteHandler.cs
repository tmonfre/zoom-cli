using System;
using System.Collections.Generic;

namespace zoom {
    public class DeleteHandler {
        public static int Run(DeleteOptions options, string[] args) {
            // figure out name to delete
            List<string> trimmedArgs = new List<string>(args);
            trimmedArgs.RemoveAll(s => s.StartsWith("-") || s == "delete");

            string nameToDelete = trimmedArgs.Count > 0 ? trimmedArgs[0] : "";

            // if user didn't supply name of object to delete
            while (nameToDelete.Length == 0) {
                Console.Write("Please provide the name of the meeting you'd like to delete: ");
                nameToDelete = Console.ReadLine();
            }

            MeetingFile.RemoveMeeting(nameToDelete);
            return 0;
        }
    }
}
