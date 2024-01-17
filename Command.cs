using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advik.Commands
{
    public class Command
    {
        public readonly String order;
        public Command(String order) { this.order = order; }

        public virtual String Output(String[] args)
        {
            return " ";
        }

    }
}
