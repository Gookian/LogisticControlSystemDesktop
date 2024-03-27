namespace LogisticControlSystemDesktop.Models
{
    public class PackageСontent
    {
        public int PackageСontentId { get; set; }

        // Внешние ключи
        public int PackageId { get; set; }
        public int ProductId { get; set; }

        // Ссылки на объекты внешнего ключа
        public Package Package { get; set; }
        public Product Product { get; set; }
    }
}
