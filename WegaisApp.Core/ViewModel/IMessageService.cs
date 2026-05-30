namespace WegaisApp.Core.ViewModel
{
    public interface IMessageService
    {
        enum Type
        {
            Information,
            Exclamation
        }
        void ShowMessage(string message);
        void ShowMessage(string message, string title);
        void ShowMessage(string message, string title, IMessageService.Type type);
    }
}
