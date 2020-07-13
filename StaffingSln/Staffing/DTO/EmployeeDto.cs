using System;

namespace Staffing.DTO
{
    /// <summary>Неизменяемый класс для сотрудника.</summary>
    public class EmployeeDto
    {
        /// <summary>Уникальный идентификатор.</summary>
        public int Id { get; }

        /// <summary>Имя сотрудника.</summary>
        public string Name { get; }

        /// <summary>Должность.</summary>
        public PositionDto Position { get; }

        /// <summary>Дата рождения.</summary>
        public DateTime DateOfBirth { get; }

        /// <summary>Конструктор задающий все значения.</summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="name">Имя сотрудника.</param>
        /// <param name="position">Должность.</param>
        /// <param name="dateOfBirth">Дата рождения.</param>
        public EmployeeDto(int id, string name, PositionDto position, DateTime dateOfBirth)
        {
            Id = id;
            Name = name;
            Position = position;
            DateOfBirth = dateOfBirth.Date;
        }
    }
}
