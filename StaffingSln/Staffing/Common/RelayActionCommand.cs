namespace Common
{
    #region Делегаты для методов WPF команд
    /// <summary>Делегат исполнительного метода команды без параметра</summary>
    public delegate void ExecuteActionHandler();
    /// <summary>Делегат исполнительного метода статуса команды без параметра</summary>
    /// <returns><see langword="true"/> если выполнение команды разрешено</returns>
    public delegate bool CanExecuteActionHandler();


    #endregion

    /// <summary>Класс для команд без параметров</summary>
    public class RelayActionCommand : RelayCommand
    {
        /// <summary>Конструктор команды</summary>
        /// <param name="execute">Выполняемый метод команды</param>
        /// <param name="canExecute">Метод разрешающий выполнение команды</param>
        public RelayActionCommand(ExecuteActionHandler execute, CanExecuteActionHandler canExecute = null)
            : base(_ => execute(), _ => canExecute()) { }

    }
}
