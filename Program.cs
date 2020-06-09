using System;
using System.IO;
using CommandLine;

namespace zoom {
    class Program {
        public static string DIRECTORY_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ".zoom-cli");
        public static string MEETING_FILE_PATH = Path.Combine(DIRECTORY_PATH, "meetings.json");
        public static string VERSION = "1.0.0";

        static int Main(string[] args) {
            _setup();

            return new Parser(with => { with.HelpWriter = null; with.AutoHelp = false; })
                .ParseArguments<NewOptions, LaunchOptions, ListOptions, UpdateOptions, DeleteOptions>(args)
                .MapResult(
                    (NewOptions opts) => NewHandler.Run(opts, args),
                    (LaunchOptions opts) => LaunchHandler.Run(opts, args),
                    (ListOptions opts) => ListHandler.Run(opts, args),
                    (UpdateOptions opts) => UpdateHandler.Run(opts, args),
                    (DeleteOptions opts) => DeleteHandler.Run(opts, args),
                errs => ErrorHandler.Run(errs, args));
        }

        private static void _setup() {
            // generate home directory if doesn't exist
            if (!Directory.Exists(DIRECTORY_PATH)) {
                Directory.CreateDirectory(DIRECTORY_PATH);
            }

            // generate tracking file if doesn't exist
            if (!File.Exists(MEETING_FILE_PATH)) {
                File.WriteAllText(MEETING_FILE_PATH, MeetingFile.GenerateEmptyFileContents());
            }
        }
    }
}
