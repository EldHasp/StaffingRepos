namespace Staffing.Common
{
    /// <summary>Интерфейс копирования.</summary>
    /// <typeparam name="T">Тип для копирования.</typeparam>
    public interface ICopy<T>
    {
        /// <summary>Возвращает экземпляр заданного типа с копией данных.</summary>
        /// <returns>Новый экземпляр заданного типа.</returns>
        T Copy();

        /// <summary>Копирует данные из переданного объекта.</summary>
        /// <param name="obj">Объект с данными.</param>
        void CopyFrom(T obj);

        /// <summary>Копирует данные в переданный объект.</summary>
        /// <param name="obj">Объект для данных.</param>
        void CopyTo(T obj);
    }
}
