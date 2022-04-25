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
using DocumentHQ.Models;

namespace DocumentHQ.Views.UserControls
{
    /// <summary>
    /// Interaction logic for Books.xaml
    /// </summary>
    public partial class BooksUC : UserControl
    {
        public BookModel BookModel
        {
            get { return (BookModel)GetValue(BookModelProperty); }
            set { SetValue(BookModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BookModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BookModelProperty =
            DependencyProperty.Register("BookModel", typeof(BookModel), typeof(BooksUC), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BooksUC booksUC = d as BooksUC;
            if (booksUC != null)
            {
                booksUC.DataContext = booksUC.BookModel;
            }
        }

        public BooksUC()
        {
            InitializeComponent();
        }
    }
}
