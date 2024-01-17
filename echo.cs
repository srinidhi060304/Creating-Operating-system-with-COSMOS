using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advik.Commands
{
    public class Echo : Command
    {
        public Echo(String command) : base(command) { }

        public override String Output(String[] args)
        {
            Console.Write("Text typed: ");
            String s = "";
            foreach (String arg in args)
            {
                s = s + " " + arg;
            }
            Console.WriteLine(s);
            return "";

        }

    }
}
