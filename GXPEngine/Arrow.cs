using System;

namespace GXPEngine
{
	public class Arrow : GameObject
	{
		public Vector2 startPoint;
		public Vector2 vector;

		public float scaleFactor;

		public uint color = 0xffffffff;
		public uint lineWidth = 1;

		public Arrow(Vector2 pStartPoint, Vector2 pVector, float pScale, uint pColor = 0xffffffff, uint pLineWidth = 1)
		{
			startPoint = pStartPoint;
			vector = pVector;
			scaleFactor = pScale;

			color = pColor;
			lineWidth = pLineWidth;
		}

		protected override void RenderSelf(GXPEngine.Core.GLContext glContext)
		{
			Vector2 endPoint = startPoint + vector * scaleFactor;
			Gizmos.RenderLine(startPoint.x, startPoint.y, endPoint.x, endPoint.y, color, lineWidth, true);

			Vector2 smallVec = vector.Normalized() * -10; // constant length 10, opposite direction of vector
			Vector2 left = new Vector2(-smallVec.y, smallVec.x) + smallVec + endPoint;
			Vector2 right = new Vector2(smallVec.y, -smallVec.x) + smallVec + endPoint;

			Gizmos.RenderLine(endPoint.x, endPoint.y, left.x, left.y, color, lineWidth, true);
			Gizmos.RenderLine(endPoint.x, endPoint.y, right.x, right.y, color, lineWidth, true);
		}
	}
}
