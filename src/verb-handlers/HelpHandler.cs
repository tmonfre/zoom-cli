namespace zoom {
    public class HelpHandler {
        public static int Run(string[] args) {
            if (args.Length == 0 || args.Length == 1) {
                return ErrorHandler.printHelpInfo();
            } else {
                switch(args[1]) {
                    case "new":
                        return NewHandler.PrintHelpMenu();
                    case "list":
                        return ListHandler.PrintHelpMenu();
                    case "update":
                        return UpdateHandler.PrintHelpMenu();
                    case "delete":
                        return DeleteHandler.PrintHelpMenu();
                    case "launch":
                        return LaunchHandler.PrintHelpMenu();
                    default:
                        return ErrorHandler.printHelpInfo();
                }
            }
        }
    }
}
