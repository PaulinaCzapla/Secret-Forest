using UnityEngine;

namespace Gameplay.LevelGenerating
{
    public class RoomsGrid : MonoBehaviour
    {
        [SerializeField] private GridSettings gridSettings;

        
        public int columns = 5;
        public int rows = 5;
        private bool draw = false;
        
        private void OnDrawGizmos()
        {
            int spaceMultiplier = 0;
            int heightMultiplier = 0;
            for (int i = 0; i < 2 * rows; i++)
            {
                if (i > 0)
                {
                    if (i % 2 == 0)
                    {
                        spaceMultiplier++;
                    }
                    else
                    {
                        heightMultiplier++;
                    }
                }

                Gizmos.DrawLine(
                    new Vector3(0,
                        gridSettings.roomHeight * heightMultiplier + gridSettings.spaceBetween * spaceMultiplier, 0),
                    new Vector3(columns * gridSettings.roomWidth + (columns - 1) * gridSettings.spaceBetween,
                        gridSettings.roomHeight * heightMultiplier + gridSettings.spaceBetween * spaceMultiplier));
            }
            
            spaceMultiplier = 0;
            int widthMultiplier = 0;
            for (int i = 0; i < 2 * rows; i++)
            {
                if (i > 0)
                {
                    if (i % 2 == 0)
                        spaceMultiplier++;
                    else
                        widthMultiplier++;
                }

                Gizmos.DrawLine(
                    new Vector3(gridSettings.roomWidth * widthMultiplier + gridSettings.spaceBetween * spaceMultiplier,
                        0, 0),
                    new Vector3(gridSettings.roomWidth * widthMultiplier + gridSettings.spaceBetween * spaceMultiplier,
                        rows * gridSettings.roomHeight + (rows - 1) * gridSettings.spaceBetween));
            }
        }
    }
}