using DocumentHQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DocumentHQ.ViewModels.Commands
{
    public class DelLinkCommand : ICommand
    {
        public ShellViewModel ShellVM { get; set; }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelLinkCommand(ShellViewModel vm)
        {
            ShellVM = vm;
        }

        public bool CanExecute(object? parameter)
        {
            //must always bind to selected semester in view
            ShortcutModel? selectedShortcut = parameter as ShortcutModel;
            if (selectedShortcut != null)
                return true;

            return false;
        }

        public void Execute(object? parameter)
        {
            ShortcutModel? shortcut = parameter as ShortcutModel;
            //pass paramters ID to create course method in the VM
            if (shortcut != null)
            {
                ShellVM.DelShortcut(shortcut);
            }
        }
    }
}
