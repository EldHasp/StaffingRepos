namespace Staffing.InterfacesVM
{
    /// <summary>Интерфейс показа подробной информации по выбраному Сотруднику.</summary>
    public interface ISelectedEmployee
    {
        /// <summary>Данные выбранного сотрудника.</summary>
        IEmployeeVM SelectedEmployee { get; }
    }
}
