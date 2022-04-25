using DocumentHQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace DocumentHQ.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ResourcesUC.xaml
    /// </summary>
    public partial class ShortcutsUC : UserControl
    {
        public ShortcutModel ShortcutModel
        {
            get { return (ShortcutModel)GetValue(ShortcutModelProperty); }
            set { SetValue(ShortcutModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShortcutModelProperty =
            DependencyProperty.Register("ShortcutModel", typeof(ShortcutModel), typeof(ShortcutsUC), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ShortcutsUC shortcutsUC = d as ShortcutsUC;
            if (shortcutsUC != null)
            {
                shortcutsUC.DataContext = shortcutsUC.ShortcutModel;
            }
        }

        public ShortcutsUC()
        {
            InitializeComponent();
        }
    }
}
