using Coffee_Machine_Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Machine_Application.Service
{
    internal class MachineService
    {
        public void PowerOff(CoffeeMachine machine)
        {
            machine.IsPowerOff = true;
        }
        public void PowerOn(CoffeeMachine machine)
        {
            machine.IsPowerOff = true;
        }
    }
}
