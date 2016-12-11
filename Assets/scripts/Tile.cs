using UnityEngine;

public class Tile : ScriptableObject {
  public enum Orientation {UP, DOWN, LEFT, RIGHT};

  public GameObject layerBase;
  public GameObject layerIcon;
  public Orientation orientation;

  // public float width { get { return layerBase.GetComponent< }}
}
