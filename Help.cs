using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advik.Commands
{
    public class Help : Command
    {
        public Help(String command) : base(command) { }

        public override String Output(String[] args)
        {
            Console.WriteLine(" welcome to 50 shades of Tech");
            Console.WriteLine("This is the help module! Given Below are the available input commands and thier description");
            Console.WriteLine("\nType 'echo <text>' to repeat what text is entered");
            Console.WriteLine("Type 'file remdir <directory name>' to delete a directory\n");
            Console.WriteLine("Type 'file createdir <directory name>' to delete a directory\n");
            Console.WriteLine("Type 'file create <filename with path>' to create a file\n");
            Console.WriteLine("Type 'file delete <filename with path>' to delete a file\n");
            Console.WriteLine("To Write\\edit a file\n");
            Console.WriteLine("->type 'file create -o <filename with path>' to overwrite the data in the file (will work even if '-o' is not written");
            Console.WriteLine("->type 'file create -e <filename with path>' to edit the data in the file ");
            Console.WriteLine("->type 'file create -r <filename with path>' to replace any data in the file with a new data");
            Console.WriteLine("->type 'file create -d <filename with path>' to delete specefic data in the file ");
            Console.WriteLine("Type 'file readstr <filename with path>' to read a file\n");
            Console.WriteLine("Type 'file rename <filename with path> to 0rename the file");
            Console.WriteLine("Type 'file copy <filename with path of src> <filename with path of dest> to rename the file");
            Console.WriteLine("Type 'shutdown' to shutdown\n");
            Console.WriteLine("Type 'reboot' to reboot\n");
            return "";
        }
    }
}