using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Level
{
    List<Ball> balls;
    List<NLineSegment> lines;

    public Level()
    {
        balls = new List<Ball>();
        lines = new List<NLineSegment>();
    }

    public int BallCount() => balls.Count;
    public int LineCount() => lines.Count;
    public Ball BallAtIndex(int pIndex) => balls[pIndex];
    public NLineSegment LineAtIndex(int pIndex) => lines[pIndex];
}