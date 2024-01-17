using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys = Cosmos.System;

namespace Advik.Commands
{
    public class File : Command
    {
        public File(String name) : base(name) { }
        int checkpass(string name)
        {

            FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(@"0:\Pass123\password.txt").GetFileStream();
            Byte[] data = new Byte[fs.Length];
            fs.Read(data, 0, data.Length);
            string response = Encoding.ASCII.GetString(data);
            response = response.TrimStart();
            String[] password = response.Split(" ");

            if (response.Contains(name))
            {

                Console.WriteLine("Enter password");
                Console.ForegroundColor = ConsoleColor.Black;
                string pass = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < password.Length; i++)
                {
                    if (password[i].Trim().Equals(name))
                    {
                        Console.WriteLine(i + " " + password[i]);
                        if (password[i + 1].Trim().Equals(pass))
                        {
                            Console.WriteLine("Password Entered Correctly!!Good job!!\n\n");
                            return 0;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                }
            }
            else
            {
                return 2;
            }
            return -1;

        }
        void createpass(string file, string password)
        {

            FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(@"0:\Pass123\password.txt").GetFileStream();
            Byte[] data = new Byte[fs.Length];
            fs.Read(data, 0, data.Length);
            String response = Encoding.ASCII.GetString(data) + "\n" + file + " " + password;
            string[] str = response.Split(" ");
            change(str, fs);

        }

        void readwriteonly(string type, string path, string text)
        {
            String path1;
            if (type.Equals("readonly"))
            {
                path1 = "0:\\Pass123\\readonly.txt";
            }
            else
            {
                path1 = "0:\\Pass123\\writeonly.txt";
            }

            FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(@path1).GetFileStream();
            Byte[] data = new Byte[fs.Length];
            fs.Read(data, 0, data.Length);
            String response = Encoding.ASCII.GetString(data) + "\n" + path;
            string[] str = response.Split(" ");
            change(str, fs);
            if (text != null)
            {
                FileStream fs1 = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(@path).GetFileStream();
                string[] str1 = text.Split(" ");
                change(str1, fs1);
            }
            else
            {
                return;
            }

        }

        int checkreadwrite(String file)
        {
            FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile("0:\\Pass123\\readonly.txt").GetFileStream();
            Byte[] data = new Byte[fs.Length];
            fs.Read(data, 0, data.Length);
            String response = Encoding.ASCII.GetString(data);
            if (response.Contains(file))
            {
                return 1;
            }
            FileStream fs1 = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile("0:\\Pass123\\writeonly.txt").GetFileStream();
            Byte[] data1 = new Byte[fs1.Length];
            fs1.Read(data1, 0, data1.Length);
            String response1 = Encoding.ASCII.GetString(data1);
            if (response1.Contains(file))
            {
                return 2;
            }
            return 0;
        }

        void delreadwrite(String file)
        {
            FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(@"0:\Pass123\readonly.txt").GetFileStream();
            Byte[] data = new Byte[fs.Length];
            fs.Read(data, 0, data.Length);
            String response = Encoding.ASCII.GetString(data);
            if (response.Contains(file))
            {
                response.Replace(file, "");
                String[] s = response.Split(" ");
                change(s, fs);
            }
            FileStream fs1 = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(@"0:\Pass123\writeonly.txt").GetFileStream();
            Byte[] data1 = new Byte[fs1.Length];
            fs1.Read(data1, 0, data1.Length);
            String response1 = Encoding.ASCII.GetString(data1);
            if (response1.Contains(file))
            {
                response1.Replace(file, "");
                String[] s = response1.Split(" ");
                change(s, fs1);
            }

        }
        String getpass(string file)
        {
            FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(@"0:\Pass123\password.txt").GetFileStream();
            Byte[] data = new Byte[fs.Length];
            fs.Read(data, 0, data.Length);
            string response = Encoding.ASCII.GetString(data);
            response = response.TrimStart();
            String[] password = response.Split(" ");
            for (int i = 0; i < password.Length; i++)
            {
                if (password[i].Trim().Equals(file))
                {
                    return password[i + 1];
                }
            }
            return null;
        }

        void deletepass(string file, string password)
        {

            FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(@"0:\Pass123\password.txt").GetFileStream();
            Byte[] data = new Byte[fs.Length];
            fs.Read(data, 0, data.Length);
            String response = Encoding.ASCII.GetString(data);
            String[] s = response.Split(" ");
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Trim().Equals(file))
                {
                    if (s[i + 1].Trim().Equals(password))
                    {
                        s[i] = "";
                        s[i + 1] = "";
                        break;
                    }
                }
            }
            Sys.FileSystem.VFS.VFSManager.DeleteFile("0:\\Pass123\\password.txt");
            Sys.FileSystem.VFS.VFSManager.CreateFile("0:\\Pass123\\password.txt");
            FileStream fs2 = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(@"0:\Pass123\password.txt").GetFileStream();
            change(s, fs2);
        }

