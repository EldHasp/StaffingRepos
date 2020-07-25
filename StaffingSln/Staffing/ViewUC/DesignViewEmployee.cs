using Staffing.DTO;
using Staffing.InterfacesVM;
using System;

namespace Staffing.ViewUC
{
    public class DesignViewEmployee : ISelectedEmployee
    {
        public IEmployeeVM SelectedEmployee { get; }
            = new DesignEmployee(752, "Ivan", new PositionDto(49, "Сотрудник"), new DateTime(1980, 9, 25));
    }
}
