using Staffing.DTO;
using Staffing.InterfacesVM;
using Staffing.Model;
using System;
using System.Linq;

namespace Staffing.ViewModel
{
    public class StaffingViewModel : OnlyViewVM
    {
        private readonly StaffingModel model;
        public StaffingViewModel(StaffingModel model)
            : base(false)
        {
            this.model = model;
            model.ActionEmployees += Model_ActionEmployees;

            foreach (PositionDto position in model.Positions.Values)
                Positions.Add(position);


            foreach (EmployeeDto employee in model.Employees.Values)
                AddingEmployee(employee);

        }

        private void Model_ActionEmployees(object sender, ActionListEnum action, EmployeeDto item)
        {
            switch (action)
            {
                case ActionListEnum.Added:
                    AddingEmployee(item);
                    break;
                case ActionListEnum.Removed:
                    RemovingEmployee(item);
                    break;
                case ActionListEnum.Changed:
                    ChangingEmployee(item);
                    break;
                default:
                    throw new ArgumentException(nameof(action));
            }
        }

        /// <summary>Добавление Сотрудника в список.</summary>
        /// <param name="item">Добавляемый Сотрудник.</param>
        private void AddingEmployee(EmployeeDto employee)
        {
            EmployeeVM emplVm = (EmployeeVM)Employees.FirstOrDefault(empl => empl.Id == employee.Id);
            if (emplVm == null)
                Employees.Add(new EmployeeVM(employee));
            else
                emplVm.SetDto(employee);
        }

        /// <summary>Удаление Сотрудника из списка.</summary>
        /// <param name="item">Удаляемый Сотрудник.</param>
        private void RemovingEmployee(EmployeeDto employee)
        {
            for (int i = 0; i < Employees.Count; i++)
            {
                if (Employees[i].Id != employee.Id)
                    continue;

                Employees.RemoveAt(i);
                break;
            }
        }

        /// <summary>Редактирование данных Сотрудника из списка.</summary>
        /// <param name="item">Новые данные Сотрудника.</param>
        private void ChangingEmployee(EmployeeDto employee)
        {
            EmployeeVM emplVm = (EmployeeVM)Employees.FirstOrDefault(empl => empl.Id == employee.Id);
            if (emplVm != null)
                emplVm.SetDto(employee);
        }

        protected override void AddEmployeeMethod(IEmployeeVM parameter)
        {
            // base.AddEmployeeMethod(parameter);

            if (!AddEmployeeCanMethod(parameter))
                return;

            EmployeeVM empl = (EmployeeVM)parameter;
            model.AddEmployee(empl.Copy());

            ModeExitMethod(ViewModeEnum.Adding);
        }

        protected override void RemoveEmployeeMethod(IEmployeeVM parameter)
        {
            // base.RemoveEmployeeMethod(parameter);

            if (!RemoveEmployeeCanMethod(parameter))
                return;

            EmployeeVM empl = (EmployeeVM)parameter;
            model.RemoveEmployee(empl.Dto);
        }

        protected override void SaveEditEmployeeMethod(IEmployeeVM parameter)
        {
            // base.SaveEditEmployeeMethod(parameter);

            if (!SaveEditEmployeeCanMethod(parameter))
                return;


            EmployeeVM empl = (EmployeeVM)parameter;
            model.ChangeEmployee(empl.Dto, empl.Copy());

            ModeExitMethod(ViewModeEnum.Editing);
        }
    }
}
