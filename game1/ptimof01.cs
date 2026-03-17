using pwned;
using vano123123;
using Franik747;
using delta;
using bobeko;

namespace ptimof01
{
    static void SaveGame()
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktop, "Save.txt");
            string content;
            File.WriteAllText(filePath, content);
           // 
        }//
}