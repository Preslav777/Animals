using System.Collections.Generic;

namespace AnimalsProject.Models
{
    public class Breeds
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Animals> Animals { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}