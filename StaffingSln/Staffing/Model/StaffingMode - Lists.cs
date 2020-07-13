using Staffing.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Staffing.Model
{
    // Файл со списками Модели.

    /// <summary>Класс Модели.</summary>
    public partial class StaffingModel
    {
        /// <summary>Словарь должностей.</summary>
        public ReadOnlyDictionary<int, PositionDto> Positions { get; }
            = new ReadOnlyDictionary<int, PositionDto>
            (new PositionDto[]
                {
                    new PositionDto(1, "Директор"),
                    new PositionDto(15, "Начальник отдела"),
                    new PositionDto(7, "Старший сотрудник"),
                    new PositionDto(13, "Младщий сотрудник"),
                }
                .ToDictionary(p => p.Id)
            );

        /// <summary>Приватный изменяемый словарь сотрудников.</summary>
        private readonly Dictionary<int, EmployeeDto> employeesDict ;

        /// <summary>Публичный неизменяемый словарь сотрудников.</summary>
        public ReadOnlyDictionary<int, EmployeeDto> Employees { get; }

        /// <summary>Конструктор Модели</summary>
        public StaffingModel()
        {

            employeesDict = new EmployeeDto[]
            {
                new EmployeeDto(279, "Пётр", Positions[1], new DateTime(1980,10,25)),
                new EmployeeDto(654, "Фёдор", Positions[15], new DateTime(1980,10,25)),
                new EmployeeDto(941, "Иван", Positions[7], new DateTime(1980,10,25)),
                new EmployeeDto(692, "Сидор", Positions[13], new DateTime(1980,10,25)),
                new EmployeeDto(395, "Миша", Positions[13], new DateTime(1980,10,25)),
            }
            .ToDictionary(emp => emp.Id);

            Employees = new ReadOnlyDictionary<int, EmployeeDto>(employeesDict);
        }
    }
}
