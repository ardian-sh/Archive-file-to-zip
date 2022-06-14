using System;
using System.IO;
using System.IO.Compression;

namespace FileArchive
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //your directory, example directory in D:\\diretory
                const string directory = "D:\\diretory";
                DirectoryInfo directoryInfo = new(directory);

                FileInfo[] fileInfo = directoryInfo.GetFiles();
                if (fileInfo.Length > 0)
                {
                    Console.WriteLine("Archive file proccess");
                    //name file zip
                    string nameFileZip = Path.Combine(directory, DateTime.Now.ToString("dd-MM-yyyyy") + ".zip");

                    using (FileStream zipCreate = new(nameFileZip, FileMode.Create))
                    {
                        using (ZipArchive zipArchive = new(zipCreate, ZipArchiveMode.Create))
                        {
                            foreach (FileInfo file in fileInfo)
                            {
                                zipArchive.CreateEntryFromFile(file.FullName, file.Name);
                                Console.WriteLine("Archive file " + file.Name + " success");
                            }                  
                        }
                    }

                    //if you want to delete files that have been archived
                    Array.ForEach(fileInfo, fi => fi.Delete());

                    Console.WriteLine("Archive file finish");
                }
                else
                {
                    Console.WriteLine("File is empty");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Archive file error ", e.Message);
            }
            
            Console.ReadKey();
        }
    }
}
