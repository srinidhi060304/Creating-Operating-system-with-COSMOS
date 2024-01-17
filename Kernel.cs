using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem;
using Advik.Commands;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Advik
{
    public class Kernel : Sys.Kernel
    {
        private CommandManager commandManager;
        private CosmosVFS vfs;
        string path;
        protected override void BeforeRun()
        {
            Console.Clear();
            Console.WriteLine("Cosmos booted successfully.\n\n\n\n\n \t\t\t\t\t\t\t Welcome to 50 Shades of Tech!!!!");
            Console.WriteLine("\nEnter 'help' to access list of all available functions\n\n");
            this.commandManager = new CommandManager();
            this.vfs = new CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(vfs);
            int f = 0;
            foreach (var i in Directory.GetDirectories(@"0:\"))
            {
                if (i.Equals("Pass123"))
                {
                    var files = Directory.GetFiles(@"0:\Pass123");
                    f++;
                    break;

                }
            }
            if (f == 0)
            {
                Sys.FileSystem.VFS.VFSManager.CreateDirectory("0:\\Pass123");

                Sys.FileSystem.VFS.VFSManager.CreateFile("0:\\Pass123\\password.txt");
                Sys.FileSystem.VFS.VFSManager.CreateFile("0:\\Pass123\\readonly.txt");
                Sys.FileSystem.VFS.VFSManager.CreateFile("0:\\Pass123\\writeonly.txt");
            }
            path = "0:\\";

        }
        public string cd(string name)
        {
            if (name.Contains("0:\\"))
            {
                path = name;

            }
            else if (name.Equals("0:\\Pass123") || name.Equals("Pass123"))
            {

            }
            else if (Directory.GetDirectories(path).Contains(name))
            {
                if (path.Equals("0:\\"))
                {
                    path = path + name;
                }
                else
                {
                    path = path + "\\" + name;
                }
            }
            else if (name.Equals(".."))
            {
                String[] s = path.Split("\\");
                path = "0:";
                for (int i = 1; i < s.Length - 1; i++)
                {
                    path = path + "\\" + s[i];
                }
            }
            else
            {
                Console.WriteLine("Wrong Input");
            }
            return path;
        }
        protected override void Run()
        {
            Console.Write(path + ">");
            string Com = Console.ReadLine();
            string[] strings = Com.Split(' ');
            if (strings[0].Equals("cd"))
            {
                path = cd(strings[1]);
            }
            else
            {
                String response = this.commandManager.processInput(Com.ToLower(), path);
                Console.WriteLine(response + "\n");

            }

        }
    }
}