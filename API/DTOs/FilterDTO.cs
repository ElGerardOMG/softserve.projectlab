namespace API.DTOs
{
    public class FilterDTO
    {
        public ICollection<FilterField>? Filters { get; set; }


    }

    public class FilterField
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
        public bool? IsLike { get; set; }
    }
}
