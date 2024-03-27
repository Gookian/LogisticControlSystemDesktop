namespace LogisticControlSystemDesktop.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        // Внешние ключи
        public int ProductDataId { get; set; }
        public int ProductStateId { get; set; }

        // Ссылки на объекты внешнего ключа
        public ProductData ProductData { get; set; }
        public ProductState ProductState { get; set; }
    }
}
