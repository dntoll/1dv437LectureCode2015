#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace LectureExample
{
	public class Map
{
		public float getRoofHeight(float modelX) {
			return ((float)Math.Sin(modelX * 4.0f) + 1.0f) * 0.25f;
		}

		public float getFloorHeight(float modelX) {
			return getRoofHeight (modelX) + 0.5f;
		}

		public float GetEndPosition ()
		{
			return 10;
		}

		public Vector2[] GetEnemystartPositions ()
		{
			Vector2[] ret = new Vector2[GameModel.MAX_ENEMIES];

			for (int i = 0; i < GameModel.MAX_ENEMIES; i++) {
				float xpos = 3.0f + (float)i * (GetEndPosition () - 3.0f) / GameModel.MAX_ENEMIES;
				float ypos = getFloorHeight (xpos - 0.05f);
				ret [i] = new Vector2 (xpos , ypos);
			}

			return ret;
		}
}


}

