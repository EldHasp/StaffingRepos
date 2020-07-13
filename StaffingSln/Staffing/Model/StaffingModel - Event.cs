using Staffing.DTO;

namespace Staffing.Model
{
    // Файл с событием Модели.

    /// <summary>Перечисление с указанием действий для списка.</summary>
    public enum ActionListEnum
    {
        /// <summary>Добавлен элемент.</summary>
        Added,
        /// <summary>Удалён элемент.</summary>
        Removed, 
        /// <summary>Изменён элемент.</summary>
        Changed
    }

    /// <summary>Делегат события.</summary>
    public delegate void ActionListHandler<T>(object sender, ActionListEnum action, T item);

    public partial class StaffingModel
    {
        public event ActionListHandler<EmployeeDto> ActionEmployees;

        private void RaiseActionEmployees(ActionListEnum action, EmployeeDto employee)
            => ActionEmployees?.Invoke(this, action, employee);
    }
}
