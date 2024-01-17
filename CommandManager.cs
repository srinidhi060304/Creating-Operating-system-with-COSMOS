using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace os_project.Commands
{
    public class CommandManager
    {
        private List<Command> commands;
        public CommandManager()
        {

            this.commands = new List<Command>(5);
            this.commands.Add(new Shutdown("shutdown"));
            this.commands.Add(new Reboot("reboot"));
            this.commands.Add(new Help("help"));
            this.commands.Add(new echo("echo"));
            this.commands.Add(new File("file"));
        }

        public String processInput(String str, string path)
        {
            String[] word = str.Split(' ');
            String func = word[0];
            int count = 0;
            List<String> text = new List<String>();
            foreach (String s in word)
            {
                if (count != 0)
                    text.Add(s);
                count++;
            }
            foreach (Command cmd in this.commands)
            {
                if (func.Equals("file"))
                {
                    text.Add(path);
                }
                if (cmd.order == func)
                {
                    return cmd.Output(text.ToArray());
                }
            }

            return "Wrong command entered";
        }
    }
}
