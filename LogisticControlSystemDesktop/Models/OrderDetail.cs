namespace LogisticControlSystemDesktop.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int Count { get; set; }

        // Внешние ключи
        public int OrderId { get; set; }
        public int ProductDataId { get; set; }

        // Ссылки на объекты внешнего ключа
        public Order Order { get; set; }
        public ProductData ProductData { get; set; }
    }
}
