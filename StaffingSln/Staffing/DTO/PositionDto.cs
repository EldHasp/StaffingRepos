namespace Staffing.DTO
{
    /// <summary>Неизменяемый класс для должности.</summary>
    public class PositionDto
    {
        /// <summary>Уникальный идентификатор.</summary>
        public int Id { get; }
        /// <summary>Название.</summary>
        public string Title { get; }

        /// <summary>Конструктор задающий все значения.</summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="title">Название.</param>
        public PositionDto(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
