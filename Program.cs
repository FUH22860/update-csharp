public class Program {
    static void Main(string[] args) {
        string result = Update.copyEverthingBeforeUpdateToBackupLocation();
        Console.WriteLine(result);

        //string result2 = Update.copyEverthingFromBackupLocationToFinalDestination();
        //Console.WriteLine(result2);

        //Console.WriteLine(args.Length);

        bool result3 = Update.zipAllContentInBackupLocation();
        Console.WriteLine(result3);
    }
}