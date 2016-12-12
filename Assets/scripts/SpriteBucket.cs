using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteBucket : MonoBehaviour {
  public List<Sprite> sprites;

  [HideInInspector] [System.NonSerialized]
  public ShuffleDeck deck;

  void Awake() {
    deck = new ShuffleDeck(sprites);
  }
}
