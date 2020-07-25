namespace Staffing.InterfacesVM
{
    /// <summary>Интерфейс основной ViewModel.</summary>
    public interface IMainViewModel: IEmployeesList, ISelectedEmployee, IAddEmployee, IEditEmployee
    {
        /// <summary>Режим отображения.</summary>
        ViewModeEnum ViewMode { get; }
    }
}
