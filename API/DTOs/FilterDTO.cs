namespace API.DTOs
{
    public class FilterDTO
    {
        public ICollection<FilterField>? Filters { get; set; }
    }

    public class GenericFilterDTO
    {
        public bool? IsActive { get; set; }
        public string? Status { get; set; }
        public bool? OrderByStatus { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? OrderByDate { get; set; }
    }


    public class FilterField
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
        public bool? IsLike { get; set; }
    }
}
