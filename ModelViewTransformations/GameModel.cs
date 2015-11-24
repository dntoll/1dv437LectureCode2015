#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace ModelViewTransformations.model
{


	public class GameModel
	{
		public class Shuttle {
			public void update (float elapsedTime)
			{
				shuttleCenterPosition.X += speed * elapsedTime;
			}

			public void down (float elapsedTime)
			{
				shuttleCenterPosition.Y += speed * elapsedTime;
			}

			public void up (float elapsedTime)
			{
				shuttleCenterPosition.Y -= speed * elapsedTime;
			}

			public float radius = 0.10f;
			public float speed = 0.5f;
			public Vector2 shuttleCenterPosition = new Vector2(2-0.1f, 0.5f);

		}

		/**
		 * A map consists of a roof and a floor and a width
		 */
		public class Map {

			public float getRoofHeight(float x) {
				Random r = new Random ((int)(x * 100.0f));

				return (float)Math.Sin(x*3-0.2f ) / 5 + (float)r.NextDouble() * 0.1f + 0.25f;
			}

			public float getFloorHeight(float x) {
				return getRoofHeight (x) + 0.5f;
			}

			public float getLevelWidth () {
				return 10.0f;
			}

			public float getLevelHeight () {
				return 1.0f;
			}

			public bool playerReachedEnd (Shuttle player)
			{
				if (player.shuttleCenterPosition.X >= getLevelWidth () - player.radius) {
					return true;
				}
				return false;
			}

			public bool playerCollidesRoof (Shuttle player)
			{
				float roofHeight = getRoofHeight (player.shuttleCenterPosition.X);

				if (player.shuttleCenterPosition.Y-player.radius < roofHeight) {
					return true;
				}
				return false;
			}

			public bool playerCollidesFloor (Shuttle player)
			{
				float roofHeight = getFloorHeight (player.shuttleCenterPosition.X);

				if (player.shuttleCenterPosition.Y+player.radius > roofHeight) {
					return true;
				}
				return false;
			}

			public Vector2 getPlayerStartPosition (Shuttle player)
			{
				return new Vector2 (player.radius, getLevelHeight () / 2.0f);
			}
		}

		public Shuttle player = new Shuttle();
		public Map map = new Map();

		public GameModel() {
			player.shuttleCenterPosition = map.getPlayerStartPosition (player);
		}

		public void update(float elapsedTime, IModelListener listener) {
			player.update (elapsedTime);

			if (map.playerReachedEnd (player) == true) {
				player.shuttleCenterPosition = map.getPlayerStartPosition (player);
			}
			if (map.playerCollidesRoof (player) || map.playerCollidesFloor(player) ) {
				player.shuttleCenterPosition = map.getPlayerStartPosition (player);
				listener.hitWall ();
			}

		}

		public void playerUp (float elapsedTime)
		{
			player.up (elapsedTime);
		}

		public void playerDown (float elapsedTime)
		{
			player.down (elapsedTime);
		}

	}

}

