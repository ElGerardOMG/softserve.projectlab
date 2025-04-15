namespace API.Utils.Interfaces
{
    public interface IBaseClass
    {
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public bool? IsActive { get; set; }
    }
}
