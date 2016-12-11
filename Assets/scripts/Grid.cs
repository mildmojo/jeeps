using UnityEngine;
using System.Collections.Generic;

public class Grid {
  public int width;
  public int height;

  private System.Random rnd;
  private List<Tile> tiles;

  public void Create(int w, int h, Sprite boardSprite, int seed = 0) {
    width = w;
    height = h;

    rnd = new System.Random();
    if (seed > 0) rnd = new System.Random(seed);

    for (var i = w * h; i > 0; i--) {
      tiles.Add(Tile.Create(boardSprite, null));
    }
  }


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
