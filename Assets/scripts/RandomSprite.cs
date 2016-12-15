using UnityEngine;
using System.Collections.Generic;

public class RandomSprite : MonoBehaviour {
  public List<Sprite> sprites;
  public string sharedDeckName;

  private ShuffleDeck spriteDeck;
  private SpriteRenderer renderer;

  private static Dictionary<string, ShuffleDeck> sharedDecks = new Dictionary<string, ShuffleDeck>();

  void Awake() {
    if (sharedDeckName.Length > 0) {
      if (!sharedDecks.TryGetValue(sharedDeckName, out spriteDeck)) {
        spriteDeck = new ShuffleDeck(sprites);
        sharedDecks[sharedDeckName] = spriteDeck;
      }
    } else {
      spriteDeck = new ShuffleDeck(sprites);
    }

    renderer = GetComponent<SpriteRenderer>();
  }

  void OnEnable() {
    RandomizeSprite();
  }

  public void RandomizeSprite() {
    renderer.sprite = (Sprite) spriteDeck.Draw();
  }
}
