using System;
using System.Windows;
using WegaisApp.Core.ViewModel;

namespace WegaisApp.WPF
{
    public class WindowsMessageService : IMessageService
    {
        public void ShowMessage(string message) => MessageBox.Show(message);
    }
}
