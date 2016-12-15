using UnityEngine;

public class TwoDee {
  public GameObject gameObject;

  public TwoDee(GameObject newGameObj) {
    gameObject = newGameObj;
  }

  public float x          { get { return gameObject.transform.position.x; }
                            set { gameObject.transform.position = Vector3.right * value; } }
  public float y          { get { return gameObject.transform.position.y; }
                            set { gameObject.transform.position = Vector3.up * value; } }
  public float left       { get { return x - width / 2; }
                            set { x = value + width / 2; } }
  public float top        { get { return y + height / 2; }
                            set { y = value - height / 2; } }
  public Vector3 position { get { return gameObject.transform.position; }
                            set { gameObject.transform.position = value; } }
  public Vector3 scale    { get { return gameObject.transform.localScale; }
                            set { gameObject.transform.localScale = value; } }
  public float height     { get { return dimensions().y; } }
  public float width      { get { return dimensions().x; } }
  public Transform parent { get { return gameObject.transform.parent; }
                            set { gameObject.transform.parent = value; } }

  private Vector3 dimensions() {
    return gameObject.GetComponent<SpriteRenderer>().bounds.size;
  }
}
