
namespace Glades
{
    public class AdjacentGlade
    {
        public AdjacentType Type { get; private set; }
        public SpawnedGlade SpawnedGlade { get; private set; }
        
        //public AdjacentGlade(SpawnedGladeGlade glade)
        //  public AdjacentSide side;

        public AdjacentGlade(AdjacentType type, SpawnedGlade glade)
        {
            this.Type = type;
            SpawnedGlade = glade;
        }
    }
}