        public int check(String name)
        {
            String[] l = name.Split("\\");
            String name2 = l[0];
            for (int i = 1; i < l.Length - 1; i++)
            {
                name2 += "\\" + l[i];
            }
            int f = 0;
            foreach (var s in Directory.GetFiles(@name2))
            {
                if (s == l[l.Length - 1])
                {
                    f = 1;
                }
            }
            return f;
        }

        public void change(String[] args, FileStream fs)
        {
            if (fs.CanWrite)
            {
                StringBuilder sb = new StringBuilder();


                foreach (String s in args)
                {
                    if (s.Equals("\\n"))
                    {
                        sb.Append("\n");
                    }
                    else if (s.Equals(""))
                    {
                        continue;
                    }
                    else
                    {
                        sb.Append(s + " ");
                    }
                }

                Byte[] data = Encoding.ASCII.GetBytes(sb.ToString());
                foreach (byte i in data)
                {
                    fs.WriteByte(i);
                }
                fs.Close();
                Console.WriteLine("Text written successfully! Yay!!\n");
            }
            else
            {
                Console.WriteLine("File not Readable!\n");
            }
        }
        public override String Output(String[] args)
        {
            //file create MyFile.txt
            String response = "";
            String path = args[args.Length - 1];
            if (path.Equals("0:\\"))
            {

            }
            else
            {
                path = path + "\\";
            }
            switch (args[0])
            {
                case "create":
                    try
                    {

                        String name = args[1];
                        String name1 = path + name;
                        int f = check(name1);
                        if (f == 1)
                        {
                            Console.WriteLine("File Already Present!\n");
                            break;
                        }
                        while (true)
                        {
                            Sys.FileSystem.VFS.VFSManager.CreateFile(name1);
                            Console.Write("You want a password(y/n)??: ");
                            String s1 = Console.ReadLine();
                            if (s1.Equals("y"))
                            {
                                Console.WriteLine("Enter Password");
                                Console.ForegroundColor = ConsoleColor.Black;
                                String pass = Console.ReadLine();
                                Console.ForegroundColor = ConsoleColor.White;
                                createpass(name, pass);
                                break;

                            }
                            else if (s1.Equals("n"))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Wrong input! Try again!\n");
                            }

                        }
                        int c = 0;

                        while (true)
                        {
                            Console.Write("Should the file be readonly(y/n)??: ");
                            String s1 = Console.ReadLine();
                            if (s1.Equals("y"))
                            {

                                Console.WriteLine("Enter text for the file before locking option to edit\n");
                                String pass1 = Console.ReadLine();
                                readwriteonly("readonly", name1, pass1);
                                c = 1;
                                break;

                            }
                            else if (s1.Equals("n"))
                            {
                                Console.WriteLine("File can be editted!\n");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Wrong input! Try again!\n");
                            }
                        }
                        if (c == 0)
                        {
                            while (true)
                            {
                                Console.Write("Should the file be writeonly(y/n)??: ");
                                String s1 = Console.ReadLine();
                                if (s1.Equals("y"))
                                {

                                    readwriteonly("writeonly", name1, null);
                                    c = 1;
                                    break;

                                }
                                else if (s1.Equals("n"))
                                {
                                    Console.WriteLine("File can be read!\n");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Wrong input! Try again!\n");
                                }
                            }
                        }

                        response = "Your file" + args[1] + " was successfully created!\n";
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;
                    }

                    break;

                case "delete":
                    try
                    {
                        String name = args[1];
                        String name1 = path + name;
                        int f = check(name1);
                        if (f == 0)
                        {
                            Console.WriteLine("Uh oh! File not available!\n");
                            break;
                        }
                        String p = "";
                        while (true)
                        {
                            int i = checkpass(name);
                            if (i == 2)
                            {
                                break;
                            }
                            else if (i == 0)
                            {
                                p = getpass(name);
                            }
                            else
                            {
                                Console.WriteLine("Oops...Wrong password try again!\n");
                                continue;
                            }
                        }

                        deletepass(name, p);
                        delreadwrite(name1);
                        Sys.FileSystem.VFS.VFSManager.DeleteFile(name1);
                        response = "Your file" + args[1] + " was successfully removed!\n";
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;
                    }

                    break;

                case "createdir":
                    try
                    {
                        String name = args[1];
                        String name1 = path + name;
                        Sys.FileSystem.VFS.VFSManager.CreateDirectory(name1);
                        response = "Sucessfully created directory" + args[1];
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;
                    }
                    break;
                case "deldir":
                    try
                    {
                        String name = args[1];
                        String name1 = path + name;
                        Sys.FileSystem.VFS.VFSManager.DeleteDirectory(name1, true);
                        response = "Sucessfully remove directory" + args[1];
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;
                    }
                    break;
                case "writestr":
                    try
                    {
                        if (args[1].Equals("-e"))
                        {
                            String name = args[2];
                            String name1 = path + name;
                            int f = check(name1);
                            if (f == 0)
                            {
                                Console.WriteLine("Uh oh! File not available!\n");
                                break;
                            }
                            int rw = checkreadwrite(name1);
                            if (rw == 1)
                            {
                                Console.WriteLine("File is only readble!\n");
                                break;
                            }
                            while (true)
                            {
                                int i = checkpass(name);
                                if (i == 0 || i == 2)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Oops...Wrong password try again!\n");
                                    continue;
                                }
                            }
                            FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(name1).GetFileStream();
                            Byte[] data = new Byte[fs.Length];
                            fs.Read(data, 0, data.Length);
                            response = Encoding.ASCII.GetString(data);
                            if (rw != 2)
                            {
                                Console.WriteLine("Currently Present in the file\n");
                                Console.WriteLine(response);
                            }
                            Console.WriteLine("\nStart with a \\n to start with a new line or start typing now to continue from current line\n");
                            String s = Console.ReadLine();
                            string[] str = s.Split(" ");
                            change(str, fs);
                            break;
                        }
                        else if (args[1].Equals("-o"))
                        {
                            String name = args[2];
                            String name1 = path + name;
                            int f = check(name1);
                            if (f == 0)
                            {
                                Console.WriteLine("Uh oh! File not available!\n");
                                break;
                            }
                            int rw = checkreadwrite(name1);
                            if (rw == 1)
                            {
                                Console.WriteLine("File is only readble!\n");
                                break;
                            }
                            while (true)
                            {
                                int i = checkpass(name);
                                if (i == 0 || i == 2)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Oops...Wrong password try again!\n");
                                    continue;
                                }
                            }
                            Sys.FileSystem.VFS.VFSManager.DeleteFile(name1);
                            Sys.FileSystem.VFS.VFSManager.CreateFile(name1);
                            Console.WriteLine("Enter your text in the text file and to go to next line type \"\\n\" preceeded and followed by a \' \'(space) : \n");
                            String s = Console.ReadLine();
                            string[] str = s.Split(" ");
                            FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(name1).GetFileStream();
                            change(str, fs);
                            break;
                        }
                        else if (args[1].Equals("-r"))
                        {
                            String name = args[2];
                            String name1 = path + name;
                            int f = check(name1);
                            if (f == 0)
                            {
                                Console.WriteLine("Uh oh! File not available!\n");
                                break;
                            }
                            int rw = checkreadwrite(name1);
                            if (rw == 1)
                            {
                                Console.WriteLine("File is only readble!\n");
                                break;
                            }
                            while (true)
                            {
                                int i = checkpass(name);
                                if (i == 0 || i == 2)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Oops...Wrong password try again!\n");
                                    continue;
                                }
                            }
                            FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(name1).GetFileStream();
                            Byte[] data = new Byte[fs.Length];
                            fs.Read(data, 0, data.Length);
                            response = Encoding.ASCII.GetString(data);
                            if (rw != 2)
                            {
                                Console.WriteLine("Currently Present in the file\n");
                                Console.WriteLine(response);
                            }
                            Console.WriteLine("\n\nWarning: If You enter a single word and that word occurs multiple times in the text, it will be replaced everywhere.");
                            Console.WriteLine("Thus to prevent this type the word you want to replace along with a follow up words for the position of the words you want to replace till unique\n");
                            Console.WriteLine("Text to replace:");
                            String s = Console.ReadLine();
                            if (response.Contains(s))
                            {
                                Console.WriteLine("Text to replace with: ");
                                String s1 = Console.ReadLine();
                                response = response.Replace(s, s1);
                                Sys.FileSystem.VFS.VFSManager.DeleteFile(name1);
                                Sys.FileSystem.VFS.VFSManager.CreateFile(name1);
                                FileStream fs2 = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(name1).GetFileStream();
                                change(response.Split(" "), fs2);
                            }
                            else
                            {
                                Console.WriteLine("Entered Text/word to be changed is not present in the file\n");
                                break;
                            }

