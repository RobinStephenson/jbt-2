using UnityEngine;
using System.Collections;

public class TileObject
{
  private Vector2 position;
  private Vector2 size;

  public TileObject(Vector2 pos, Vector2 dim)
  {
      this.position = pos;
      this.size = dim;
  }
}
