using System;
using System.Collections.Generic;
using System.IO;

namespace TheApplication.Model
{
    class FileManipulator
    {

        public static string ReadFile(string filePath, string FileName)
        {
            
            string fileContent = string.Empty;

            try
            {
                StreamReader StreamReader = new StreamReader(filePath + FileName);
                fileContent = StreamReader.ReadToEnd();
            }

            catch (Exception e)
            {

            }

            return fileContent;
        }

        public static bool SaveFile(string FilePath, string FileName, string FileContent, bool Append)
        {
            string pathString = Path.Combine(FilePath, FileName);

            if (File.Exists(pathString))
            {
                if (Append)
                {
                    try
                    {
                        using (FileStream FileStream = File.Open(pathString, FileMode.Open))
                        {
                            StreamReader StreamReader = new StreamReader(FileStream);
                            FileContent = StreamReader.ReadToEnd() + FileContent;
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
                else
                {
                    try
                    {
                        using (FileStream FileStream = File.Open(pathString, FileMode.Truncate))
                        {
                            StreamWriter StreamWriter = new StreamWriter(FileStream);
                            StreamWriter.Write(FileContent);
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(FilePath);
                using (FileStream FileStream = File.Create(pathString)) {
                    StreamWriter StreamWriter = new StreamWriter(FileStream);
                    StreamWriter.Write(FileContent);
                }
            }

            return true;
        }

        public static List<SEDocument> SearchDirectory(string Directory)
        {
            List<SEDocument> SEDocuments = new List<SEDocument>();
            WalkDirectoryTree(Directory, SEDocuments);
            return SEDocuments;
        }

        private static void WalkDirectoryTree(string DirectoryPath, List<SEDocument> SEDocuments)
        {
            DirectoryInfo root = new DirectoryInfo(DirectoryPath);
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;
            

            // First, process all the files directly under this folder 
            try
            {
                files = root.GetFiles("*.*");
            }

            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (FileInfo fi in files)
                {
                    using (StreamReader reader = new StreamReader(fi.FullName)) {
                        SEDocuments.Add(new SEDocument(reader.ReadToEnd()));
                    }
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    WalkDirectoryTree(dirInfo.FullName, SEDocuments);
                    
                }
            }
        }
    }
}
