using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class BoardConfig {
  public int seed = 0;

}

[System.Serializable]
public class NamedTiles {
  public string name;
  public List<Tile> tiles;
}
