#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace LectureExample
{
	public class Enemy
	{
		public Vector2 modelCenterPosition;
		public Enemy (Vector2 vector2)
		{
			modelCenterPosition = vector2;
		}
}


}

