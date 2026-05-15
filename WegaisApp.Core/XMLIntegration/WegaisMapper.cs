using System;
using System.Xml.Serialization;
using WegaisApp.Core.DB.Entities;

namespace WegaisApp.Core.XMLIntegration
{
    /// <summary>
    /// Компонент преобразования данных из формата ЕГАИС (XML/DTO) в доменные модели приложения
    /// </summary>
    public static class WegaisMapper
    {
        /// <summary>
        /// Выполняет десериализацию XML-файла в объектную модель документов ЕГАИС.
        /// </summary>
        /// <param name="filePath">Полный путь к XML-файлу.</param>
        /// <returns>DTO-модель документов.</returns>
        public static Documents Deserialize(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            XmlSerializer serializer = new XmlSerializer(typeof(Documents));
            using FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var resultDto = (Documents?)serializer.Deserialize(fs);

            return resultDto ?? throw new InvalidOperationException($"Файл {filePath} пуст " +
                $"или имеет неправильный формат");
        }

        // ==================== МАППИНГ ====================

        /// <summary>
        /// Преобразует пакет документов ЕГАИС в список записей в БД
        /// </summary>
        /// <param name="dto">Десериализованный объект XML документа</param>
        /// <returns>Список позиций склада, готовых к сохранению в БД</returns>
        public static WegaisDataBundle MapToRows(Documents dto)
        {
            var stockPositions = dto.Document.ReplyRests_v2?.Products?.Cast<IStockPosition>()
                              ?? dto.Document.ReplyRestsShop_v2?.Products?.Cast<IStockPosition>();

            // Защита от дубликтов
            HashSet<DB.Entities.Producer> resultProducers = new();
            HashSet<Product> resultProducts = new();
            // Нет защиты от дубликтов StockPosition, т.к у DTO нет ключевого поля,
            // гарантирующего уникальность
            List<StockPosition> resultStockPositions = new();

            foreach (var stockPosition in stockPositions)
            {
                var row = MapToRow(stockPosition);

                resultProducers.Add(row.producer);
                resultProducts.Add(row.product);
                resultStockPositions.Add(row.stockPosition);
            }

            return new WegaisDataBundle
                (resultProducers.ToList(),
                resultProducts.ToList(),
                resultStockPositions);
        }

        // ---------- ВНУТРЕННИЕ МЕТОДЫ ----------

        private static (DB.Entities.Producer producer, Product product, StockPosition stockPosition) MapToRow(IStockPosition dtoStockPosition)
        {
            // Извлечение вложений DTO
            var dtoProduct = dtoStockPosition.Product;
            var dtoProducer = dtoProduct.Producer;

            // Маппинг производителя для получения его ID (ClientRegId)
            var rowProducer = MapProducerToRow(dtoProducer);
            // Маппинг продукта и установка полученного ID производителя
            var rowProduct = MapProductToRow(dtoProduct, rowProducer.ClientRegId);
            // Сброка итоговой позиции и связь с продуктом
            var rowStockPosition = MapStockPositionToRow(dtoStockPosition, rowProduct.AlcCode);

            return (rowProducer, rowProduct, rowStockPosition);
        }

        public static DB.Entities.Producer MapProducerToRow(Producer dtoProducer)
        {
            // Switch Expression для определения типа производителя
            // и выбора конструктора
            return dtoProducer switch
            {
                { TS: { } ts } => new(
                    clientRegId: ts.ClientRegId.ToString(),
                    type: "TS",
                    fullName: ts.FullName,
                    shortName: ts.ShortName,
                    country: ts.address.Country,
                    adressDescription: ts.address.description
                    ),
                { FO: { } fo } => new(
                    clientRegId: fo.ClientRegId.ToString(),
                    type: "FO",
                    fullName: fo.FullName,
                    shortName: fo.ShortName,
                    country: fo.address.Country,
                    adressDescription: fo.address.description
                    ),
                { UL: { } ul } => new(clientRegId: ul.ClientRegId.ToString(),
                    type: "UL",
                    fullName: ul.FullName,
                    shortName: ul.ShortName,
                    inn: ul.INN.ToString(),
                    kpp: ul.KPP.ToString(),
                    country: ul.address.Country,
                    regionCode: ul.address.RegionCode,
                    adressDescription: ul.address.description
                    ),
                _ => throw new ArgumentException("DTO не содержит данных о производителе")
            };
        }

        public static Product MapProductToRow(IProduct product, string producerId)
        {
            return new(
                alcCode: product.AlcCode.ToString(),
                fullName: product.FullName,
                capacity: product.Capacity,
                unitType: product.UnitType,
                alcVolume: product.AlcVolume,
                productVCode: product.ProductVCode.ToString(),
                producerId: producerId
                );
        }

        public static StockPosition MapStockPositionToRow(IStockPosition dtoPos, string productId)
        {
            // Базовые поля
            var row = new StockPosition(dtoPos.Quantity, productId);

            // Если это расширенная позиция (склад), дописываем справки
            if (dtoPos is ProductsStockPosition warehousePos)
            {
                row.InformF1RegId = warehousePos.InformF1RegId;
                row.InformF2RegId = warehousePos.InformF2RegId;
            }
            // Если это магазин, поля останутся null или default

            return row;
        }
    }
}