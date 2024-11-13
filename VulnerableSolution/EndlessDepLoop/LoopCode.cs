using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VulnerableSolution.EndlessDepLoop
{
    internal class LoopCode
    {
        private readonly LoopManager _loopManager;
        public LoopCode()
        {
            _loopManager = new LoopManager();
        }

        public void UseLoopCode()
        {
            _loopManager.UseLoopBll();
        }
    }
}
