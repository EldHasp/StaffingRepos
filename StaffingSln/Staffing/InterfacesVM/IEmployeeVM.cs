using Staffing.DTO;
using System;

namespace Staffing.InterfacesVM
{
    /// <summary>Интерфейс элемента Сотрудника.</summary>
    public interface IEmployeeVM
    {
        /// <summary>Уникальный идентификатор.</summary>
        int Id { get; }

        /// <summary>Имя Сотрудника.</summary>
        string FirstName { get; set; }

        /// <summary>Должность.</summary>
        PositionDto Position { get; set; }

        /// <summary>Дата рождения.</summary>
        DateTime DateOfBirth { get; set; }

        /// <summary>Возраст - полных лет.</summary>
        int Age { get; }

        /// <summary>Полная информация по Сотруднику.</summary>
        string About { get; }
    }
}