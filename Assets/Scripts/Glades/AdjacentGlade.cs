
namespace Glades
{
    public class AdjacentGlade
    {
        public string cellID;

        public AdjacentType type;
        
        //public AdjacentGlade(SpawnedGladeGlade glade)
        //  public AdjacentSide side;

        public AdjacentGlade(AdjacentType type)
        {
            this.type = type;
        }
    }
}