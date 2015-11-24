#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace LectureExample
{
	public class GameModel
	{
		public Shuttle player = new Shuttle();


		public Map map = new Map();

		public void Update (float seconds, IModelObserver observer)
		{
			player.Update (seconds);

			if (player.centerModelPosition.Y - player.modelRadius < map.getRoofHeight (player.centerModelPosition.X)) {
				player.centerModelPosition.Y = map.getRoofHeight (player.centerModelPosition.X) + player.modelRadius;
				player.takeDamage ();
				observer.Collision ();
			}
			if (player.centerModelPosition.Y + player.modelRadius > map.getFloorHeight (player.centerModelPosition.X)) {
				player.centerModelPosition.Y = map.getFloorHeight (player.centerModelPosition.X) - player.modelRadius;
				player.takeDamage ();
				observer.Collision ();
			}

			if (player.hitPoints <= 0) {
				player = new Shuttle();
				map = new Map ();
			}
		}
}

}

