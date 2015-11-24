#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace LectureExample
{
	public class Map
{
		public float getRoofHeight(float modelX) {
			return ((float)Math.Sin(modelX * 4.0f) + 1.0f) * 0.25f;
		}

		public float getFloorHeight(float modelX) {
			return getRoofHeight (modelX) + 0.5f;
		}
}


}

