using Staffing.DTO;
using Staffing.InterfacesVM;
using System;

namespace Staffing.ViewUC
{
    public class DesignEmployee : IEmployeeVM
    {
        public int Id { get; }
        public string FirstName { get; set; }
        public PositionDto Position { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age => (int)((DateTime.Now - DateOfBirth).TotalDays / 365.25);
        public string About => FirstName + "   " + Position.Title + "    " + Age;

        public DesignEmployee(int id, string firstName, PositionDto position, DateTime dateOfBirth)
        {
            Id = id;
            FirstName = firstName;
            Position = position;
            DateOfBirth = dateOfBirth;
        }
    }
}
