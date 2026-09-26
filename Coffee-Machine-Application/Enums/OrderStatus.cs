namespace Coffee_Machine_Application.Enums
{
    public enum OrderStatus
    {
        Received = 1,
        Sourcing,
        Preparing,
        WaitingForVendingMachine,
        WaitingForIngredients,
        Delivered,
        Cancelled,
        Failed,
        Paused,
    }
}
