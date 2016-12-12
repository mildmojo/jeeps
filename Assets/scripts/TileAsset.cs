using UnityEngine;

public class TileAsset : ScriptableObject {
  public enum Orientation {UP, DOWN, LEFT, RIGHT};

  public Sprite layerBase;
  public Sprite layerIcon;
  public Orientation orientation;

  // public float x          { get { return twodee.x; } }
  // public float y          { get { return twodee.y; } }
  // public float width      { get { return twodee.width; } }
  // public float height     { get { return twodee.height; } }
  // public float left       { get { return twodee.left; } }
  // public float top        { get { return twodee.top; } }
  // public Vector3 position { get { return twodee.position; } }
  // public Vector3 scale    { get { return twodee.scale; } }

  // private TwoDee twodee;

  public abstract void OnPlayerEnter();
  public abstract void OnPlayerArrive();
  public abstract void OnPlayeExit();
}
