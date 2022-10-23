using System;
using LevelGenerating.LevelGrid;
using PlayerInteractions.StaticEvents;
using UnityEngine;

namespace PlayerInteractions
{
    public class Player : MonoBehaviour
    {
       //public GridCell 

       // private void OnEnable()
       // {
       //     PlayerMovementStaticEvents.SubscribeToMovePlayerToPosition(MovePlayer);
       // }

       private void MovePlayer(Vector2 arg0)
       {
           this.gameObject.transform.position = arg0;
       }
    }
}