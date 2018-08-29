using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeekAndArchive
{
    class Program
    {
        public static List<FileInfo> MyFiles { get; private set; }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            ConpressFile(GetFileInfoFromFullPath(e.FullPath));
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
            ConpressFile(GetFileInfoFromFullPath(e.FullPath));
        }
        static void Main(string[] args)
        {
            var allFiles = Directory.GetFiles(args[1], args[0], SearchOption.AllDirectories);
            Console.WriteLine("Done");
            MyFiles = new List<FileInfo>();
            foreach (string File in allFiles)
            {
                Console.WriteLine("Found file in: '{0}'.", File);
                MyFiles.Add(new FileInfo(File));
            }
            foreach (var File in MyFiles)
            {
                Console.WriteLine(File.Directory);
                AddEventListeners(args[0], File.DirectoryName, File);
            }
            

            Console.ReadLine();
        }

        private static FileInfo GetFileInfoFromFullPath(string FullPath)
        {
            foreach (FileInfo File in MyFiles)
            {
                if (File.FullName == FullPath)
                {
                    return File;
                }
            }
            return null;
        }
        private static void ConpressFile(FileInfo fi)
        {
            // Get the stream of the source file.
            try
            {
                using (FileStream inFile = fi.OpenRead())
                {
                    // Prevent compressing hidden and 
                    // already compressed files.
                    if ((File.GetAttributes(fi.FullName)
                        & FileAttributes.Hidden)
                        != FileAttributes.Hidden & fi.Extension != ".gz")
                    {
                        // Create the compressed file.
                        using (FileStream outFile =
                                    File.Create(fi.FullName + ".gz"))
                        {
                            using (GZipStream Compress =
                                new GZipStream(outFile,
                                CompressionMode.Compress))
                            {
                                // Copy the source file into 
                                // the compression stream.
                                inFile.CopyTo(Compress);

                                Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                                    fi.Name, fi.Length.ToString(), outFile.Length.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex ) when (ex is UnauthorizedAccessException|| ex is IOException)
            {
                Console.WriteLine("Err");
            }
            
        }
        private static void AddEventListeners(string FileName, string Directory, FileInfo File)
        {
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Path = Directory,
                /* Watch for changes in LastAccess and LastWrite times, and
                   the renaming of files or directories. */
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                // Only watch text files.
                Filter = FileName
            };

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }
    }
}
