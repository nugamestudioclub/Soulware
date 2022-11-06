using Godot;
using System;

[Tool]
public class Autotile : TileSet
{
	public override bool _IsTileBound(int drawnId, int neighborId)
	{
		return GetTilesIds().Contains(neighborId);
	}
}
