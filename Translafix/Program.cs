using UndertaleModLib;
using static UndertaleModLib.UndertaleReader;

namespace Translafix;
public class Program
{

    /// <summary>
    /// A simple warning handler that prints warnings to console.
    /// </summary>
    /// <param name="warning">The warning to print</param>
    /// <param name="isImportant">Whether the warning is important (may lead to data corruption)</param>
    private static void WarningHandler(string warning, bool isImportant) => Console.WriteLine($"[WARNING]: {warning}");

    /// <summary>
    /// A simple message handler that prints messages to console.
    /// </summary>
    /// <param name="message">The message to print</param>
    private static void MessageHandler(string message) => Console.WriteLine($"[MESSAGE]: {message}");

    // <summary>
    // Outputs the text for correct usage of the program
    // </summary>
    private static void PrintUsage() => Console.WriteLine("Usage: <target data.win> -o <output file>\nExample: ./translafix notworking.win -o working.win");

    /// <summary>
    /// Read supplied filename and return the data file.
    /// </summary>
    /// <param name="datafile">The datafile to read</param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException">If the data file cannot be found</exception>
    private static UndertaleData ReadDataFile(FileInfo datafile, WarningHandlerDelegate warningHandler, MessageHandlerDelegate messageHandler)
    {
        using FileStream fs = datafile.OpenRead();
        UndertaleData gmData = UndertaleIO.Read(fs, warningHandler, messageHandler);
        return gmData;
    }

    /// <summary>
    /// Saves the currently loaded <see cref="Data"/> to an output path.
    /// </summary>
    /// <param name="outputPath">The path where to save the data.</param>
    /// <exception cref="IOException">If saving fails</exception>
    private static void SaveDataFile(string outputPath, UndertaleData Data)
    {
        try
        {
            // Save data.win to temp file
            using (FileStream fs = new(outputPath + "temp", FileMode.Create, FileAccess.Write))
            {
                UndertaleIO.Write(fs, Data, MessageHandler);
            }

            // If we're executing this, the saving was successful. So we can replace the new temp file
            // with the older file, if it exists.
            File.Move(outputPath + "temp", outputPath, true);

            Console.WriteLine($"Saved fixed data file to '{outputPath}'");
        }
        catch (Exception e)
        {
            // Delete the temporary file in case we partially wrote it
            if (File.Exists(outputPath + "temp"))
                File.Delete(outputPath + "temp");
            throw new IOException($"Could not save data file: {e.Message}");
        }
    }

    /// <summary>
    /// Main entrypoint for Translafix
    /// </summary>
    /// <param name="args">Arguments passed on to program.</param>
    /// <returns>Result code of the program.</returns>
    public static int Main(string[] args)
    {
        Console.WriteLine("translafix (c) 2026 pugdev, eliandro4 and UTMT contributors");

        if (OperatingSystem.IsWindows() && args.Length == 0 && Console.GetCursorPosition() == (0, 0))
        {
            Console.WriteLine("Translafix is a CLI-only program and needs to be run in the Command Line or Powershell\n\n(Press any key to dismiss this message.)");
            Console.ReadKey();
            return 1;
        }

        if (args.Length < 3)
        {
            PrintUsage();
            return 1;
        }

        string dataPath = args[0];
        string outputFlag = args[1];
        string outputFile = args[2];

        FileInfo translaFile = new(dataPath);

        if (outputFlag != "-o" && outputFlag != "--output")
        {
            Console.WriteLine("ERROR: incorrect output flag");
            PrintUsage();
            return 1;
        }

        if (!File.Exists(translaFile.FullName))
        {
            Console.WriteLine("ERROR: target file does not exist");
            PrintUsage();
            return 1;
        }

        Console.WriteLine($"Attempting to load: {translaFile.FullName}");
        UndertaleData translaData = ReadDataFile(translaFile, WarningHandler, MessageHandler);

        Console.WriteLine("Loaded data.win! Saving in correct format...");
        SaveDataFile(outputFile, translaData);

        Console.WriteLine("Done :D");

        return 0;
    }
}