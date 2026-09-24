using Coffee_Machine_Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Machine_Application.Model
{
    public class Order
    {
        public Order()
        {
            this.OnStatusChanged += Program.PrintStatus;
        }

        public delegate void StatusChanged(OrderStatus status);
        public event StatusChanged OnStatusChanged;

        private OrderStatus _status;
        public Guid OrderId { get; init; }
        public User Customer { get; set; }
        public CoffeeType CoffeeType { get; set; }
        public QuantityRange Quantity { get; set; }
        public OrderStatus Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;
                OnStatusChanged.Invoke(_status);
            }
        }

        public DateTime ReceivedTime { get; set; }
        public DateTime SourcingEndTime { get; set; }
        public DateTime ProcessingEndTime { get; set; }
        public DateTime DeliveredTime { get; set; }

        public Guid VendingMachineId { get; set; }
    }
}
