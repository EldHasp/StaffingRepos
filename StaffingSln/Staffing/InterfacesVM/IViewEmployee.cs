namespace Staffing.InterfacesVM
{
    /// <summary>Интерфейс показа подробной информации по выбраному Сотруднику.</summary>
    public interface IViewEmployee
    {
        /// <summary>Данные выбранного сотрудника.</summary>
        IEmployeeVM SelectedEmployeу { get; }
    }
}
