using System.ComponentModel;

namespace WegaisApp.Core.ViewModel
{
    /// <summary>
    /// Предствление для вывода на UI
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
        public decimal Count { get; private set; }

        public StockPositionInfo(string? name, decimal capacity, decimal alcVolume, string? producerName, decimal count)
        {
            Name = name;
            Capacity = capacity;
            AlcVolume = alcVolume;
            ProducerName = producerName;
            Count = count;
        }

        /// <summary>
        /// Linq GroupJoin запрос из трех таблиц Products+StockPositions+Producers
        /// </summary>
        public static List<StockPositionInfo> PreviewQuery(WegaisDataBundle data)
        {
            return data.Products.GroupJoin(data.StockPositions,
                p => p.AlcCode,
                s => s.ProductId,
                (p, s) => new
                {
                    p.AlcCode,
                    p.FullName,
                    p.Capacity,
                    p.AlcVolume,
                    TotalCount = s.Sum(item => item.Quantity),
                    p.ProducerId
                })

                .Join(data.Producers,
                product => product.ProducerId,
                producer => producer.ClientRegId,
                (product, producer) => new StockPositionInfo
                (
                    name: product.FullName,
                    capacity: product.Capacity,
                    alcVolume: product.AlcVolume,
                    producerName: producer.ShortName,
                    count: product.TotalCount
                )).ToList();
        }
    }
}