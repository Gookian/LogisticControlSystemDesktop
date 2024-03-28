namespace LogisticControlSystemDesktop.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public string Number { get; set; }

        // Внешние ключи
        public int VehicleId { get; set; }

        // Ссылки на объекты внешнего ключа
        public Vehicle Vehicle { get; set; }
    }
}
