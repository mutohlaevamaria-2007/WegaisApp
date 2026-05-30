using System;
using System.Windows;
using WegaisApp.Core.ViewModel;

namespace WegaisApp.WPF
{
    public class WindowsMessageService : IMessageService
    {
        public void ShowMessage(string message) => MessageBox.Show(message);
        public void ShowMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK);
        }
        public void ShowMessage(string message, string title, IMessageService.Type type)
        {
            MessageBoxImage icon = type switch
            {
                IMessageService.Type.Information => MessageBoxImage.Information,
                IMessageService.Type.Exclamation => MessageBoxImage.Exclamation,
            };
            MessageBox.Show(message, title, MessageBoxButton.OK, icon);
        }
    }
}
