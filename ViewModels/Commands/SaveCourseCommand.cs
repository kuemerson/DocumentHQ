using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DocumentHQ.Models;

namespace DocumentHQ.ViewModels.Commands
{
    public class SaveCourseCommand : ICommand
    {
        public ShellViewModel ShellVM { get; set; }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SaveCourseCommand(ShellViewModel vm)
        {
            ShellVM = vm;
        }

        public bool CanExecute(object? parameter)
        {
            //must always bind to selected semester in view
            CourseModel? selectedCourse = parameter as CourseModel;
            if (selectedCourse != null)
                return true;

            return false;

        }

        public void Execute(object? parameter)
        {
            CourseModel? course = parameter as CourseModel;
            //pass paramters ID to create course method in the VM
            if (course != null)
            {
                ShellVM.SaveCourse(course);
            }

        }
    }
}
