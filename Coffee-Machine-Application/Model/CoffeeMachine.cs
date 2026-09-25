using Coffee_Machine_Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Machine_Application.Model
{
    public class CoffeeMachine
    {
        public delegate void MachineStatusChange(CoffeeMachine machine, Guid orderId);
        public event MachineStatusChange OnMachineStatusChange;
        public CoffeeMachine(int id)
        {
            this.Id = id;
            this.OnMachineStatusChange += Program.PrintMachineStatus;
        }

        public int Id { get; set; }

        public Guid CurrentOrderId { get; set; }

        private MachineStatus _status;
        public MachineStatus Status 
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;
                OnMachineStatusChange?.Invoke(this, CurrentOrderId);
            }
        }
    }
}
