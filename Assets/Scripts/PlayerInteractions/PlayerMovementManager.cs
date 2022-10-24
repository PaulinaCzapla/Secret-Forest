﻿using System;
using Glades;
using PlayerInteractions.StaticEvents;
using UnityEngine;

namespace PlayerInteractions
{
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

        private void MovePlayer(Vector2 destination)
        {
            this.gameObject.transform.position = destination;
        }

        private void TryMovePlayer(SpawnedGlade destination, bool forced)
        {
            Debug.Log("move playre");
            if (currentOccupiedGlade == null || forced)
            {
                currentOccupiedGlade = destination;
                MovePlayer(destination.SpawnPosition);
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
                    Debug.Log("Targeted glade is adjacent");
                    Debug.Log(GetAdjacentSide(currentOccupiedGlade.GridCell.PositionInGrid.Position,
                        destination.GridCell.PositionInGrid.Position));
                    if (currentOccupiedGlade
                        .AdjacentGlades[
                            GetAdjacentSide(currentOccupiedGlade.GridCell.PositionInGrid.Position,
                                destination.GridCell.PositionInGrid.Position)].type != AdjacentType.Blocked)
                    {
                        currentOccupiedGlade = destination;
                        MovePlayer(destination.SpawnPosition);
                    }
                }
            }
        }


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