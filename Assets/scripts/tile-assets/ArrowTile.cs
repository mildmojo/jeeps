using UnityEngine;
using System.Collections.Generic;

public class ArrowTile : MonoBehaviour {
  public enum Orientation {UP, DOWN, LEFT, RIGHT};
  public Orientation direction;

  private Vector3 dirVector;
  private Dictionary<Orientation, Vector3> dirToVector = new Dictionary<Orientation, Vector3> {
    { Orientation.UP, new Vector3(0,1,0) },
    { Orientation.DOWN, new Vector3(0,-1,0) },
    { Orientation.LEFT, new Vector3(-1,0,0) },
    { Orientation.RIGHT, new Vector3(1,0,0) },
  };

  void Awake() {
    dirVector = dirToVector[direction];
    var overlaySprite = transform.FindChild("TileOverlay");
    overlaySprite.up = dirVector;
  }

  void OnPlayerArrive(object p) {
    var player = (PlayerController) p;
    player.SetDirection(dirVector);
    Debug.Log("Finished OnPlayerArrive in ArrowTile");
  }

  // public override void OnPlayerEnter(PlayerController player, List<Tile> board) {
  //   throw new System.NotImplementedException();
  // }

  // public override void OnPlayerArrive(PlayerController player, List<Tile> board) {
  //   player.direction = direction;
  // }

  // public override void OnPlayeExit(PlayerController player, List<Tile> board) {
  //   throw new System.NotImplementedException();
  // }
}
