internal static class ProgramHelpers
{

    public static List<string> SearchFiles(string directoryPath, string fileName)
    {
        List<string> matchingFiles = new List<string>();

        try
        {
            // SearchOption.AllDirectories to search in sub directories
             foreach (string file in Directory.GetFiles(directoryPath, fileName, SearchOption.AllDirectories))
            {
                matchingFiles.Add(file);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        return matchingFiles;
    }
}