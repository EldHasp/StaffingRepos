using System;

namespace Staffing.Common
{
    /// <summary>Интерфейс клонирования экземпляра.</summary>
    /// <typeparam name="T">Тип экземпляра.</typeparam>
    public interface ICloneable<T> : ICloneable
    {
        /// <summary>Возвращает клон экземпляра в указанном типе.</summary>
        /// <returns>Типизированный клон.</returns>
        new T Clone();
    }
}
