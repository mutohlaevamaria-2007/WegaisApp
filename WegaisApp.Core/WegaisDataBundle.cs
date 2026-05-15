using WegaisApp.Core.DB.Entities;

namespace WegaisApp.Core
{
    /// <summary>
    /// Пакет данных из записей трех таблиц
    /// </summary>
    public class WegaisDataBundle
    {
        public List<Producer> Producers { get; set; }
        public List<Product> Products { get; set; }
        public List<StockPosition> StockPositions { get; set; }

        public WegaisDataBundle()
        {
            Producers = new();
            Products = new();
            StockPositions = new();
        }

        public WegaisDataBundle(List<Producer> producers, List<Product> products, List<StockPosition> stockPositions)
        {
            Producers = producers;
            Products = products;
            StockPositions = stockPositions;
        }
        /// <summary>
        /// Вставить записи, удалить дубликаты
        /// </summary>
        public void UnionRange(WegaisDataBundle data)
        {
            Producers = Producers.Union(data.Producers).ToList();
            Products = Products.Union(data.Products).ToList();
            StockPositions.AddRange(data.StockPositions);
        }
    }
}
