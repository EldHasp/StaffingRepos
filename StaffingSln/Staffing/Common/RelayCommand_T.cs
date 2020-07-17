using System.ComponentModel;

namespace Common
{
	#region Делегаты для методов WPF команд
	/// <summary>Делегат исполнительного метода команды</summary>
	/// <param name="parameter">Параметр команды</param>
	public delegate void ExecuteHandler<T>(T parameter);
	/// <summary>Делегат исполнительного метода статуса команды</summary>
	/// <param name="parameter">Параметр команды</param>
	/// <returns><see langword="true"/> если выполнение команды разрешено</returns>
	public delegate bool CanExecuteHandler<T>(T parameter);
	#endregion

	/// <summary>Класс для команд без параметров</summary>
	public class RelayCommand<T> : RelayCommand
	{

		/// <summary>Конструктор команды</summary>
		/// <param name="execute">Выполняемый метод команды</param>
		/// <param name="canExecute">Метод разрешающий выполнение команды</param>
		public RelayCommand(ExecuteHandler<T> execute, CanExecuteHandler<T> canExecute = null)
			: base(p => execute(p is T t ? t : default), p => p is T t && (canExecute?.Invoke(t) ?? true)) { }

	}
}
