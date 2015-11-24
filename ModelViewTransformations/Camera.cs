using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ModelViewTransformations.view
{
	class Camera
	{
		float scale = 1024;
		float border = 10;
		float dispX = 0;
		float dispY = 0;
		float modelWidth;
		Rectangle windowBounds;


		public Camera (Rectangle windowBounds, float modelWidth)
		{
			this.windowBounds = windowBounds;
			//Minimum bounds bestämmer skalan i spelet
			float minimumBounds = windowBounds.Width < windowBounds.Height ? windowBounds.Width : windowBounds.Height;

			scale = minimumBounds - border * 2;

			this.modelWidth = modelWidth;
		}

		public void centerOn (float modelX)
		{

			float vx = modelX * scale;
			float drawableWindowWidth = (windowBounds.Width - border * 2) ;
			dispX = vx - drawableWindowWidth/2;

			//cap
			if (dispX < 0) {
				dispX = 0;
			}
			float viewWidth = modelWidth * scale;

			if (dispX > viewWidth - drawableWindowWidth) {
				dispX = viewWidth - drawableWindowWidth;
			}

		}

		public float getScale ()
		{
			return scale;
		}



		public float modelRadiusToViewTextureScale (Rectangle textureBounds, float modelRadius)
		{
			float longestSideTexels = textureBounds.Width > textureBounds.Height ? textureBounds.Width : textureBounds.Height;

			float viewSizePixels = modelRadius * scale;

			float viewScale = viewSizePixels / longestSideTexels;

			return viewScale*2.0f; //since it was radius!!!
		}

		public Vector2 modelToView (Vector2 position)
		{
			float x = position.X * scale + border - dispX;
			float y = position.Y * scale + border - dispY;
			return new Vector2 (x, y);
		}
	}
}

