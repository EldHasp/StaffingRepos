using Common;
using Staffing.DTO;
using Staffing.InterfacesVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Staffing.ViewModel
{
    public class OnlyViewVM : OnPropertyChangedClass, IMainViewModel
    {
        private ViewModeEnum _viewMode;
        public ViewModeEnum ViewMode { get => _viewMode; private set => SetProperty(ref _viewMode, value); }

        public ObservableCollection<IEmployeeVM> Employees { get; } = new ObservableCollection<IEmployeeVM>();

        private IEmployeeVM _selectedEmployee;
        public IEmployeeVM SelectedEmployee { get => _selectedEmployee; set => SetProperty(ref _selectedEmployee, value); }

        private RelayCommand<ViewModeEnum> _modeCommand;
        public RelayCommand<ViewModeEnum> ModeCommand => _modeCommand
            ?? (_modeCommand = new RelayCommand<ViewModeEnum>(ModeMethod, ModeCanMethod));

        /// <summary>Метод состояния команды переключения Режимов Представления.</summary>
        /// <param name="parameter">Требуемый Режим.</param>
        /// <returns><see langword="true"/>, если переключиться в Режим возможно.</returns>
        private bool ModeCanMethod(ViewModeEnum parameter)
        {
            switch (parameter)
            {
                case ViewModeEnum.Empty:
                    return SelectedEmployee == null;
                case ViewModeEnum.View:
                    return SelectedEmployee != null;
                case ViewModeEnum.Adding:
                    return true;
                case ViewModeEnum.Editing:
                    return ViewMode == ViewModeEnum.View;
                default:
                    throw new ArgumentException(nameof(parameter));
            }
        }

        /// <summary>Метод команды переключения Режимов Представления.</summary>
        /// <param name="parameter">Требуемый Режим.</param>
        private void ModeMethod(ViewModeEnum parameter)
        {
            // Проверка состояния команды.
            if (!ModeCanMethod(parameter))
                return;

            switch (parameter)
            {
                // Переключение в режим Empty.
                case ViewModeEnum.Empty:
                    ViewMode = ViewModeEnum.Empty;
                    break;

                // Переключение в режим View.
                case ViewModeEnum.View:
                    ViewMode = ViewModeEnum.View;
                    break;

                // Создание в свойстве AddEmployee нового экземпляра EmployeeVM
                // и переключение в режим Adding.
                case ViewModeEnum.Adding:
                    AddEmployee = new EmployeeVM();
                    ViewMode = ViewModeEnum.Adding;
                    break;

                // Создание в свойстве EditEmployee клона экземпляра EmployeeVM 
                // из свойства SelectedEmployee и переключение в режим Adding.
                case ViewModeEnum.Editing:
                    EditEmployee = ((EmployeeVM)SelectedEmployee).Clone();
                    ViewMode = ViewModeEnum.Editing;
                    break;

                default:
                    throw new ArgumentException(nameof(parameter));
            }

        }

        private RelayCommand<IEmployeeVM> _removeEmployeeCommand;
        public RelayCommand<IEmployeeVM> RemoveEmployeeCommand => _removeEmployeeCommand
            ?? (_removeEmployeeCommand = new RelayCommand<IEmployeeVM>(RemoveEmployeeMethod, RemoveEmployeeCanMethod));

        /// <summary>Метод состояния команды удаления Сотрудника из списка.
        /// В производном классе должен быть заменён на вызов метода Модели.</summary>
        /// <param name="parameter">Удаляемый Сотрудник.</param>
        /// <returns><see langword="true"/>, если Сотрудник есть в списке.</returns>
        protected virtual bool RemoveEmployeeCanMethod(IEmployeeVM parameter)
        {
            return Employees.Contains(parameter);
        }

        /// <summary>Метод команды удаления Сотрудника из списка.
        /// В производном классе должен быть заменён на вызов метода Модели.</summary>
        /// <param name="parameter">Удаляемый Сотрудник.</param>
        protected virtual void RemoveEmployeeMethod(IEmployeeVM parameter)
        {
            if (!RemoveEmployeeCanMethod(parameter))
                return;

            Employees.Remove(parameter);
        }

        public ObservableCollection<PositionDto> Positions { get; } = new ObservableCollection<PositionDto>();

        private IEmployeeVM _addEmployee;
        public IEmployeeVM AddEmployee { get => _addEmployee; private set => SetProperty(ref _addEmployee, value); }

        private RelayCommand<IEmployeeVM> _addEmployeeCommand;
        public RelayCommand<IEmployeeVM> AddEmployeeCommand => _addEmployeeCommand
            ?? (_addEmployeeCommand = new RelayCommand<IEmployeeVM>(AddEmployeeMethod, AddEmployeeCanMethod));

        /// <summary>Метод состояния команды добавления Сотрудника в список.
        /// В производном классе должен быть заменён на вызов метода Модели.</summary>
        /// <param name="parameter">Добавляемый Сотрудник.</param>
        /// <returns><see langword="true"/>, если Сотрудника нет в списке 
        /// и свойства имеют допустимые значения.</returns>
        protected virtual bool AddEmployeeCanMethod(IEmployeeVM parameter)
        {
            return !Employees.Contains(parameter)
                && !string.IsNullOrWhiteSpace(parameter.FirstName)
                && parameter.DateOfBirth >= new DateTime(1950, 1, 1)
                && parameter.DateOfBirth < DateTime.Now.AddYears(-18)
                && Positions.Contains(parameter.Position);
        }

        /// <summary>Метод команды добавления Сотрудника в список.
        /// В производном классе должен быть заменён на вызов метода Модели.</summary>
        /// <param name="parameter">Добавляемый Сотрудник.</param>
        protected virtual void AddEmployeeMethod(IEmployeeVM parameter)
        {
            if (!AddEmployeeCanMethod(parameter))
                return;

            EmployeeVM empl = (EmployeeVM)parameter;
            empl.SetDto(new EmployeeDto(random.Next(), empl.FirstName, empl.Position, empl.DateOfBirth));
            Employees.Add(empl);

            ModeExitMethod(ViewModeEnum.Adding);
        }

        protected static readonly Random random = new Random();

        private RelayCommand<ViewModeEnum> _modeExitCommand;
        public RelayCommand<ViewModeEnum> ModeExitCommand => _modeExitCommand
            ?? (_modeExitCommand = new RelayCommand<ViewModeEnum>(ModeExitMethod, ModeExiCantMethod));

        /// <summary>Метод состояния команды выхода из Режима.</summary>
        /// <param name="mode">Режим из которого надо выйти.</param>
        /// <returns><see langword="true"/>, если текущий Режим совпадает с указанным.</returns>
        protected bool ModeExiCantMethod(ViewModeEnum mode)
        {
            return mode != ViewModeEnum.Empty && mode != ViewModeEnum.View
                   && mode == ViewMode;
        }

        /// <summary>Метод команды выхода из Режима.</summary>
        /// <param name="mode">Режим из которого надо выйти.</param>
        protected void ModeExitMethod(ViewModeEnum mode)
        {
            if (!ModeExiCantMethod(mode))
                return;

            // Если нет выбранного Сотрудник, то переход в Режим Empty.
            // Иначе -  в Режим View.
            if (SelectedEmployee == null)
                ModeMethod(ViewModeEnum.Empty);
            else
                ModeMethod(ViewModeEnum.View);

            switch (mode)
            {
                case ViewModeEnum.Adding:
                    AddEmployee = null;
                    break;
                case ViewModeEnum.Editing:
                    EditEmployee = null;
                    break;
                default:
                    throw new ArgumentException(nameof(mode)); ;
            }
        }

        private IEmployeeVM _editEmployee;
        public IEmployeeVM EditEmployee { get => _editEmployee; private set => SetProperty(ref _editEmployee, value); }

        private RelayCommand<IEmployeeVM> _saveEditEmployeeCommand;
        public RelayCommand<IEmployeeVM> SaveEditEmployeeCommand => _saveEditEmployeeCommand
            ?? (_saveEditEmployeeCommand = new RelayCommand<IEmployeeVM>(SaveEditEmployeeMethod, SaveEditEmployeeCanMethod));

        /// <summary>Метод состояния команды изменения данных Сотрудника.
        /// В производном классе должен быть заменён на вызов метода Модели.</summary>
        /// <param name="parameter">Новые Данные Сотрудника.</param>
        /// <returns><see langword="true"/>, если Сотрудника с таким Id нет в списке
        /// и свойства имеют допустимые значения.</returns>
        protected virtual bool SaveEditEmployeeCanMethod(IEmployeeVM parameter)
        {
            return Employees.Any(empl => empl.Id == parameter.Id)
                && !string.IsNullOrWhiteSpace(parameter.FirstName)
                && parameter.DateOfBirth >= new DateTime(1950, 1, 1)
                && parameter.DateOfBirth < DateTime.Now.AddYears(-18)
                && Positions.Contains(parameter.Position); ;
        }

        /// <summary>Метод команды изменения данных Сотрудника.
        /// В производном классе должен быть заменён на вызов метода Модели.</summary>
        /// <param name="parameter">Новые Данные Сотрудника.</param>
        protected virtual void SaveEditEmployeeMethod(IEmployeeVM parameter)
        {
            if (!SaveEditEmployeeCanMethod(parameter))
                return;

            EmployeeVM employee = (EmployeeVM)Employees.First(empl => empl.Id == parameter.Id);
            employee.SetDto(((EmployeeVM)parameter).Copy());

            ModeExitMethod(ViewModeEnum.Editing);
        }

        protected override void PropertyNewValue<T>(ref T fieldProperty, T newValue, string propertyName)
        {
            base.PropertyNewValue(ref fieldProperty, newValue, propertyName);

            if (nameof(SelectedEmployee) == propertyName)
            {
                if (ViewMode == ViewModeEnum.Empty && SelectedEmployee != null)
                    ViewMode = ViewModeEnum.View;
                else if (ViewMode == ViewModeEnum.View && SelectedEmployee == null)
                    ViewMode = ViewModeEnum.Empty;
            }
        }

        /// <summary>Конструктор по умолчанию.
        /// Вызывается если не нужна связь с Моделью.</summary>
        public OnlyViewVM() : this(true) { }

        /// <summary>Универсальный конструктор.
        /// Может быть вызван только из производного класса.</summary>
        /// <param name="onlyView"><see langword="true"/>, если нужна
        /// демонстрационная инициализация коллекций.</param>
        protected OnlyViewVM(bool onlyView)
        {
            if (!onlyView)
                return;

            new List<PositionDto>
                {
                    new PositionDto(1, "Директор"),
                    new PositionDto(15, "Начальник отдела"),
                    new PositionDto(7, "Старший сотрудник"),
                    new PositionDto(13, "Младщий сотрудник"),
                }
            .ForEach(pst => Positions.Add(pst));

            new List<EmployeeVM>
            {
                new EmployeeVM(new EmployeeDto(279, "Пётр", Positions[0], new DateTime(1980,10,25))),
                new EmployeeVM(new EmployeeDto(654, "Фёдор", Positions[3], new DateTime(1990,10,25))),
                new EmployeeVM(new EmployeeDto(941, "Иван", Positions[2], new DateTime(1985,10,25))),
                new EmployeeVM(new EmployeeDto(692, "Сидор", Positions[1], new DateTime(1995,10,25))),
                new EmployeeVM(new EmployeeDto(395, "Миша", Positions[3], new DateTime(1988,10,25)))
            }
            .ForEach(empl => Employees.Add(empl));
        }
    }
}
