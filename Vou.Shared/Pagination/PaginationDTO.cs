namespace Vou.Shared.Pagination
{
    public class PaginationDTO
    {
        public int Id { get; set; }

        public int Page { get; set; } = 1;

        public int RecordsNumber { get; set; } = 20;

        public string? Filter { get; set; }
    }
}
