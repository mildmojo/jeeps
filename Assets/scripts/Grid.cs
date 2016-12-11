using UnityEngine;
using System.Collections.Generic;

public class Grid {
  public int width;
  public int height;

  private System.Random rnd;
  private List<Tile> tiles;

  public Grid(int w, int h, Sprite boardSprite, int seed = 0) {
    int x;
    int y;
    width = w;
    height = h;

    rnd = new System.Random();
    if (seed > 0) rnd = new System.Random(seed);

    for (var i = w * h; i > 0; i--) {
      var tile = Tile.Create(boardSprite, null);
      var tileRect = new TwoDee(tile.gameObject);
    }


  }

}
