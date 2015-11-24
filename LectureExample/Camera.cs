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
	public class Camera
{
		int scale;
		float displacement = 0;
		float halfWindowWidth = 0;

		public Camera (Viewport viewport)
		{
			scale = viewport.Height;
			halfWindowWidth = viewport.Width / 2;

		}

		public float getTextureScale (float modelRadius, Rectangle textureBounds)
		{
			//2.0 is since we have a radius...
			return 2.0f * modelRadius * (float)scale / (float)textureBounds.Height;



		}

		public Vector2 getViewFromModelPosition (Vector2 modelCenterPos)
		{
			Vector2 viewPosition = modelCenterPos * scale;
			Vector2 windowPosition = viewPosition + new Vector2(displacement, 0);


			return windowPosition;
		}

		public void centerOn (Vector2 centerOnModelPosition)
		{
			float centerViewPosition = centerOnModelPosition.X * scale;
			displacement = -(centerViewPosition - halfWindowWidth);
		}


}


}

