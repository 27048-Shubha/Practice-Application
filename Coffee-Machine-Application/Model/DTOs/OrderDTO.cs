namespace Coffee_Machine_Application.Model
{
    using Coffee_Machine_Application.Enums;

    public class OrderDTO
    {
        public Guid OrderId { get; init; }
        public CoffeeType Type { get; set; }
        public QuantityRange Quantity { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime ReceivedTime { get; set; }
        public Guid VendingMachineId { get; set; }

        internal OrderDTO(Guid orderId, CoffeeType type, QuantityRange quantity, DateTime receivedTime, Guid vendingmachineId)
        {
            this.OrderId = orderId;
            this.Type = type;
            this.Quantity = quantity;
            this.ReceivedTime = receivedTime;
            this.VendingMachineId = vendingmachineId;
        }

    }
}
