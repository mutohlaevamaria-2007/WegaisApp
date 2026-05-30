using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using WegaisApp.Core.DB;
using WegaisApp.Core.DB.Entities;
using WegaisApp.Core.WeatherAPIIntegration;
using WegaisApp.Core.XMLIntegration;

namespace WegaisApp.Core.ViewModel
{
    public class WegaisViewModel : BaseViewModel
    {
        #region Таблицы
        public ObservableCollection<DB.Entities.Producer>? Producers { get; private set; }
        public ObservableCollection<Product>? Products { get; private set; }
        public ObservableCollection<StockPosition>? StockPositions { get; private set; }
        #endregion

        #region Импорт XML
        private WegaisDataBundle _importBundle;
        private bool _isImportedPreviewVisible;
        private string _importInfo;
        private List<StockPositionInfo> _stockPositionsPreview;

        /// <summary>
        /// Записи, готовые для добавления в БД
        /// </summary>
        public WegaisDataBundle ImportBundle { get => _importBundle; private set { _importBundle = value; OnPropertyChanged(); } }
        /// <summary>
        /// Отображение панели/группы UI превью импорта
        /// </summary>
        public bool IsImportedPreviewVisible { get => _isImportedPreviewVisible; private set { _isImportedPreviewVisible = value; OnPropertyChanged(); } }
        /// <summary>
        /// Данные о таблицах
        /// </summary>
        public string ImportInfo { get => _importInfo; private set { _importInfo = value; OnPropertyChanged(); } }
        /// <summary>
        /// GroupJoin запрос для превью таблицы
        /// </summary>
        public List<StockPositionInfo> StockPositionsPreview { get => _stockPositionsPreview; private set { _stockPositionsPreview = value; OnPropertyChanged(); } }
        public RelayCommand SaveImportCommand => new(_ => SaveImport());
        #endregion

        #region Текущая погода
        private WeatherResponse _currentWeather;
        public WeatherResponse CurrentWeather { get => _currentWeather; private set { _currentWeather = value; OnPropertyChanged(); } }
        #endregion

        private readonly IMessageService _messageService;
        private readonly WegaisContext _dbContext;

        public WegaisViewModel(IMessageService messageService)
        {
            _messageService = messageService;

            _dbContext = new();
            // Загрузка таблиц в кеш AppContext
            _dbContext.Producers.Load();
            _dbContext.Products.Load();
            _dbContext.StockPositions.Load();

            // Привязка свойств VM к контексту (динамическое обновление)
            Producers = _dbContext.Producers.Local.ToObservableCollection();
            Products = _dbContext.Products.Local.ToObservableCollection();
            StockPositions = _dbContext.StockPositions.Local.ToObservableCollection();
        }

        #region Импорт XML
        public void ImportXml(string[] files)
        {
            if (TryParseXmlFiles(files))
            {
                IsImportedPreviewVisible = true;
                StockPositionsPreview = StockPositionInfo.PreviewQuery(ImportBundle);
                UpdateImportInfo(ImportBundle);
            }
        }
        private bool TryParseXmlFiles(string[] files)
        {
            WegaisDataBundle data = new();
            // Обработка пути
            foreach (string filePath in files)
            {
                try
                {
                    Documents documents = WegaisMapper.Deserialize(filePath);
                    var result = WegaisMapper.MapToRows(documents);
                    data.UnionRange(result);
                }
                catch (Exception)
                {
                    _messageService.ShowMessage($"Неправильный формат файла");
                    return false;
                }
            }
            ImportBundle = data;
            return true;
        }
        private void UpdateImportInfo(WegaisDataBundle data)
        {
            ImportInfo = $"Производители: {data.Producers.Count}\n" +
                $"Виды продукции: {data.Products.Count}\n" +
                $"Позиции склада: {data.StockPositions.Count}";
        }
        private void SaveImport()
        {
            _messageService.ShowMessage("Изменено записей: " + _dbContext.UnionRange(ImportBundle));
            //ImportBundle = null;
            IsImportedPreviewVisible = false;
        }
        #endregion

        public async Task UpdateCurrentWeather()
        {
            WeatherClient weatherClient = new();
            CurrentWeather = await weatherClient.GetCurrentWeatherAsync();
        }
    }
}
