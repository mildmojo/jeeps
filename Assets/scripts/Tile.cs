using UnityEngine;

[RequireComponent (typeof(Sprite))]
public class Tile : MonoBehaviour {
  /*
    Tracks:
      GameObject (actual in-scene object)
      Tile type/sprite
      Overlay type/sprite
      Overlay behavior?

      OnTriggerEnter
      OnTriggerStay
      OnTriggerExit
  */

  // public enum Types {STANDARD, ICE};

  [HideInInspector] [System.NonSerialized]
  public int x;
  [HideInInspector] [System.NonSerialized]
  public int y;

  // public Sprite baseSprite;
  // public Sprite overlaySprite;
  // public GameObject baseLayer;
  // public GameObject overlayLayer;

  // private bool isInitialized;

  // public static Tile Create(Sprite baseTileSprite, Sprite overlayTileSprite = null) {
  //   var gobj = new GameObject("Tile");
  //   var tile = gobj.AddComponent<Tile>();
  //   tile.Init(baseTileSprite, overlayTileSprite);
  //   return tile;
  // }

  // void Init(Sprite baseTileSprite, Sprite overlayTileSprite = null) {
  //   if (isInitialized) return;

  //   isInitialized = true;

  //   baseSprite = baseTileSprite;
  //   overlaySprite = overlayTileSprite;

  //   baseLayer = addLayerChild("TileBaseLayer", baseSprite);
  //   overlayLayer = addLayerChild("TileOverlayLayer", overlaySprite);
  // }

  // private GameObject addLayerChild(string objName, Sprite sprite) {
  //   var layer = new GameObject(objName);
  //   var spriteComponent = baseLayer.AddComponent<SpriteRenderer>();
  //   spriteComponent.sprite = sprite;
  //   layer.transform.parent = this.gameObject.transform;
  //   return layer;
  // }
}
