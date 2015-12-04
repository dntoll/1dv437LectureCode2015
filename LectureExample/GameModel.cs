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
		public static int MAX_ENEMIES = 10;
		public Enemy[] enemies= new Enemy[10]; 

		public GameModel() {
			RestartGame ();
		}

		public void Update (float seconds, IModelObserver observer)
		{
			if (IsGameOver () == false) {
				player.Update (seconds);
			}

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

			for (int  i = 0; i < MAX_ENEMIES; i++) {
				if ((player.centerModelPosition - enemies [i].modelCenterPosition).Length () < player.modelRadius + enemies[i].modelRadius) {
					player.takeDamage ();
					observer.Collision ();
				}
			}

		}



		public void RestartGame() {
			player = new Shuttle();
			map = new Map ();

			Vector2[] startpos = map.GetEnemystartPositions ();

			for (int i = 0; i < MAX_ENEMIES; i++) {
				enemies [i] = new Enemy (startpos [i]);
			}
		}

		public bool HasWon() {
			return player.centerModelPosition.X >= map.GetEndPosition ();
		}

		public bool IsGameOver() {
			return player.hitPoints <= 0;
		}
	}

}

