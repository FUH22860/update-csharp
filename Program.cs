public class Program {
    static void Main(string[] args) {
        string result = Update.copyEverthingBeforeUpdateToBackupLocation();
        Console.WriteLine(result);

        bool pacmanDatabaseResult = Update.zipPacmanDatabase();
        Console.WriteLine(pacmanDatabaseResult);

        bool result2 = Update.zipAllContentInBackupLocation("pre-backup.zip");
        Console.WriteLine(result2);

        string result3 = Update.copyEverthingAfterUpdateToBackupLocation();
        Console.WriteLine(result3);

        bool result4 = Update.zipAllContentInBackupLocation("after-backup.zip");
        if (result4) {
            Console.WriteLine(result4);
        } else {

        }

        string result5 = Update.copyEverthingFromBackupLocationToFinalDestination(args[0]); // "/artemis/test/"
        Console.WriteLine(result5);

        //Console.WriteLine(args[0]);
    }
}