namespace Hoteis.Models
{
    public class DetailsDto
    {
        public Hotel Prior { get; set; }
        public Hotel Current { get; set; }
        public Hotel Next { get; set; }
        public List<Estado> Estados{ get; set; }        
    }
}