using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;

namespace VerGen.Tool.UI.Infrastructure
{
    public class SelectWindow : Window
    {
        public ListView ListControl { get; set; }

        public CheckBox SelectAllControl { get; set; }

        public List<string> Selected { get; set; }

        protected void SelectAll()
        {
            if (SelectAllControl.IsChecked == true)
            {
                ListControl.SelectAll();
                ListControl.Focus();
            }
            else
            {
                ListControl.UnselectAll();
            }
        }

        protected void SetResult<T>(Expression<Func<T,string>> expression)
        {
            Selected = ListControl.SelectedItems.Cast<T>().AsQueryable().Select(expression).ToList();
            DialogResult = true;
        }
    }
}