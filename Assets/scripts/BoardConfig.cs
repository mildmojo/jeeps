using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class BoardConfig {
  public int seed = 0;

  public List<NamedTiles> tilePrefabs;
}

[System.Serializable]
public class NamedTiles {
  public string name;
  public List<Tile> tiles;
}
