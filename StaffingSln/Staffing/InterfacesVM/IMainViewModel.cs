namespace Staffing.InterfacesVM
{
    /// <summary>Интерфейс основной ViewModel.</summary>
    public interface IMainViewModel: IEmployeesList, IViewEmployee, IAddEmployee, IEditEmployee
    {
        /// <summary>Режим отображения.</summary>
        ViewModeEnum ViewMode { get; }
    }
}
