using System;
using GXPEngine.Core;
using GXPEngine.OpenGL;

namespace GXPEngine
{
	/// <summary>
	/// Implements an OpenGL line
	/// </summary>
	public class NLineSegment : GameObject
	{
		public Vector2 start;
		public Vector2 end;

		public uint color = 0xffffffff;
		public uint lineWidth = 1;

		public Arrow _normal;
		public NLineSegment(float pStartX, float pStartY, float pEndX, float pEndY, uint pColor = 0xffffffff, uint pLineWidth = 1)
			: this(new Vector2(pStartX, pStartY), new Vector2(pEndX, pEndY), pColor, pLineWidth)
		{
		}

		public NLineSegment(Vector2 pStart, Vector2 pEnd, uint pColor = 0xffffffff, uint pLineWidth = 1)
		{
			start = pStart;
			end = pEnd;
			color = pColor;
			lineWidth = pLineWidth;
			_normal = new Arrow(new Vector2(0, 0), new Vector2(0, 0), 40, 0xffff0000, 1);
			AddChild(_normal);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														RenderSelf()
		//------------------------------------------------------------------------------------------------------------------------
		override protected void RenderSelf(GLContext glContext)
		{
			if (game != null)
			{
				recalculateArrowPosition();
				Gizmos.RenderLine(start.x, start.y, end.x, end.y, color, lineWidth);
			}
		}
		private void recalculateArrowPosition()
		{
			_normal.startPoint = (start + end) * 0.5f;
			_normal.vector = (end - start).Normal();
		}
	}
}

