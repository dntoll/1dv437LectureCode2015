using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace ModelViewTransformations.view
{
	public class GameView : IModelListener 
	{

		Texture2D shuttle;
		LineDrawer renderer;
		SpriteBatch spriteBatch;
		model.GameModel model;
		Camera camera;

		SoundEffect effect;

		public GameView (GraphicsDevice gd, ContentManager c, model.GameModel model)
		{
			renderer = new LineDrawer (gd);
			shuttle = c.Load<Texture2D> ("shuttle.png");
			spriteBatch = new SpriteBatch(gd);
			this.model = model;
			camera = new Camera (gd.Viewport.Bounds, model.map.getLevelWidth());

			effect = c.Load<SoundEffect> ("fire.wav");
		}

		void DrawMap ()
		{
			Vector2 oldRoofPosition = camera.modelToView (new Vector2 (0, model.map.getRoofHeight (0)));
			Vector2 oldFloorPosition = camera.modelToView (new Vector2 (0, model.map.getFloorHeight (0)));
			for (float modelX = 0; modelX <= model.map.getLevelWidth(); modelX += 1.0f / camera.getScale()) {
				float roofX = model.map.getRoofHeight (modelX);
				float floorY = model.map.getFloorHeight (modelX);
				Vector2 roofViewPosition = camera.modelToView (new Vector2 (modelX, roofX));
				Vector2 floorViewPosition = camera.modelToView (new Vector2 (modelX, floorY));
				renderer.DrawLine (spriteBatch, oldRoofPosition, roofViewPosition, Color.White);
				renderer.DrawLine (spriteBatch, oldFloorPosition, floorViewPosition, Color.White);
				oldRoofPosition = roofViewPosition;
				oldFloorPosition = floorViewPosition;
			}
		}

		void DrawFrame ()
		{

			Vector2 topLeft = camera.modelToView (new Vector2 (0, 0));
			Vector2 bottomRight = camera.modelToView (new Vector2 (model.map.getLevelWidth(), model.map.getLevelHeight()));
			Vector2 bottomLeft = camera.modelToView (new Vector2 (0, model.map.getLevelHeight()));
			Vector2 topRight = camera.modelToView (new Vector2 (model.map.getLevelWidth(), 0));

			renderer.DrawLine (spriteBatch, topLeft, topRight, Color.Red);
			renderer.DrawLine (spriteBatch, topLeft, bottomLeft, Color.Red);
			renderer.DrawLine (spriteBatch, bottomLeft, bottomRight, Color.Red);
			renderer.DrawLine (spriteBatch, bottomRight, topRight, Color.Red);
		}

		void DrawShuttle ()
		{
			Vector2 viewShuttlePosition = camera.modelToView (model.player.shuttleCenterPosition);
			float viewScale = camera.modelRadiusToViewTextureScale (shuttle.Bounds, model.player.radius);
			spriteBatch.Draw (shuttle, viewShuttlePosition, shuttle.Bounds, Color.White, 3.14f / 2.0f, new Vector2 (shuttle.Bounds.Width / 2, shuttle.Bounds.Height / 2), viewScale, SpriteEffects.None, 0);

			Vector2 start = camera.modelToView (new Vector2 (model.player.shuttleCenterPosition.X+model.player.radius, model.player.shuttleCenterPosition.Y));
			for (int i = 0; i <= 360; i++) {
				double angle = (double)i / (2.0f * Math.PI);
				float dx = (float)Math.Cos (angle) * model.player.radius;
				float dy = (float)Math.Sin (angle) * model.player.radius;

				Vector2 at = camera.modelToView(new Vector2 (dx + model.player.shuttleCenterPosition.X, dy + model.player.shuttleCenterPosition.Y));

				renderer.DrawLine (spriteBatch, start, at, Color.Green);
				start = at;
			}


		}

		public void Draw() {

			camera.centerOn (model.player.shuttleCenterPosition.X);
			spriteBatch.Begin ();

			DrawShuttle ();


			DrawFrame ();
			DrawMap ();
			spriteBatch.End ();
		}


		#region IModelListener implementation
		public void hitWall ()
		{
			effect.Play(0.1f,1,1);
		}
		#endregion
	}
}

