using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LectureExample
{
	public class IMGUI
	{
		LineDrawer lines;

		Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch;

		ButtonState oldState;

		public IMGUI (Microsoft.Xna.Framework.Content.ContentManager content, GraphicsDevice device)
		{
			lines = new LineDrawer (device);
			spriteBatch = new SpriteBatch(device);
		}

		public void startUI() {
			spriteBatch.Begin ();
		}

		public void drawUI() {
			spriteBatch.End ();
		}

		public bool doButton (Rectangle screenCoords, Keys fastKey)
		{


			float left = screenCoords.Left;
			float right = screenCoords.Right;
			float top = screenCoords.Top;
			float bottom = screenCoords.Bottom;
			//Logik
			Color color = Color.White;

			float mouseX = Mouse.GetState ().X;
			float mouseY = Mouse.GetState ().Y;
			bool mouseOver = false;
			bool mouseClicked = false;
			if (mouseX >= left && mouseX <= right && mouseY >= top && mouseY <= bottom) {
				mouseOver = true;
				color = Color.Gold;

				if (Mouse.GetState ().LeftButton == ButtonState.Pressed) {
					color = Color.Green;
					oldState = Mouse.GetState ().LeftButton;
				}
				if (Mouse.GetState ().LeftButton == ButtonState.Released && oldState == ButtonState.Pressed) {
					mouseClicked = true;
					color = Color.Blue;
				}
			}






			Vector2[] corners = new Vector2[5];

			corners [0] = new Vector2 (left, top);
			corners [1] = new Vector2 (right, top);
			corners [2] = new Vector2 (right, bottom);
			corners [3] = new Vector2 (left, bottom);
			corners [4] = new Vector2 (left, top);

			Vector2 start = corners[0];
			for (int i= 1; i < 5; i++) {

				Vector2 end = corners [i];
				lines.DrawLine (spriteBatch, start, end, color);
				start = end;
			}
				//throw new NotImplementedException ();
			if (Keyboard.GetState ().IsKeyDown (fastKey)) {
				return true;
			}
			return mouseClicked && mouseOver;
		}
	}
}

