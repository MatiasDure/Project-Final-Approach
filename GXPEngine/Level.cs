using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Level
{
    public List<Ball> balls;
    public List<NLineSegment> lines;
    Vector2 gravity;

    public Level()
    {
        balls = new List<Ball>();
        lines = new List<NLineSegment>();
    }

    void Update()
    {
    }

    public int BallCount() => balls.Count;
    public int LineCount() => lines.Count;
    public Ball BallAtIndex(int pIndex) => balls[pIndex];
    public NLineSegment LineAtIndex(int pIndex) => lines[pIndex];
}