#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

#endregion

namespace LectureExample
{
	class CollisionParticleSystem
{
		Vector2[] particlePositions = new Vector2[10];
		public void doCollision (Vector2 centerModelPosition)
		{
			//skapa particlar
			Random r = new Random ();
			for (int i= 0; i< 10; i++) {
				float x = 2.0f * (float)r.NextDouble () - 1;
				float y = 2.0f * (float)r.NextDouble () - 1;
				Vector2 rand=  new Vector2 (x, y);
				particlePositions[i] = centerModelPosition + rand * 0.1f;
			}
		}

		public void draw (Camera camera, SpriteBatch spriteBatch, Texture2D particles)
		{
			//rita ut partiklar
			for (int i = 0 ; i< 10; i++) {
				if (particlePositions [i] != null) {
					Vector2 screenPos = camera.getViewFromModelPosition (particlePositions [i]);


					spriteBatch.Draw (particles, 
					                  screenPos, 
					                  particles.Bounds, 
					                  Color.White, 3.14f/2.0f, 
					                  new Vector2 (particles.Bounds.Width / 2, particles.Bounds.Height / 2), 
					                  0.1f, SpriteEffects.None, 0);
				}
			}
		}

		public void  update(float elapsedTime) {


		}
}


}

