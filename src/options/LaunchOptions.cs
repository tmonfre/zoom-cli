using CommandLine;

namespace zoom {

    [Verb("launch", HelpText = "Launch meeting")]
    public class LaunchOptions {
        [Option("id", Default = "", HelpText = "Meeting ID")]
        public string MeetingID { get; set; }

        [Option('p', "password", Default = "", HelpText = "Meeting Password")]
        public string Password { get; set; }

        [Option('n', "name", Default = "", HelpText = "Name used to launch meeting")]
        public string Name { get; set; }
    }
}
