using DocumentHQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DocumentHQ.ViewModels.Commands
{
    public class DelSemesterCommand :ICommand
    {
        public ShellViewModel ShellVM { get; set; }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelSemesterCommand(ShellViewModel vm)
        {
            ShellVM = vm;
        }

        public bool CanExecute(object? parameter)
        {
            //must always bind to selected semester in view
            SemesterModel? selectedSemester = parameter as SemesterModel;
            if (selectedSemester != null)
                return true;

            return false;
        }

        public void Execute(object? parameter)
        {
            SemesterModel? semester = parameter as SemesterModel;
            //pass paramters ID to create course method in the VM
            if (semester != null)
            {
                ShellVM.DelSemester(semester);
            }
        }
    }
}

