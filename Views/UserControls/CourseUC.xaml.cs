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
    /// Interaction logic for CourseUC.xaml
    /// </summary>
    public partial class CourseUC : UserControl
    {


        public CourseModel CourseModel
        {
            get { return (CourseModel)GetValue(CourseModelProperty); }
            set { SetValue(CourseModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CourseModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CourseModelProperty =
            DependencyProperty.Register("CourseModel", typeof(CourseModel), typeof(CourseUC), new PropertyMetadata(null, SetValues)); //null is defualt and SetValues does work when property is changed
        //updates data context of the user control every time the selection changes
        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //cast
            CourseUC courseUC = d as CourseUC;

            if(courseUC != null)
            {
                courseUC.DataContext = courseUC.CourseModel;
            }
        }

        public CourseUC()
        {
            InitializeComponent();
        }
    }
}
