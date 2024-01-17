using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advik.Commands
{
    public class Reboot : Command
    {
        public Reboot(String command) : base(command) { }

        public override String Output(String[] args)
        {
            Cosmos.System.Power.Reboot();
            return "";
        }
    }
}