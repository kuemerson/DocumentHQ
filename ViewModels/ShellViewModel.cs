using DocumentHQ.Models;
using DocumentHQ.ViewModels.Commands;
using DocumentHQ.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DocumentHQ.ViewModels
{
    public class ShellViewModel : INotifyPropertyChanged
    {
        //all observable collections bind to listviews in the ShellWindowView
        public ObservableCollection<SemesterModel> SemesterModels { get; set; }
        public ObservableCollection<CourseModel> CourseModels { get; set; }
        public ObservableCollection<ShortcutModel> ShortcutModels { get; set; }
        public ObservableCollection<BookModel> BookModels { get; set; }

        private SemesterModel selectedSemester;
        public SemesterModel SelectedSemester
        {
            get { return selectedSemester; }
            //every time a differnet semester is selected get all courses
            set 
            { 
                selectedSemester = value;
                //pass name of property
                OnPropertyChanged("SelectedSemester");
                ShortcutModels.Clear();
                BookModels.Clear();
                GetCourses();
            }
        }

        private CourseModel selectedCourse;

        public CourseModel SelectedCourse
        {
            get { return selectedCourse; }
            set 
            { 
                selectedCourse = value;
                OnPropertyChanged("SelectedCourse");
                //react to event in shell vm code behind
                SelectedCourseChanged?.Invoke(this, new EventArgs());
                GetShortcuts();
                GetBooks();
                //TODO: GetCourseInfo();
            }
        }

        private ShortcutModel selectedShortcut;

        public ShortcutModel SelectedShortCut
        {
            get { return selectedShortcut; }
            set { 
                selectedShortcut = value;
                OnPropertyChanged("SelectedShortcut");
            }
        }

        private BookModel selectedBook;

        public BookModel SelectedBook
        {
            get { return selectedBook; }
            set { 
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }

        private Visibility isVisible;

        public Visibility IsVisible
        {
            get { return isVisible; }
            set
            { 
                isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }



        //command properties
        public NewCourseCommand NewCourseCommand{ get; set; }
        public NewSemesterCommand NewSemesterCommand { get; set; }
        public SaveCourseCommand SaveCourseCommand { get; set; }
        public DelLinkCommand DelLinkCommand { get; set; }
        public DelBookCommand DelBookCommand { get; set; }
        public DelCourseCommand DelCourseCommand { get; set; }
        public DelSemesterCommand DelSemesterCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public event EventHandler SelectedCourseChanged;

        //Constructor
        public ShellViewModel()
        {
            //commands
            NewSemesterCommand = new NewSemesterCommand(this);
            NewCourseCommand = new NewCourseCommand(this);
            SaveCourseCommand = new SaveCourseCommand(this);
            DelLinkCommand = new DelLinkCommand(this);
            DelBookCommand = new DelBookCommand(this);
            DelCourseCommand = new DelCourseCommand(this);
            DelSemesterCommand = new DelSemesterCommand(this);

            //initialize observable collections
            SemesterModels = new ObservableCollection<SemesterModel>();
            CourseModels = new ObservableCollection<CourseModel>();
            ShortcutModels = new ObservableCollection<ShortcutModel>();
            BookModels = new ObservableCollection<BookModel>();

            IsVisible = Visibility.Collapsed;

            //get all semseters as soon as vm is created to populate listview
            GetSemesters();


        }
        //creates new semester with initial data
        public void CreateSemester()
        {
            SemesterModel semester = new SemesterModel()
            {
                Year = DateTime.Now.Year.ToString(),
                Season = "Spring",
                Name = "Spring 2022"
            };

            DatabaseHelper.Insert(semester);
            //call GetSemesters method to refill collection and include new item
            GetSemesters();
        }
        //called in NewCourseCommand: ShellVM.CreateCourse(selectedSemester.Id);
        public void CreateCourse(int semesterId)
        {
            CourseModel course = new CourseModel()
            {
                //sets SemesterID for each course to currently selected semesters ID
                //links each course to parent semester
                SemesterID = semesterId,
                //test data for name and start time
                Title = "Test",
            };

            DatabaseHelper.Insert(course);
            //call GetCourses method to refill collection and include new item
            GetCourses();
            SelectedCourse = course;
        }
        //method to clear semester observable collection and read in items from database
        private void GetSemesters()
        {
            //read SemesterModel table from database into list
            var semesters = DatabaseHelper.Read<SemesterModel>();
            //clear collection before reading from database
            SemesterModels.Clear();
            //iterate and add to empty collection with foreach
            foreach (var semester in semesters)
            {
                SemesterModels.Add(semester);
            }
        }

        private void GetCourses()
        {
            //check if a semester is selected
            if(selectedSemester != null)
            {
                //use linq to only read the courses that have a id that matches the selected semseter
                //reads to IEnumberable by defualt - call ToList()
                var courses = DatabaseHelper.Read<CourseModel>().Where(n => n.SemesterID == SelectedSemester.Id).ToList();

                CourseModels.Clear();
                foreach(var course in courses)
                {
                    CourseModels.Add(course);
                }
            }
        }
        //public so method can be called in ShellWindow.View.xaml.cs
        public void GetShortcuts()
        {
            //check if a course is selected
            if (selectedCourse != null)
            {
                //use linq to only read the courses that have a id that matches the selected semseter
                //reads to IEnumberable by defualt - call ToList()
                var shortcuts = DatabaseHelper.Read<ShortcutModel>().Where(n => n.courseId == SelectedCourse.Id).ToList();

                ShortcutModels.Clear();
                foreach (var shortcut in shortcuts)
                {
                    ShortcutModels.Add(shortcut);
                }
            }
        }
        //public so method can be called in ShellWindow.View.xaml.cs
        public void GetBooks()
        {
            //check if a course is selected
            if (selectedCourse != null)
            {
                //use linq to only read the courses that have a id that matches the selected semseter
                //reads to IEnumberable by defualt - call ToList()
                var books = DatabaseHelper.Read<BookModel>().Where(n => n.courseId == SelectedCourse.Id).ToList();

                BookModels.Clear();
                foreach (var book in books)
                {
                    BookModels.Add(book);
                }
            }
        }

        public void ShowCourseInput()
        {
            IsVisible = Visibility.Visible;
        }

        public void SaveCourse(CourseModel course)
        {
            IsVisible = Visibility.Collapsed;
            DatabaseHelper.Update<CourseModel>(course);
            GetCourses();
        }

        public void DelShortcut(ShortcutModel shortcut)
        {
            if(shortcut != null)
            {
                DatabaseHelper.Delete<ShortcutModel>(shortcut);
                GetShortcuts();
            }
        }
         public void DelBook(BookModel book)
        {
            if(book != null)
            {
                DatabaseHelper.Delete<BookModel>(book);
                GetBooks();
            }
        }

        public void DelCourse(CourseModel course)
        {
            if(course != null)
            {
                IsVisible = Visibility.Collapsed;
                DatabaseHelper.Delete<CourseModel>(course);
                ShortcutModels.Clear();
                BookModels.Clear();
                GetCourses();
            }
        }
        public void DelSemester(SemesterModel semester)
        {
            if (semester != null)
            {
                DatabaseHelper.Delete<SemesterModel>(semester);
                GetSemesters();
                CourseModels.Clear();
                BookModels.Clear();
                ShortcutModels.Clear();
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
