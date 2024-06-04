namespace API.Models.ModelInterfaces
{
    public interface IBaseModel
    {
        public int Id { get; set; }
        bool? IsActive { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
