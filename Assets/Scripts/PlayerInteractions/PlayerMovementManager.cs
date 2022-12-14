using System;
using GameManager;
using Glades;
using PlayerInteractions.StaticEvents;
using UI;
using UnityEngine;

namespace PlayerInteractions
{
    /// <summary>
    /// A class that implements player's movement behaviour.
    /// </summary>
    public class PlayerMovementManager : MonoBehaviour
    {
        private SpawnedGlade currentOccupiedGlade;

        private void OnEnable()
        {
            PlayerMovementStaticEvents.SubscribeToTryMovePlayerToGlade(TryMovePlayer);
        }

        private void OnDisable()
        {
            PlayerMovementStaticEvents.UnsubscribeFromTryMovePlayerToGlade(TryMovePlayer);
        }

        /// <summary>
        /// Sets player's position.
        /// </summary>
        /// <param name="destination"> New position. </param>
        private void MovePlayer(Vector2 destination)
        {
            this.gameObject.transform.position = destination;
        }

        /// <summary>
        /// Tries move player to given glade.
        /// </summary>
        /// <param name="destination"> Target glade. </param>
        /// <param name="forced"> Information if move is forced and shouldn't consider available paths.. </param>
        private void TryMovePlayer(SpawnedGlade destination, bool forced)
        {
            if (currentOccupiedGlade == null || forced)
            {
                MoveToGlade(destination);
            }
            else
            {
                var xDiff = Mathf.Abs(currentOccupiedGlade.GridCell.PositionInGrid.X -
                                      destination.GridCell.PositionInGrid.X);
                var yDiff = Mathf.Abs(currentOccupiedGlade.GridCell.PositionInGrid.Y -
                                      destination.GridCell.PositionInGrid.Y);

                if ((xDiff == 1 && yDiff == 0)
                    || (yDiff == 1 && xDiff == 0))
                {
                    if (currentOccupiedGlade
                        .AdjacentGlades[
                            GetAdjacentSide(currentOccupiedGlade.GridCell.PositionInGrid.Position,
                                destination.GridCell.PositionInGrid.Position)].Type != AdjacentType.Blocked)
                    {
                        MoveToGlade(destination);
                    }
                }
            }
        }

        /// <summary>
        /// Moves player to glade.
        /// </summary>
        /// <param name="destination"> Target glade. </param>
        private void MoveToGlade(SpawnedGlade destination)
        {
            currentOccupiedGlade = destination;
            MovePlayer(destination.SpawnPosition);
            GameManager.GameController.GetInstance().CurrentGladeID = destination.Id;
            GameManager.GameController.GetInstance().CurrentGlade = destination;
            PlayerMovementStaticEvents.InvokePlayerMovedToGlade(destination);
            destination.Glade.OnPlayerArrived?.Invoke();
        }

        /// <summary>
        /// Checks adjacent side. 
        /// </summary>
        /// <param name="gladePos"> Current glade position. </param>
        /// <param name="adjacentGladePos"> Adjacent glade position. </param>
        /// <returns> Adjacent side type. </returns>
        private AdjacentSide GetAdjacentSide(Vector2 gladePos, Vector2 adjacentGladePos)
        {
            if (adjacentGladePos.x - gladePos.x < 0)
                return AdjacentSide.Left;
            if (adjacentGladePos.x - gladePos.x > 0)
                return AdjacentSide.Right;
            if (adjacentGladePos.y - gladePos.y > 0)
                return AdjacentSide.Up;

            return AdjacentSide.Down;
        }
    }
}