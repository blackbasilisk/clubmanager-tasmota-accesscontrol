using System.Threading;
using System.IO;
using System;

namespace SM.ClubManager.Library.Base.Infrastructure
{
    public class FileHelper
    {
        EventlogHelper eventLogHelper;

        public FileHelper()
        {
            eventLogHelper = new EventlogHelper();
        }

        public void SaveFile(string filePath, string content)
        {
            //FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);

            //fs.Write();
            //try
            //{

            //}
            //catch (Exception)
            //{
                
            //    throw;
            //}
            //File.AppendAllText(filePath, content, Encoding.UTF8);                       
        }
        public static void DeleteAllFilesInFolder(string path, bool isIgnoreFileInUse)
        {
            foreach (var item in Directory.GetFiles(path))
            {
                try
                {
                    File.Delete(item);
                }
                catch (IOException ioEx)
                {
                    if (isIgnoreFileInUse)
                        continue;
                    else
                        throw;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

    }
}
