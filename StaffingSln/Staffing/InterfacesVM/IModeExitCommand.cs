using Common;

namespace Staffing.InterfacesVM
{
    /// <summary>Интерфейс >Команда выхода из Режима Отображения</summary>
    public interface IModeExitCommand
    {
        /// <summary>Команда выхода из указанного Режима Отображения.</summary>
        RelayCommand<ViewModeEnum> ModeExitCommand { get; }
    }
}
