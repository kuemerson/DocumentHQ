using System.Windows;
using System.Windows.Input;
using System.IO;
using DocumentHQ.ViewModels.Helpers;
using DocumentHQ.ViewModels;
using System;
using System.Windows.Documents;
using DocumentHQ.Models;
using System.Diagnostics;

namespace DocumentHQ.Views
{
    /// <summary>
    /// Interaction logic for ShellWindowView.xaml
    /// </summary>
    public partial class ShellWindowView : Window
    {
        //create ShellViewModel var to allow access to vm in code behind without breaking bindings
        //- needed for drag and drop and saving without commands 
        //- initialize in the constructor with resource key
        ShellViewModel shellViewModel;
        public ShellWindowView()
        {
            InitializeComponent();
            //assign to current vm from view and must be cast to ShellViewModel
            //- needed for drag and drop and saving without commands 
            shellViewModel = Resources["shellVM"] as ShellViewModel;
            //add event handler for every time selected course changes
            shellViewModel.SelectedCourseChanged += ShellViewModel_SelectedCourseChanged;
        }

        private void ShellViewModel_SelectedCourseChanged(object? sender, EventArgs e)
        {
            //clear contents both rtf boxes in the view on course change before attempting to read the new courses
            myTasksRTB.Document.Blocks.Clear();
            quickNotesRTB.Document.Blocks.Clear();
            //null checks followed by basically the same steps as saving but load instead
            //would like to clean up and elminate redundency if time allows
            if (shellViewModel.SelectedCourse != null)
            {
                if (!string.IsNullOrEmpty(shellViewModel.SelectedCourse.MyTasksFile))
                {
                    //FileStream taskStream = new FileStream(shellViewModel.SelectedCourse.MyTasksFile, FileMode.Open);
                    //var taskContents = new TextRange(myTasksRTB.Document.ContentStart, myTasksRTB.Document.ContentEnd);
                    //taskContents.Load(taskStream, DataFormats.Rtf);

                    //have to use using statements to close files or crashes/doesn't load if selecting courses too fast
                    using(FileStream taskStream = new FileStream(shellViewModel.SelectedCourse.MyTasksFile, FileMode.Open))
                    {
                        var taskContents = new TextRange(myTasksRTB.Document.ContentStart, myTasksRTB.Document.ContentEnd);
                        taskContents.Load(taskStream, DataFormats.Rtf);
                    }
                }
                if (!string.IsNullOrEmpty(shellViewModel.SelectedCourse.QuickNoteFile))
                {
                    using (FileStream noteStream = new FileStream(shellViewModel.SelectedCourse.QuickNoteFile, FileMode.Open))
                    {
                        var noteContents = new TextRange(quickNotesRTB.Document.ContentStart, quickNotesRTB.Document.ContentEnd);
                        noteContents.Load(noteStream, DataFormats.Rtf);
                    }
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void shortcutsLV_PreviewDrop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                //TODO:Add for each loop to handle all files in array if time allows
                string filename = Path.GetFileName(files[0]);
                if (filename != null)
                {
                    ShortcutModel shortcut = new ShortcutModel()
                    {
                        courseId = shellViewModel.SelectedCourse.Id,
                        Label = filename,
                        ShortcutPath = Path.GetFullPath(files[0]),
                    };
                    DatabaseHelper.Insert(shortcut);
                    shellViewModel.GetShortcuts();
                }
            }
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                string url = (string)e.Data.GetData(DataFormats.Text);
     
                if (url != null)
                {
                    ShortcutModel shortcut = new ShortcutModel()
                    {
                        courseId = shellViewModel.SelectedCourse.Id,
                        Label = url,
                        ShortcutPath = url,
                    };
                    DatabaseHelper.Insert(shortcut);
                    shellViewModel.GetShortcuts();
                }
            }
        }

        private void shortcutsLV_PreviewDragOver(object sender, DragEventArgs e)
        {
            //TODO
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //create and assign file paths
            string myTasksRTF = System.IO.Path.Combine(Environment.CurrentDirectory, $"{shellViewModel.SelectedCourse.Id}myTasks.rtf");
            string quickNotesRTF = System.IO.Path.Combine(Environment.CurrentDirectory, $"{shellViewModel.SelectedCourse.Id}quickNotes.rtf");
            //update the paths for selected course in the database
            shellViewModel.SelectedCourse.MyTasksFile = myTasksRTF;
            shellViewModel.SelectedCourse.QuickNoteFile = quickNotesRTF;
            DatabaseHelper.Update(shellViewModel.SelectedCourse);
            //start filestream and save contents of rich text box from view in rtf format
            FileStream tasksStream = new FileStream(myTasksRTF, FileMode.Create);
            var taskContents = new TextRange(myTasksRTB.Document.ContentStart, myTasksRTB.Document.ContentEnd);
            taskContents.Save(tasksStream, DataFormats.Rtf);

            FileStream noteStream = new FileStream(quickNotesRTF, FileMode.Create);
            var noteContents = new TextRange(quickNotesRTB.Document.ContentStart, quickNotesRTB.Document.ContentEnd);
            noteContents.Save(noteStream, DataFormats.Rtf);

        }

        private void booksLV_PreviewDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                //TODO:Add for each loop to handle all files in array if time allows
                string filename = Path.GetFileNameWithoutExtension(files[0]);
                if (filename != null)
                {
                    BookModel book = new BookModel()
                    {
                        courseId = shellViewModel.SelectedCourse.Id,
                        Label = filename,
                        BookPath = Path.GetFullPath(files[0]),
                    };
                    DatabaseHelper.Insert(book);
                    shellViewModel.GetBooks();
                }
            }
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                string url = (string)e.Data.GetData(DataFormats.Text);

                if (url != null)
                {
                    BookModel book = new BookModel()
                    {
                        courseId = shellViewModel.SelectedCourse.Id,
                        Label = url,
                        BookPath = url,
                    };
                    DatabaseHelper.Insert(book);
                    shellViewModel.GetBooks();
                }
            }
        }

        private void Link_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //surround with try catch block due to high likelihood of errors
            //and message window to instruct user to re-add bad link

            Process p = new Process();
            try
            {
                p.StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = shellViewModel.SelectedBook.BookPath,
                };
                p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Unable to open Shortcut link. Please delete the link and re-add.", "DocumentHQ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

            
        }

        private void Shortcut_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Process p = new Process();
            try
            {
                p.StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = shellViewModel.SelectedShortCut.ShortcutPath,
                };

                p.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: Unable to open Shortcut link. Please delete the link and re-add.", "DocumentHQ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
