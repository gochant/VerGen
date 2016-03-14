using System;
using System.Windows;

namespace VerGen.Tool.UI.Infrastructure
{
    public static class UIHelper
    {
        public static void ConfirmOperate(Action action)
        {
            var messageBoxResult = MessageBox.Show("确定进行该操作？", "用户确认", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                action();
            }
        }
    }
}