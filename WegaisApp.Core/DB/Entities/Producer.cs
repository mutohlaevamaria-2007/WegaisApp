using System;
using System.ComponentModel.DataAnnotations;
using WegaisApp.Core.ViewModel;

namespace WegaisApp.Core.DB.Entities
{
    public class Producer : BaseViewModel
    {
        private string _type = null!;
        private string _fullName = null!;
        private string? _shortName;
        private string? _inn;
        private string? _kpp;
        private int _country;
        private int? _regionCode;
        private string? _adressDescription;
        private string? _testMigrationColumnAdded;

        /// <summary>
        /// PK.<br/>
        /// Уникальный ID ЕГАИС (12 знаков).<br/>
        /// Используется string для защиты нуля в начале значения
        /// (например "0123...")
        /// </summary>
        [Key] public string ClientRegId { get; set; } = null!;
        /// <summary>
        /// Тип производителя<br/>
        /// TS - Торговая точка<br/>
        /// FO - Иностранная организация<br/>
        /// UL - Юридическое лицо РФ<br/>
        /// </summary>
        public string Type { get => _type; set { _type = value; OnPropertyChanged(); } }
        /// <summary>
        /// Полное официальное наименование организации
        /// </summary>
        public string FullName { get => _fullName; set { _fullName = value; OnPropertyChanged(); } }
        /// <summary>
        /// Сокращенное наименование организации
        /// </summary>
        public string? ShortName { get => _shortName; set { _shortName = value; OnPropertyChanged(); } }
        /// <summary>
        /// ИНН (10 или 12 цифр). Null для TS и FO
        /// </summary>
        public string? Inn { get => _inn; set { _inn = value; OnPropertyChanged(); } }
        /// <summary>
        /// КПП (9 цифр). Null для TS и FO
        /// </summary>
        public string? Kpp { get => _kpp; set { _kpp = value; OnPropertyChanged(); } }
        /// <summary>
        /// Цифровой код страны по ОКСМ
        /// </summary>
        public int Country { get => _country; set { _country = value; OnPropertyChanged(); } }
        /// <summary>
        /// Двузначный код региона
        /// </summary>
        public int? RegionCode { get => _regionCode; set { _regionCode = value; OnPropertyChanged(); } }
        /// <summary>
        /// Текстовое описание адреса или доп. информация
        /// </summary>
        public string? AdressDescription { get => _adressDescription; set { _adressDescription = value; OnPropertyChanged(); } }
        /// <summary>
        /// Тест миграции
        /// </summary>
        public string? TestMigrationColumnAdded { get => _testMigrationColumnAdded; set { _testMigrationColumnAdded = value; OnPropertyChanged(); } }

        /// <summary>
        /// Навигационное свойство для создания FK с помощью EF Core
        /// </summary>
        public List<Product> Products { get; set; } = null!;

        // Конструкто для EF Core
        public Producer() { }

        /// <summary>
        /// Конструктор TS или FO
        /// </summary>
        public Producer(string clientRegId, string type, string fullName, string? shortName,
            int country, string? adressDescription)
        {
            ClientRegId = clientRegId;
            Type = type;
            FullName = fullName;
            ShortName = shortName;
            Country = country;
            AdressDescription = adressDescription;
        }

        /// <summary>
        /// Конструктор UL 
        /// </summary>
        public Producer(string clientRegId, string type, string fullName, string? shortName,
            string? inn, string? kpp, int country, int? regionCode, string? adressDescription)
        {
            ClientRegId = clientRegId;
            Type = type;
            FullName = fullName;
            ShortName = shortName;
            Inn = inn;
            Kpp = kpp;
            Country = country;
            RegionCode = regionCode;
            AdressDescription = adressDescription;
        }

        // Перезаписан хеш-код, т.к равенство бизнес-сущности определяет равенство PK,
        // а не равенство ссылки
        /// <summary>
        /// Равенство определяется по PK - ClientRegId
        /// </summary>
        public override int GetHashCode() => HashCode.Combine(ClientRegId);
        public override bool Equals(object? obj) =>
            obj is Producer other && other.ClientRegId == ClientRegId;
    }
}
