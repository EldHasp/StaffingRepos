using Common;
using Staffing.DTO;
using System.Collections.ObjectModel;

namespace Staffing.InterfacesVM
{
    /// <summary>Интерфейс редактирования данных Сотрудника.</summary>
    public interface IEditEmployee
    {
        /// <summary>Коллекция должностей.</summary>
        ObservableCollection<PositionDto> Positions { get; }

        /// <summary>Данные редактируемого Сотрудника.</summary>
        IEmployeeVM EditEmployee { get; }

        /// <summary>Команда сохранения изменения данных Сотрудника.</summary>
        RelayCommand<IEmployeeVM> SaveEditEmployeeCommand { get; }

        /// <summary>Команда выхода из режима редактирования данных Сотрудника.</summary>
        RelayActionCommand EditModeExitCommand { get; }
    }
}
