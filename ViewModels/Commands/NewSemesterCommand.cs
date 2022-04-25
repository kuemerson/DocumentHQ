using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DocumentHQ.ViewModels.Commands
{
    //impleament ICommand interface
    public class NewSemesterCommand : ICommand
    {
        public ShellViewModel ShellVM { get; set; } 

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        //sets defualt shell view model
        public NewSemesterCommand(ShellViewModel vm)
        {
            ShellVM = vm;
        }

        //can always excaute for now
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            ShellVM.CreateSemester();
            //TODO: call create new semester function
        }
    }
}
