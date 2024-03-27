using System;

namespace LogisticControlSystemDesktop.Models
{
    public class Delivery
    {
        public int DeliveryId { get; set; }
        public DateTime DateTimeLoad { get; set; }

        // Внешние ключи
        public int WarehouseId { get; set; }
        public int DeliveryPointId { get; set; }
        public int FlightId { get; set; }
        public int PackageId { get; set; }

        // Ссылки на объекты внешнего ключа
        public Warehouse Warehouse { get; set; }
        public DeliveryPoint DeliveryPoint { get; set; }
        public Flight Flight { get; set; }
        public Package Package { get; set; }
    }
}
