using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Obstacle: Sprite
{
    Vector2 _position;
    Vector2 _oPosition; 
    public List<NLineSegment> _lines;
    List<Caps> _caps;
    float _width;
    float _height;
    public Obstacle(TiledObject obj = null) : base("empty.png" , false, false)
    {

        _caps = new List<Caps>();
        _lines = new List<NLineSegment>();


        if (obj != null)
        {
            _oPosition = new Vector2(obj.X , obj.Y);
            rotation = obj.Rotation;
            _width = obj.Width;
            _height = obj.Height;
        }
        AddCollision();
    }
    

    void AddCollision()
    {

        Vector2 firstPoint = _oPosition;
        Vector2 secondPoint = firstPoint + Vector2.GetUnitVectorDeg(rotation) * _width;
        Vector2 thirdPoint = secondPoint + Vector2.GetUnitVectorDeg(rotation + 90) * _height;
        Vector2 fourthPoint = thirdPoint + Vector2.GetUnitVectorDeg(rotation + 180) * _width;


        PolygonMaker(new Vector2[] { fourthPoint, thirdPoint, secondPoint, firstPoint });
    
    }
    void PolygonMaker(Vector2[] polyGon)
    {
        if (polyGon.Length < 2)
        {
            Console.WriteLine("Polygon with less than 2 points can't be made!");
            return;
        }

        for (int i = 1; i < polyGon.Length; i++)
        {
            AddLine(polyGon[i - 1], polyGon[i]);

        }
        AddLine(polyGon[polyGon.Length - 1], polyGon[0]);
        AddLineCaps(polyGon, 10);
    }
    void AddLine(Vector2 start, Vector2 end)
    {
        Level myLevel = ((MyGame)game).level;
        NLineSegment line = new NLineSegment(start, end, 0xff00ff00, 4);
        myLevel.AddChild(line);
        myLevel.lines.Add(line);
        //_lines.Add(line);
    }

    void AddPolygon(Vector2[] pPoints, int pRadius = 0, bool pCloseShape = true)
    {
        if (pPoints.Length < 2) return;
        for (int i = pPoints.Length - 1; i > 0; i--) //going through array in reverse to avoid index out of bound exception
        {
            NLineSegment line = new NLineSegment(pPoints[i - 1], pPoints[i], 0xff00ff00, 4);
            AddChild(line);
            _lines.Add(line);
        }
        if (pCloseShape)
        {
            NLineSegment closeLine = new NLineSegment(pPoints[pPoints.Length - 1], pPoints[0], 0xff00ff00, 4); //close opening by creating a line between first and last points
            AddChild(closeLine);
            _lines.Add(closeLine);
        }
        AddLineCaps(pPoints, pRadius);
    }

    
    void AddLineCaps(Vector2[] pPoints, int pRadius = 0)
    {
        Level myLevel = ((MyGame)game).level;
        for (int i = 0; i < pPoints.Length; i++)
        {
            Caps lineCap = new Caps(pPoints.ElementAt(i) , 1);
            myLevel.AddChild(lineCap);
            myLevel.caps.Add(lineCap);
        }
    }

    //void AddLine(Vector2 pStart, Vector2 pEnd, int pRadius = 0, bool CloseShape = true) => AddPolygon(new Vector2[] { pStart, pEnd }, pRadius, CloseShape);


}
