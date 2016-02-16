using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VerGen.Schema.Infrastructure
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase()
        {
            PropertyChanged += PropertyChangedHandler;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public virtual void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {

        }

        public virtual void InitializeCommand()
        {

        }

        //protected void ConfirmOperate(Action action)
        //{
        //    var messageBoxResult = MessageBox.Show("确定进行该操作？", "用户确认", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //    if (messageBoxResult == MessageBoxResult.Yes)
        //    {
        //        action();
        //    }
        //}
    }
}