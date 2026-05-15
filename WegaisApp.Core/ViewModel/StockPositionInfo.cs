using System.ComponentModel;

namespace WegaisApp.Core.ViewModel
{
    /// <summary>
    /// Предствление таблицы для вывода на UI
    /// </summary>
    public class StockPositionInfo
    {
        [DisplayName("Наименование продукта")]
        public string? Name { get; private set; }
        [DisplayName("Объем")]
        public decimal Capacity { get; private set; }
        [DisplayName("Крепость алк.")]
        public decimal AlcVolume { get; private set; }
        [DisplayName("Производитель")]
        public string? ProducerName { get; private set; }
        [DisplayName("Всего шт. на складе")]
        public int Count { get; private set; }

        public StockPositionInfo(string? name, decimal capacity, decimal alcVolume, string? producerName, int count)
        {
            Name = name;
            Capacity = capacity;
            AlcVolume = alcVolume;
            ProducerName = producerName;
            Count = count;
        }

        //public static List<StockPositionInfo> SHIIT(WegaisDataBundle data)
        //{
        //    List<StockPositionInfo> result = new();

        //    foreach(StockPosition stockPosition in data.StockPositions)
        //    {
        //        StockPositionInfo item = new(
        //            name: stockPosition.Product.FullName,
        //            capacity: stockPosition.Product.Capacity,
        //            alcVolume: stockPosition.Product.AlcVolume,
        //            producerName: stockPosition.Product.Producer.ShortName,
        //            count: -1 // Загулшка
        //            );

        //        result.Add(item); 
        //    }

        //    return result;
        //}

        //public static List<StockPositionInfo> PreviewQuery(WegaisDataBundle data)
        //{
        //    var result = from s in data.StockPositions
        //                 join p in data.Products on s.ProductId equals p.ProducerId
        //                 join pp in data.Producers on p.ProducerId equals pp.ClientRegId
        //                 select new {
        //                     name = p.FullName,
        //                     capacity = p.Capacity,
        //                     alcVolume = p.AlcVolume,
        //                     producerName = pp.ShortName,
        //                     count = s.Quantity.Sum()
        //                 };
        //}
    }
}