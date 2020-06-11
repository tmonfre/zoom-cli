using System;
using CommandLine;
using System.Collections.Generic;

namespace zoom {
    public class ErrorHandler {
        public static int Run(IEnumerable<Error> errs, string[] args) {
            foreach (Error err in errs) {
                if (err.GetType() == typeof(VersionRequestedError) || (args.Length > 0 && args[0] == "-v")) {
                    return printVersionInfo();
                } else if (err.GetType() == typeof(HelpVerbRequestedError) || (args.Length > 0 && args[0] == "-h")) {
                    return printHelpInfo();
                } else if (err.GetType() == typeof(BadVerbSelectedError)) {
                    return handleBadVerb(args);
                } else if (err.GetType() == typeof(NoVerbSelectedError)) {
                    return printHelpInfo();
                }
            }
            
            return 1;
        }

        public static int printVersionInfo() {
            Console.WriteLine($"Zoom Command Line Tool Version: {Program.VERSION}");
            return 0;
        }

        public static int printHelpInfo() {
            printVersionInfo();
            
            Console.WriteLine("\n  [url]\t\t\tLaunch meeting with given URL");
            Console.WriteLine("\n  [meeting id]\t\tLaunch meeting with given meeting ID");
            Console.WriteLine("\n  [name]\t\tLaunch meeting by name");
            Console.WriteLine("\n  new\t\t\tSave a meeting link with a name");
            Console.WriteLine("\n  list\t\t\tList all saved meetings");
            Console.WriteLine("\n  update\t\tUpdate saved meeting");
            Console.WriteLine("\n  delete\t\tDelete saved meeting");
            Console.WriteLine("\n  help\t\t\tDisplay more information on a specific command");
            Console.WriteLine("\n  version\t\tDisplay version information\n");

            return 0;
        }

        private static int handleBadVerb(string[] args) {
            int launchOutput;

            if (args.Length == 0) {
                return printHelpInfo();
            } else if (args[0] == "help") {
                return HelpHandler.Run(args);
            } else if ((launchOutput = LaunchHandler.TryLaunchFromString(args[0])) != -1) {
                return launchOutput;
            } else {
                return printHelpInfo();
            }
        }
    }
}
