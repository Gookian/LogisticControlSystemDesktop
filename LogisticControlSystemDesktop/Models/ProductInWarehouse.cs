namespace LogisticControlSystemDesktop.Models
{
    public class ProductInWarehouse
    {
        public int ProductInWarehouseId { get; set; }

        // Внешние ключи
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }

        // Ссылки на объекты внешнего ключа
        public Warehouse Warehouse { get; set; }
        public Product Product { get; set; }
    }
}
