﻿using DocumentHQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DocumentHQ.ViewModels.Commands
{
    public class DelBookCommand : ICommand
    {
        public ShellViewModel ShellVM { get; set; }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelBookCommand(ShellViewModel vm)
        {
            ShellVM = vm;
        }

        public bool CanExecute(object? parameter)
        {
            //must always bind to selected book in view
            BookModel? selectedBook = parameter as BookModel;
            if (selectedBook != null)
                return true;

            return false;
        }

        public void Execute(object? parameter)
        {
            BookModel? book = parameter as BookModel;
            if (book != null)
            {
                ShellVM.DelBook(book);
            }
        }
    }
}
