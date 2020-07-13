using Staffing.DTO;
using System;

namespace Staffing.Model
{
    // Файл с методами Модели.

    public partial class StaffingModel
    {
        /// <summary>ГСЧ.</summary>
        private static readonly Random random = new Random();

        /// <summary>Добавление в список новго сотрудника.</summary>
        /// <param name="employee">Данные нового сотруднкиа. ID - игнорируется.</param>
        public void AddEmployee(EmployeeDto employee)
        {
            // Проверка наличия указанной должности в списке должностей.
            if (!(Positions.TryGetValue(employee.Position.Id, out PositionDto position) && position == employee.Position))
                throw new ArgumentException("Такой должности нет.", nameof(employee));

            // Получение слуяайного уникального идентификатора.
            int id = random.Next();
            while (employeesDict.ContainsKey(id))
                id = random.Next();

            // Получение DTO с данными работника и запись его в приватный словарь.
            EmployeeDto emplNew = new EmployeeDto(id, employee.Name, employee.Position, employee.DateOfBirth);
            employeesDict.Add(id, emplNew);

            // Создание события, уведомляющего о добавлениии элемента в коллекцию.
            RaiseActionEmployees(ActionListEnum.Added, emplNew);
        }

        /// <summary>Удаление сторудника из списка.</summary>
        /// <param name="employee">Данные удаляемого сотрудника.
        /// Должны полностью совпадать с имеющимся в списке.</param>
        public void RemoveEmployee(EmployeeDto employee)
        {
            if (!employeesDict.TryGetValue(employee.Id, out EmployeeDto empl))
                throw new ArgumentException("Сотрудника с таким ID нет.", nameof(employee));

            if (empl.Name != employee.Name || empl.Position != employee.Position || empl.DateOfBirth != employee.DateOfBirth)
                throw new ArgumentException("Несовпадают данные о сутруднике", nameof(employee));

            // Удаление работника из приватного словаря.
            employeesDict.Remove(employee.Id);

            // Создание события, уведомляющего об удалении элемента из коллекции.
            RaiseActionEmployees(ActionListEnum.Removed, empl);

        }

        /// <summary>Изменение данных сотрудника.</summary>
        /// <param name="employeeOld">Старые данные о сотруднике.
        /// Должны полностью совпадать с имеющимся в списке.</param>
        /// <param name="employeeNew">Новые данные о сутруднике. ID изменяться не должен!</param>
        public void ChangeEmployee(EmployeeDto employeeOld, EmployeeDto employeeNew)
        {
            if (!employeesDict.TryGetValue(employeeOld.Id, out EmployeeDto empl))
                throw new ArgumentException("Сотрудника с таким ID нет.", nameof(employeeOld));

            if (empl.Name != employeeOld.Name || empl.Position != employeeOld.Position || empl.DateOfBirth != employeeOld.DateOfBirth)
                throw new ArgumentException("Несовпадают данные о сутруднике.", nameof(employeeOld));

            if (employeeOld.Id != employeeNew.Id)
                throw new ArgumentException("Идентификатор должен оставаться прежним.", nameof(employeeNew));

            // Запись в приватный словарь новых данных.
            employeesDict[employeeNew.Id] = employeeNew;

            // Создание события, уведомляющего об изменении элемента в коллекции.
            RaiseActionEmployees(ActionListEnum.Changed, employeeNew);

        }

    }
}
