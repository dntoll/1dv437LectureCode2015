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
	public class GameView : IModelObserver
{
		SpriteBatch spriteBatch;		
		Texture2D shuttleTexture;

		GameModel model;
		Camera camera;
		LineDrawer lines;

		CollisionParticleSystem particles = new CollisionParticleSystem();

		public GameView (GameModel model, ContentManager content, GraphicsDevice device)
		{
			this.model = model;

			shuttleTexture = content.Load<Texture2D> ("shuttle.png");

			spriteBatch = new SpriteBatch(device);

			camera = new Camera (device.Viewport);

			lines = new LineDrawer (device);

		}

		public void DrawGame(GraphicsDevice device, float timeElapsed) {

			camera.centerOn (model.player.centerModelPosition);
			Vector2 shuttleCenterWindowPosition = camera.getViewFromModelPosition (model.player.centerModelPosition);

			//Vector2 textureDisplacement = new Vector2 (shuttleTexture.Bounds.Width / 2, shuttleTexture.Bounds.Height / 2);


			float shuttleScale = camera.getTextureScale(model.player.modelRadius, shuttleTexture.Bounds);

			spriteBatch.Begin ();
			//spriteBatch.Draw (shuttleTexture, shuttleCenterWindowPosition - textureDisplacement, Color.White);
			spriteBatch.Draw (shuttleTexture, 
			                  shuttleCenterWindowPosition, 
			                  shuttleTexture.Bounds, 
			                  Color.White, 3.14f/2.0f, 
			                  new Vector2 (shuttleTexture.Bounds.Width / 2, shuttleTexture.Bounds.Height / 2), 
			                  shuttleScale, SpriteEffects.None, 0);


			for (int i = 0; i < GameModel.MAX_ENEMIES; i++) {
				Vector2 enemyCenterWindowPosition = camera.getViewFromModelPosition (model.enemies[i].modelCenterPosition);
				spriteBatch.Draw (shuttleTexture, 
				                  enemyCenterWindowPosition, 
				                  shuttleTexture.Bounds, 
				                  Color.White, 3.14f/2.0f, 
				                  new Vector2 (shuttleTexture.Bounds.Width / 2, shuttleTexture.Bounds.Height / 2), 
				                  shuttleScale, SpriteEffects.None, 0);
			}

			for (int i = 0; i < model.player.hitPoints; i++) {
				spriteBatch.Draw (shuttleTexture, new Vector2 (30 + 30 * i, 30), Color.Wheat);
			}
		
			//spriteBatch.Draw (shuttle, viewShuttlePosition, shuttle.Bounds, Color.White, 3.14f / 2.0f, new Vector2 (shuttle.Bounds.Width / 2, shuttle.Bounds.Height / 2), viewScale, SpriteEffects.None, 0);

			Vector2 modelStart = new Vector2 (0, model.map.getRoofHeight (0));
			Vector2 lineStart = camera.getViewFromModelPosition(modelStart);
			//Draw roof
			for (int i = 1; i <= 100; i++) {
				float modelX = (float)i/10.0f;
				Vector2 modelEnd = new Vector2 (modelX, model.map.getRoofHeight (modelX));
				Vector2 lineEnd = camera.getViewFromModelPosition(modelEnd);
				lines.DrawLine (spriteBatch, lineStart, lineEnd, Color.White);
				lineStart = lineEnd;
			}

			modelStart = new Vector2 (0, model.map.getFloorHeight(0));
			lineStart = camera.getViewFromModelPosition(modelStart);
			//Draw roof
			for (int i = 1; i <= 100; i++) {
				float modelX = (float)i/10.0f;
				Vector2 modelEnd = new Vector2 (modelX, model.map.getFloorHeight (modelX));
				Vector2 lineEnd = camera.getViewFromModelPosition(modelEnd);
				lines.DrawLine (spriteBatch, lineStart, lineEnd, Color.White);
				lineStart = lineEnd;
			}
			particles.update (timeElapsed);
			particles.draw (camera, spriteBatch, shuttleTexture);

			spriteBatch.End ();
		}

		#region IModelObserver implementation

		public void Collision ()
		{
			particles.doCollision (model.player.centerModelPosition);
		}

		#endregion
}

}

