using UnityEngine;
using System.Collections.Generic;

public abstract class TileAsset : MonoBehaviour {
  public enum Orientation {UP, DOWN, LEFT, RIGHT};

  public Orientation iconOrientation;
  public Sprite layerBaseSprite;
  public Sprite layerIconSprite;
  public GameObject baseLayer;
  public GameObject overlayLayer;


  // public float x          { get { return twodee.x; } }
  // public float y          { get { return twodee.y; } }
  // public float width      { get { return twodee.width; } }
  // public float height     { get { return twodee.height; } }
  // public float left       { get { return twodee.left; } }
  // public float top        { get { return twodee.top; } }
  // public Vector3 position { get { return twodee.position; } }
  // public Vector3 scale    { get { return twodee.scale; } }

  // private TwoDee twodee;

  void OnAwake() {
    if (layerBaseSprite != null) baseLayer.GetComponent<SpriteRenderer>().sprite = layerBaseSprite;
    overlayLayer.GetComponent<SpriteRenderer>().sprite = layerIconSprite;
    Debug.Log("Icon sprite was null: " + (layerIconSprite == null));
  }

  public abstract void OnPlayerEnter(PlayerController player, List<Tile> board);
  public abstract void OnPlayerArrive(PlayerController player, List<Tile> board);
  public abstract void OnPlayeExit(PlayerController player, List<Tile> board);
}
