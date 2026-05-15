using System;
using System.Collections.Generic;
using WegaisApp.Core.ViewModel;

namespace WegaisApp.Core.DB.Entities
{
    /// <summary>
    /// Остаток партии продукции на складе
    /// </summary>
    public class StockPosition : BaseViewModel
    {
        private decimal _quantity;
        private string? _informF1RegId = null!;
        private string? _informF2RegId = null!;
        private string _productId = null!;

        /// <summary>
        /// PK.<br/>
        /// Внутренний идентификатор записи в базе данных
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Текущий остаток продукции на складе в единицах измерения (шт.)
        /// </summary>
        public decimal Quantity { get => _quantity; set { _quantity = value; OnPropertyChanged(); } }
        /// <summary>
        /// Регистрационный номер справки 1 (информация о фиксации партии в ЕГАИС)<br/>
        /// null для ReplyRestsShop файла
        /// </summary>
        public string? InformF1RegId { get => _informF1RegId; set { _informF1RegId = value; OnPropertyChanged(); } }
        /// <summary>
        /// Регистрационный номер справки 2 (идентификатор текущего движения партии)<br/>
        /// null для ReplyRestsShop файла
        /// </summary>
        public string? InformF2RegId { get => _informF2RegId; set { _informF2RegId = value; OnPropertyChanged(); } }
        /// <summary>
        /// FK.<br/>
        /// Алкокод продукции (связь с таблицей Product)
        /// </summary>
        public string ProductId { get => _productId; set { _productId = value; OnPropertyChanged(); } }
        /// <summary> 
        /// Навигационное свойство для доступа к характеристикам напитка
        /// </summary>
        public Product Product { get; set; } = null!;

        // Конструкто для EF Core
        public StockPosition() { }

        public StockPosition(decimal quantity, string informF1RegId, string informF2RegId, string productId)
        {
            Quantity = quantity;
            InformF1RegId = informF1RegId;
            InformF2RegId = informF2RegId;
            ProductId = productId;
        }

        public StockPosition(decimal quantity, string productId)
        {
            Quantity = quantity;
            ProductId = productId;
        }

        // Перезаписан хеш-код, т.к равенство бизнес-сущности определяет равенство PK,
        // а не равенство ссылки
        /// <summary>
        /// Равенство определяется по PK - Id
        /// </summary>
        public override int GetHashCode() => HashCode.Combine(Id);
        public override bool Equals(object? obj) =>
            obj is StockPosition other && other.Id == Id;
    }
}
