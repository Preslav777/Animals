namespace AnimalsProject.Models
{
    public class Animals
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Age { get; set; }
        public int BreedId { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}