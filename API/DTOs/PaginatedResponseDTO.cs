namespace API.DTOs
{
    public class PaginatedResponseDTO<T>
    {
        public List<T>? Data { get; set; }
        public PaginationDataDTO? PaginationData { get; set; }
    }

    public class PaginationDataDTO
    {
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
