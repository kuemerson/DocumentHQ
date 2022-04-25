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
    /// Interaction logic for SemesterUC.xaml
    /// </summary>
    public partial class SemesterUC : UserControl
    {
        //dependency property generated with propdp code snipet
        public SemesterModel SemesterModel
        {
            get { return (SemesterModel)GetValue(SemesterModelProperty); }
            set { SetValue(SemesterModelProperty, value); }
        }
        // DO NOT FOGET THIS IN COURSE USER CONTROL!!!!
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SemesterModelProperty =
            DependencyProperty.Register("SemesterModel", typeof(SemesterModel), typeof(SemesterUC), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SemesterUC semesterUserControl = d as SemesterUC;
            if (semesterUserControl != null)
            {
                semesterUserControl.DataContext = semesterUserControl.SemesterModel;
            }
        }

        public SemesterUC()
        {
            InitializeComponent();
        }
    }
}
