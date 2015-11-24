using System;
using Microsoft.Xna.Framework;

namespace LectureExample
{
	public class Shuttle
	{
		public Vector2 centerModelPosition = new Vector2 (0.0f, 0.5f);
		public float modelRadius = 0.10f;

		float speed = 1.0f;

		bool playerWantsToGoDown = false;
		bool playerWantsToGoUp = false;

		public int hitPoints = 3;
		public float invinsibleTimer = 0;

		public Shuttle ()
		{
		}

		public void Update(float timeElapsedSeconds) {
			centerModelPosition.X = centerModelPosition.X + speed * timeElapsedSeconds;

			if (playerWantsToGoDown) {
				centerModelPosition.Y = centerModelPosition.Y + speed * timeElapsedSeconds;

			}
			if (playerWantsToGoUp) {
				centerModelPosition.Y = centerModelPosition.Y - speed * timeElapsedSeconds;
			}

			if (centerModelPosition.Y > 1.0) {
				centerModelPosition.Y = 1.0f;
			}

			if (centerModelPosition.Y < 0.0) {
				centerModelPosition.Y = 0.0f;
			}

			invinsibleTimer -= timeElapsedSeconds;

			playerWantsToGoDown = false;
			playerWantsToGoUp = false;
		}

		public void down ()
		{
			playerWantsToGoDown = true;
		}

		public void up ()
		{
			playerWantsToGoUp = true;
		}

		public void takeDamage ()
		{
			if (invinsibleTimer < 0) {
				hitPoints--;
				invinsibleTimer = 1.0f;
			}

		}
	}
}

