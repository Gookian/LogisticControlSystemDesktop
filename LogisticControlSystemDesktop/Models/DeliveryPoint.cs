namespace LogisticControlSystemDesktop.Models
{
    public class DeliveryPoint
    {
        public int DeliveryPointId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
