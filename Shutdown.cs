using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace os_project.Commands
{
    public class Shutdown: Command
    {
        public Shutdown(String command) : base(command) { }

        public override String Output(String[] args)
        {
            Cosmos.System.Power.Shutdown();
            return "";
        }
    }
}
