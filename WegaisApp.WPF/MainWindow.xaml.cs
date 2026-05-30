using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WegaisApp.Core.ViewModel;

namespace WegaisApp.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new WegaisViewModel(new WindowsMessageService());
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WegaisViewModel viewModel = (WegaisViewModel)DataContext;
            await viewModel.UpdateCurrentWeather();
        }

        #region Drag&Drop
        // Изменение курсора
        public void DropRegion_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;

            e.Handled = true;
        }
        // Парсинг + показ превью таблиц
        public void DropRegion_Drop(object sender, DragEventArgs e)
        {
            // Проверка типа файлов
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Извлечение файлов
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                WegaisViewModel viewModel = (WegaisViewModel)DataContext;
                viewModel.ImportXml(files);
            }
        }
        #endregion

    }
}