namespace Catalog.Domain.Entities
{
    public class Entity
    {
        public Entity()
        {
        }

        public Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}