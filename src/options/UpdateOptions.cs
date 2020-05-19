using CommandLine;

namespace zoom {

    [Verb("update", HelpText = "Update recurring link")]
    public class UpdateOptions {
        [Option('n', "name", Default = "", HelpText = "Name used to launch meeting", Required = true)]
        public string Name { get; set; }

        [Option("id", Default = "", HelpText = "Meeting ID")]
        public string MeetingID { get; set; }

        [Option('p', "password", Default = "", HelpText = "Meeting Password")]
        public string Password { get; set; }
    }
}
