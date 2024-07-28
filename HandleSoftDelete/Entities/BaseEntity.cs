namespace HandleSoftDelete.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; internal set; }
        public DateTime? DeletedAt { get; set; }
    }

}