                            break;
                        }
                        else if (args[1].Equals("-d"))
                        {
                            String name = args[2];
                            String name1 = path + name;
                            int f = check(name1);
                            if (f == 0)
                            {
                                Console.WriteLine("uh oh! File not available!\n");
                                break;
                            }
                            int rw = checkreadwrite(name1);
                            if (rw == 1)
                            {
                                Console.WriteLine("File is only readble!\n");
                                break;
                            }
                            while (true)
                            {
                                int i = checkpass(name);
                                if (i == 0 || i == 2)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Oops...Wrong password try again!\n");
                                    continue;
                                }
                            }
                            FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(name1).GetFileStream();
                            Byte[] data = new Byte[fs.Length];
                            fs.Read(data, 0, data.Length);
                            response = Encoding.ASCII.GetString(data);
                            if (rw != 2)
                            {
                                Console.WriteLine("Currently Present in the file\n");
                                Console.WriteLine(response);
                            }
                            Console.WriteLine("\n\nWarning: If You enter a single word and that word occurs multiple times in the text, it will be deleted everywhere.");
                            Console.WriteLine("Thus to prevent this use \"file writestr -r <filename>\" to replace that exact word with a \"\"\n");
                            f = 0;
                            while (true)
                            {
                                Console.Write("Are you sure you want to continue(y/n): ");
                                String s1 = Console.ReadLine();
                                if (s1.Equals("y"))
                                {
                                    break;
                                }
                                else if (s1.Equals("n"))
                                {
                                    f = 2;
                                }
                                else
                                {
                                    Console.WriteLine("Wrong input! Try again!\n");
                                }

                            }
                            if (f == 2)
                            {
                                break;
                            }
                            Console.WriteLine("Text to delete: ");
                            String s = Console.ReadLine();
                            if (response.Contains(s))
                            {
                                response = response.Replace(s, "");
                                Sys.FileSystem.VFS.VFSManager.DeleteFile(name1);
                                Sys.FileSystem.VFS.VFSManager.CreateFile(name1);
                                FileStream fs2 = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(name1).GetFileStream();
                                change(response.Split(" "), fs2);
                            }
                            else
                            {
                                Console.WriteLine("Entered Text/word to be changed is not present in the file\n");
                                break;
                            }

                            break;
                        }
                        else
                        {
                            String name = args[1];
                            String name1 = path + name;
                            int f = check(name1);
                            if (f == 0)
                            {
                                Console.WriteLine("Uh oh! File not available!\n");
                                break;
                            }
                            int rw = checkreadwrite(name1);
                            if (rw == 1)
                            {
                                Console.WriteLine("File is only readble!\n");
                                break;
                            }
                            while (true)
                            {
                                int i = checkpass(name);
                                if (i == 0 || i == 2)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Oops...wrong password try again!\n");
                                    continue;
                                }
                            }
                            Sys.FileSystem.VFS.VFSManager.DeleteFile(name1);
                            Sys.FileSystem.VFS.VFSManager.CreateFile(name1);
                            Console.WriteLine("Enter your text in the text file and to go to next line type \"\\n\" preceeded and followed by a \' \'(space) : \n");
                            String s = Console.ReadLine();
                            string[] str = s.Split(" ");
                            FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(name1).GetFileStream();
                            change(str, fs);
                            response = "";
                            break;

                        }
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;
                    }
                case "readstr":
                    try
                    {
                        String name = args[1];
                        String name1 = path + name;
                        int f = check(name1);
                        if (f == 0)
                        {
                            Console.WriteLine("Uh oh! File not available!\n");
                            break;
                        }
                        int rw = checkreadwrite(name1);
                        if (rw == 2)
                        {
                            Console.WriteLine("File is only writable!\n");
                            break;
                        }
                        while (true)
                        {
                            int i = checkpass(name);
                            if (i == 0 || i == 2)
                            {
                                Console.WriteLine("File accessed successfully! Yay!\n");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Oops...Wrong password try again!\n");
                                continue;
                            }
                        }
                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(name1).GetFileStream();
                        if (fs.CanRead)
                        {
                            Byte[] data = new Byte[fs.Length];
                            fs.Read(data, 0, data.Length);
                            response = Encoding.ASCII.GetString(data);

                        }
                        else
                        {
                            response = "Unable to read from the file! Not open for reading!\n";
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;
                    }
                    break;

                case "dirlist":
                    var files_list = Directory.GetFiles(@path);
                    var directory_list = Directory.GetDirectories(@path);
                    Console.WriteLine("Name\t\t\t\t\tSize");
                    foreach (var directory in directory_list)
                    {
                        if (directory.Equals("Pass123"))
                        {
                            continue;
                        }
                        Console.WriteLine(directory);
                    }
                    foreach (var file in files_list)
                    {
                        Console.WriteLine(file + "\t\t\t\t\t" + file.Length);
                    }

                    break;

                case "rename":
                    try
                    {

                        String name = args[1];
                        String name1 = path + name;
                        int f = check(name1);
                        int rw = checkreadwrite(name1);
                        if (f == 0)
                        {
                            Console.WriteLine("Uh oh! File not available!\n");
                            break;
                        }
                        while (true)
                        {
                            int i = checkpass(name);
                            if (i == 0 || i == 2)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Oops...Wrong password try again!\n");
                                continue;
                            }
                        }
                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(name1).GetFileStream();
                        Byte[] data = new Byte[fs.Length];
                        fs.Read(data, 0, data.Length);
                        response = Encoding.ASCII.GetString(data);
                        Console.WriteLine("Enter new name of the file with extension");
                        String name3 = Console.ReadLine();
                        string name2 = path + name3;
                        f = check(name2);
                        if (f == 1)
                        {
                            Console.WriteLine("File Already Present available");
                            break;
                        }
                        Sys.FileSystem.VFS.VFSManager.DeleteFile(name1);
                        delreadwrite(name1);
                        Sys.FileSystem.VFS.VFSManager.CreateFile(name2);
                        if (rw == 1)
                        {
                            readwriteonly("readonly", name2, response);
                        }
                        else if (rw == 2)
                        {
                            readwriteonly("writeonly", name2, response);
                        }
                        string[] str = response.Split(" ");
                        FileStream fs1 = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(name2).GetFileStream();
                        change(str, fs1);
                        response = "";
                        break;
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;
                    }

                case "copy":
                    try
                    {

                        String name = args[1];
                        String name2 = args[2];
                        String name1 = path + name;
                        String name3 = path + name2;
                        int f = check(name1);
                        if (f == 0)
                        {
                            Console.WriteLine("Uh oh! File " + name + " not available!\n");
                            break;
                        }
                        while (true)
                        {
                            int i = checkpass(name);
                            if (i == 0 || i == 2)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Oops...Wrong password try again!\n");
                                continue;
                            }
                        }
                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(name1).GetFileStream();
                        Byte[] data = new Byte[fs.Length];
                        fs.Read(data, 0, data.Length);
                        response = Encoding.ASCII.GetString(data);
                        f = check(name3);
                        if (f == 1)
                        {
                            Console.Write("File Already Present. Do you want to overwrite any data that is there in the file?(y/n)\n");
                            String s1 = Console.ReadLine();
                            while (true)
                            {

                                if (s1.Equals("y"))
                                {
                                    Sys.FileSystem.VFS.VFSManager.DeleteFile(name2);
                                    break;
                                }
                                else if (s1.Equals("n"))
                                {
                                    f = 2;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Wrong input! Try again!\n");
                                }
                                if (f == 2)
                                {
                                    break;
                                }
                            }
                        }

                        Sys.FileSystem.VFS.VFSManager.CreateFile(name3);

                        string[] str = response.Split(" ");
                        FileStream fs1 = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(name3).GetFileStream();
                        change(str, fs1);
                        response = "";
                        break;
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;
                    }
                case "move":
                    try
                    {

                        String name = args[1];
                        String name2 = args[2];
                        String name1 = path + name;
                        String name3 = name2 + name;
                        int f = check(name1);
                        if (f == 0)
                        {
                            Console.WriteLine("Uh oh! File " + name + " not available!\n");
                            break;
                        }
                        while (true)
                        {
                            int i = checkpass(name);
                            if (i == 0 || i == 2)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Oops...Wrong password try again!\n");
                                continue;
                            }
                        }
                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(name1).GetFileStream();
                        Byte[] data = new Byte[fs.Length];
                        fs.Read(data, 0, data.Length);
                        response = Encoding.ASCII.GetString(data);
                        f = check(name3);
                        if (f == 1)
                        {
                            Console.Write("File Already Present. Do you want to overwrite any data that is there in the file?(y/n)\n");
                            String s1 = Console.ReadLine();
                            while (true)
                            {

                                if (s1.Equals("y"))
                                {
                                    Sys.FileSystem.VFS.VFSManager.DeleteFile(name3);
                                    break;
                                }
                                else if (s1.Equals("n"))
                                {
                                    f = 2;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Wrong input! Try again!\n");
                                }
                                if (f == 2)
                                {
                                    break;
                                }
                            }
                        }

                        Sys.FileSystem.VFS.VFSManager.CreateFile(name3);
                        int rw = checkreadwrite(name1);
                        if (rw == 1)
                        {
                            readwriteonly("readonly", name3, response);
                        }
                        else if (rw == 2)
                        {
                            readwriteonly("writeonly", name3, response);
                        }
                        Sys.FileSystem.VFS.VFSManager.DeleteFile(name1);
                        string[] str = response.Split(" ");
                        FileStream fs1 = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(name2).GetFileStream();
                        change(str, fs1);
                        response = "";
                        break;
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;
                    }
                case "search":
                    try
                    {

                        String name = args[1];
                        int f = 0;
                        if (name.Contains("0:\\"))
                        {
                            f = check(name);
                        }
                        else
                        {
                            name = path + name;
                            f = check(name);
                        }
                        if (f == 0)
                        {
                            Console.WriteLine("Uh oh! File not available!\n");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("File Present in " + name);
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;
                    }

                default:
                    response = "Unexpected argument:" + args[0];
                    break;
            }
            return response;
        }
    }
}