using Common;
using Staffing.Common;
using Staffing.DTO;
using Staffing.InterfacesVM;
using System;
using System.Linq;

namespace Staffing.ViewModel
{
    /// <summary>Класс Сотрудника со свойствами для использовнаия в WPF View и ViewModel.</summary>
    public class EmployeeVM : OnPropertyChangedClass, IEmployeeVM, ICloneable<EmployeeVM>, IDto<EmployeeDto>
    {
        #region Поля для хранения хначений свойств
        private int _id;
        private string _firstName;
        private PositionDto _position;
        private DateTime _dateOfBirth;
        private int _age;
        private string _about;
        private EmployeeDto _dto;
        #endregion

        #region Свойства
        public int Id { get => _id; private set => SetProperty(ref _id, value); }
        public string FirstName { get => _firstName; set => SetProperty(ref _firstName, value); }
        public PositionDto Position { get => _position; set => SetProperty(ref _position, value); }
        public DateTime DateOfBirth { get => _dateOfBirth; set => SetProperty(ref _dateOfBirth, value); }
        public int Age { get => _age; private set => SetProperty(ref _age, value); }
        public string About { get => _about; private set => SetProperty(ref _about, value); }
        public EmployeeDto Dto { get => _dto; private set => SetProperty(ref _dto, value); }
        #endregion

        #region Методы
        public EmployeeVM Clone()
            => (EmployeeVM)((ICloneable)this).Clone();

        public EmployeeDto Copy()
            => new EmployeeDto(Id, FirstName, Position, DateOfBirth);

        public void CopyFrom(EmployeeDto obj)
        {
            Id = obj.Id;
            FirstName = obj.Name;
            Position = obj.Position;
            DateOfBirth = obj.DateOfBirth;
        }

        public void CopyTo(EmployeeDto obj)
        {
            throw new NotImplementedException();
        }

        object ICloneable.Clone() => MemberwiseClone();

        public EmployeeVM() { }

        public EmployeeVM(EmployeeDto dto) => SetDto(dto);

        public void SetDto(EmployeeDto dto)
            => CopyFrom(Dto = dto);
        #endregion

        #region "Прослушка" изменениий значений свойств
        protected override void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
        {
            base.PropertyNewValue(ref fieldProperty, newValue, propertyName);

            // Изменить значение About, если изменилось значение одного из свойств: FirstName, Position, Age.
            if (new string[] { nameof(FirstName), nameof(Position), nameof(Age) }.Contains(propertyName))
                About = FirstName + "   " + Position?.Title + "    " + Age;

            // Изменить значение Age, если изменилось значение DateOfBirth.
            if (nameof(DateOfBirth) == propertyName)
                Age= (int)((DateTime.Now - DateOfBirth).TotalDays / 365.25);
        }
        #endregion
    }
}
