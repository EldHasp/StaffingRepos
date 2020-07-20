namespace Staffing.Common
{
    /// <summary>Интерфейс связи с DTO контейнером.</summary>
    /// <typeparam name="T">DTO тип.</typeparam>
    public interface IDto<T> : ICopy<T>
    {
        /// <summary>Ссылка на DTO контейнер.</summary>
        T Dto { get; }
    }
}
