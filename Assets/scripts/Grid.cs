using UnityEngine;
using System.Collections;

public class Grid {
  public int width;
  public int height;

  private BoardConfig config;
  private System.Random rnd;
  private List<Tile> tiles;

  public void Create(int w, int h, BoardConfig cfg) {
    width = w;
    height = h;
    config = cfg;

    rnd = new System.Random();
    if (cfg.seed > 0) rnd = new System.Random(cfg.seed);

  }


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
