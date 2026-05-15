using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using WegaisApp.Core.DB;
using WegaisApp.Core.DB.Entities;
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

        #region Импорт данных xml
        private WegaisDataBundle _importBundle;
        private bool _isImportedPreviewVisible;
        private string _importInfo;

        public WegaisDataBundle ImportBundle { get => _importBundle; private set { _importBundle = value; OnPropertyChanged(); } }
        public bool IsImportedPreviewVisible { get => _isImportedPreviewVisible; private set { _isImportedPreviewVisible = value; OnPropertyChanged(); } }
        public string ImportInfo { get => _importInfo; private set { _importInfo = value; OnPropertyChanged(); } }
        public ObservableCollection<StockPositionInfo> StockPositionsPreview { get; private set; }
        public RelayCommand SaveImportCommand => new(_ => SaveImport());
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

        public void ImportXml(string[] files)
        {
            ImportBundle = ParseXmlFiles(files);
            IsImportedPreviewVisible = true;
            UpdateImportInfo(ImportBundle);
        }

        private WegaisDataBundle ParseXmlFiles(string[] files)
        {
            WegaisDataBundle data = new();
            // Обработка пути
            foreach (string filePath in files)
            {
                Documents documents = WegaisMapper.Deserialize(filePath);
                var result = WegaisMapper.MapToRows(documents);
                data.UnionRange(result);
            }
            return data;
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
            ImportBundle = null;
            IsImportedPreviewVisible = false;
        }
    }
}
