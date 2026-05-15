using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WegaisApp.Core.ViewModel;

namespace WegaisApp.Core.DB.Entities
{
    /// <summary>
    /// Вид алкогольной продукции
    /// </summary>
    public class Product : BaseViewModel
    {
        private string _fullName = null!;
        private decimal _capacity;
        private string _unitType = null!;
        private decimal _alcVolume;
        private string _productVCode = null!;
        private string _producerId = null!;

        /// <summary>
        /// PK.<br/>
        /// Уникальный код алкогольной продукции в ЕГАИС (Алкокод)
        /// </summary>
        [Key] public string AlcCode { get; set; } = null!;
        /// <summary>
        /// Полное торговое наименование продукции
        /// </summary>
        public string FullName { get => _fullName; set { _fullName = value; OnPropertyChanged(); } }
        /// <summary>
        /// Емкость тары в литрах
        /// </summary>
        public decimal Capacity { get => _capacity; set { _capacity = value; OnPropertyChanged(); } }
        /// <summary>
        /// Тип единицы измерения (например, фасованная)
        /// </summary>
        public string UnitType { get => _unitType; set { _unitType = value; OnPropertyChanged(); } }
        /// <summary>
        /// Содержание этилового спирта (крепость напитка) в процентах
        /// </summary>
        public decimal AlcVolume { get => _alcVolume; set { _alcVolume = value; OnPropertyChanged(); } }
        /// <summary>
        /// Трехзначный код классификации вида алкогольной продукции
        /// </summary>
        public string ProductVCode { get => _productVCode; set { _productVCode = value; OnPropertyChanged(); } }
        /// <summary>
        /// FK.<br/>
        /// FSRAR_ID производителя
        /// </summary>
        public string ProducerId { get => _producerId; set { _producerId = value; OnPropertyChanged(); } }
        /// <summary>
        /// Навигационное свойство для доступа к данным производителя
        /// </summary>
        public Producer Producer { get; set; } = null!;

        // Конструкто для EF Core
        public Product() { }

        public Product(string alcCode, string fullName, decimal capacity, string unitType,
            decimal alcVolume, string productVCode, string producerId)
        {
            AlcCode = alcCode;
            FullName = fullName;
            Capacity = capacity;
            UnitType = unitType;
            AlcVolume = alcVolume;
            ProductVCode = productVCode;
            ProducerId = producerId;
        }

        // Перезаписан хеш-код, т.к равенство бизнес-сущности определяет равенство PK,
        // а не равенство ссылки
        /// <summary>
        /// Равенство определяется по PK - AlcCode
        /// </summary>
        public override int GetHashCode() => HashCode.Combine(AlcCode);
        public override bool Equals(object? obj) =>
            obj is Product other && other.AlcCode == AlcCode;
    }
}
