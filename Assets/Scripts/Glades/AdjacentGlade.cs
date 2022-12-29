
namespace Glades
{
    /// <summary>
    /// Represents glade that is adjacent to other glade.
    /// </summary>
    public class AdjacentGlade
    {
        public AdjacentType Type { get; private set; }
        public SpawnedGlade SpawnedGlade { get; private set; }
        
        public AdjacentGlade(AdjacentType type, SpawnedGlade glade)
        {
            this.Type = type;
            SpawnedGlade = glade;
        }
    }
}