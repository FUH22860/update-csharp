public class Program {
    static void Main(string[] args) {

        string beforeUpdate() {

            Update.copyEverthingBeforeUpdateToBackupLocation();
            Update.zipAllContentInBackupLocation("pre-backup.zip");
            Update.zipPacmanDatabase();

            Console.ForegroundColor = ConsoleColor.Green;
            return "pre-backup complete";
        }

        string postUpdate() {

            Update.copyEverthingAfterUpdateToBackupLocation();
            Update.zipAllContentInBackupLocation("post-backup.zip");
            Update.copyEverthingFromBackupLocationToFinalDestination(args[0]);

            Console.ForegroundColor = ConsoleColor.Green;
            return "post-backup complete";
        }

        string testPacmanDatabase() {

            Update.zipPacmanDatabase();

            Console.ForegroundColor = ConsoleColor.Yellow;
            return "Test complete";
        }

        // Use https://learn.microsoft.com/en-us/dotnet/api/system.io.filesystemwatcher?redirectedfrom=MSDN&view=net-7.0

        try {
            switch(args[1]) {
                case "pre-backup":
                    beforeUpdate();
                    Console.ResetColor();
                    break;
                case "post-backup":
                    postUpdate();
                    Console.ResetColor();
                    break;
                case "testPacmanDatabase":
                    testPacmanDatabase();
                    Console.ResetColor();
                    break;
                default:
                    Console.WriteLine("Wait! How did you do that?");
                    break;
            }
        } catch (Exception e) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Message);
        }
    }
}