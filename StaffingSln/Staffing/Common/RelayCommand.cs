using System;
using System.Windows;
using System.Windows.Input;

namespace Common
{
    #region Делегаты для методов WPF команд
    /// <summary>Делегат исполнительного метода команды</summary>
    /// <param name="parameter">Параметр команды</param>
    public delegate void ExecuteHandler(object parameter);
    /// <summary>Делегат исполнительного метода статуса команды</summary>
    /// <param name="parameter">Параметр команды</param>
    /// <returns><see langword="true"/> если выполнение команды разрешено</returns>
    public delegate bool CanExecuteHandler(object parameter);
    #endregion

    #region Класс команд - RelayCommand
    /// <summary>Класс реализующий интерфейс ICommand для создания WPF команд</summary>
    public class RelayCommand : ICommand
    {
        private readonly CanExecuteHandler _canExecute;
        private readonly ExecuteHandler _onExecute;
        private readonly EventHandler _requerySuggested;

        /// <summary>Событие извещающее об измении состояния команды</summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>Конструктор команды</summary>
        /// <param name="execute">Выполняемый метод команды</param>
        /// <param name="canExecute">Метод разрешающий выполнение команды</param>
        public RelayCommand(ExecuteHandler execute, CanExecuteHandler canExecute = null)
        {
            _onExecute = execute;
            _canExecute = canExecute;

            _requerySuggested = (o, e) => Invalidate();
            CommandManager.RequerySuggested += _requerySuggested;
        }

        public void Invalidate()
            => Application.Current.Dispatcher.BeginInvoke
            (
                new Action(() => CanExecuteChanged?.Invoke(this, EventArgs.Empty)),
                null
            );

        /// <summary>Вызов разрешающего метода команды</summary>
        /// <param name="parameter">Параметр команды</param>
        /// <returns>True - если выполнение команды разрешено</returns>
        public bool CanExecute(object parameter) => _canExecute == null ? true : _canExecute.Invoke(parameter);

        /// <summary>Вызов выполняющего метода команды</summary>
        /// <param name="parameter">Параметр команды</param>
        public void Execute(object parameter) => _onExecute?.Invoke(parameter);
    }

#endregion

}
