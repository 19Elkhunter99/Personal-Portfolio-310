namespace WildlifeTracker
{
    public class Species
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Season { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Name} ({Season})";
        }
    }
}
