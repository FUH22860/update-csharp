public class Program {
    static void Main(string[] args) {
        string result = Update.copyEverthingBeforeUpdateToBackupLocation();
        Console.WriteLine(result);

        string result2 = Update.copyEverthingFromBackupLocationToFinalDestination();
        Console.WriteLine(result2);

        Console.WriteLine(args);
    }
}