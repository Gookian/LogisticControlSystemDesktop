using System;

namespace LogisticControlSystemDesktop.Models
{
    public class Package
    {
        public int PackageId { get; set; }
        public int Number { get; set; }
        public DateTime BuildDeadline { get; set; }

        // Внешние ключи
        public int PackageStateId { get; set; }

        // Ссылки на объекты внешнего ключа
        public PackageState PackageState { get; set; }
    }
}
