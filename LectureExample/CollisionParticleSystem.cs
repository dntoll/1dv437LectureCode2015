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
		class Particle {
			public Vector2 centerModelPosition;
			public Vector2 modelVelocity;
			public static Vector2 gravity = new Vector2(0, 9.82f);
			public Particle (Vector2 startPosition, Vector2 modelVelocity)
			{
				centerModelPosition = startPosition;
				this.modelVelocity = modelVelocity; 
			}

			public void update (float elapsedTime)
			{

				centerModelPosition = centerModelPosition + modelVelocity * elapsedTime;

				modelVelocity = modelVelocity + gravity * elapsedTime;
			}
		}
		static int numParticles = 100;
		Particle[] particles = new Particle[numParticles];

		public void doCollision (Vector2 centerModelPosition)
		{
			//skapa particlar
			Random r = new Random ();
			for (int i= 0; i< numParticles; i++) {
				float x = 2.0f * (float)r.NextDouble () - 1;
				float y = 2.0f * (float)r.NextDouble () - 1;
				Vector2 rand=  new Vector2 (x, y);
				rand.Normalize ();

				Vector2 modelVelocity = rand;
				float length = (float)r.NextDouble () * 0.1f; 


				particles[i] = new Particle(centerModelPosition + rand * length, modelVelocity);
			}
		}

		public void draw (Camera camera, SpriteBatch spriteBatch, Texture2D particleTexture)
		{
			//rita ut partiklar
			for (int i = 0 ; i< numParticles; i++) {

				if (particles [i] != null) {
					Particle toBeRendered = particles [i];
					Vector2 screenPos = camera.getViewFromModelPosition (toBeRendered.centerModelPosition);


					spriteBatch.Draw (particleTexture, 
					                 screenPos, 
					                 particleTexture.Bounds, 
					                 Color.White, 3.14f / 2.0f, 
					                 new Vector2 (particleTexture.Bounds.Width / 2, particleTexture.Bounds.Height / 2), 
					                 0.1f, SpriteEffects.None, 0);
				}
			}
		}

		public void  update(float elapsedTime) {
			for (int i = 0; i< numParticles; i++) {
				if (particles [i] != null) {
					particles [i].update (elapsedTime);
				}
			}

		}
}


}

