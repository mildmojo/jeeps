using UnityEngine;
using System.Collections.Generic;

public class ArrowTile : TileAsset {
  public Vector3 direction;

  public override void OnPlayerEnter(PlayerController player, List<Tile> board) {
    throw new System.NotImplementedException();
  }

  public override void OnPlayerArrive(PlayerController player, List<Tile> board) {
    player.direction = direction;
  }

  public override void OnPlayeExit(PlayerController player, List<Tile> board) {
    throw new System.NotImplementedException();
  }
}
