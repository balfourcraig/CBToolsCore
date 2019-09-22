using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CBTools_Core.IO
{
    public static class FileUsage
    {
        //public static IEnumerable<FileInfo> GetAllFilesRec(string parentDirectoryPath) => GetAllFilesRec(parentDirectoryPath);

        public static IEnumerable<FileInfo> GetAllFilesRec(string parentDirectoryPath, string searchPattern)
        {
            DirectoryInfo d = new DirectoryInfo(parentDirectoryPath);
            FileInfo[] files = string.IsNullOrWhiteSpace(searchPattern) ? d.GetFiles() : d.GetFiles(searchPattern);

            foreach (FileInfo f in files)
            {
                if (f.Name.StartsWith("-"))
                {
                    Console.WriteLine("Ignoring file " + f.Name);
                }
                else
                    yield return f;
            }
            if (d.GetDirectories().Length > 0)
            {
                foreach (DirectoryInfo sub in d.GetDirectories())
                    foreach (FileInfo file in GetAllFilesRec(sub.FullName, searchPattern))
                        yield return file;
            }
        }

        public static IEnumerable<FileInfo> GetAllFilesRec(string parentDirectoryPath, char ignoreStartingWith)
        {
            DirectoryInfo d = new DirectoryInfo(parentDirectoryPath);
            FileInfo[] files = d.GetFiles();
            foreach (FileInfo f in files)
            {
                if (f.Name.StartsWith("-"))
                {
                    Console.WriteLine("Ignoring file " + f.Name);
                }
                else
                    yield return f;
            }
            if (d.GetDirectories().Length > 0)
            {
                foreach (DirectoryInfo sub in d.GetDirectories())
                    if (sub.Name.Length > 0 && sub.Name[0] == ignoreStartingWith)
                        Console.WriteLine("Ignoring folder " + sub.Name);
                    else
                        foreach (FileInfo file in GetAllFilesRec(sub.FullName, ignoreStartingWith))
                            yield return file;
            }
        }

        public static IEnumerable<FileInfo> GetAllFilesRec(string parentDirectoryPath, char ignoreStartingWith, string searchPattern)
        {
            DirectoryInfo d = new DirectoryInfo(parentDirectoryPath);
            FileInfo[] files = string.IsNullOrWhiteSpace(searchPattern) ? d.GetFiles() : d.GetFiles(searchPattern);
            foreach (FileInfo f in files)
            {
                if (f.Name.StartsWith("-"))
                {
                    Console.WriteLine("Ignoring file " + f.Name);
                }
                else
                    yield return f;
            }
            if (d.GetDirectories().Length > 0)
            {
                foreach (DirectoryInfo sub in d.GetDirectories())
                    if (sub.Name.Length > 0 && sub.Name[0] == ignoreStartingWith)
                        Console.WriteLine("Ignoring folder " + sub.Name);
                    else
                        foreach (FileInfo file in GetAllFilesRec(sub.FullName, ignoreStartingWith, searchPattern))
                            yield return file;
            }
        }

        public static IEnumerable<string> Lines(FileInfo file) => Lines(file.FullName);

        public static IEnumerable<string> Lines(string path)
        {
            using FileStream fs = new FileStream(path, FileMode.Open);
            using StreamReader reader = new StreamReader(fs);
            while (!reader.EndOfStream)
            {
                yield return reader.ReadLine();
            }
        }
    }
}
