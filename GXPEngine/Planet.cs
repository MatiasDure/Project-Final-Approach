using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Planet:Ball
{
    CollisionInfo firstCollision = null;

    Vector2 _velocity;
    Vector2 _oldPosition;

    public Planet(Vector2 pPos):base("circle.png", 1, 1, pPos)
    {

    }

    public void Step()
    {

    }



    CollisionInfo CheckCollision()
    {
        Level myLevel = ((MyGame)game).currentLevel;

        float smallestToi = 100;
        float currentToi = smallestToi;
        bool collisionDetected = false;
        Vector2 firstColNormal = new Vector2();

        for (int i = 0; i < myLevel.BallCount(); i++)
        {
            Ball other = myLevel.BallAtIndex(i);
            smallestToi = ToiBall(other, currentToi);

            if (smallestToi != currentToi)
            {
                collisionDetected = true;
                firstColNormal = (this._oldPosition + smallestToi * this._velocity) - other.Position; // Point of impact - mover.position
                firstColNormal.Normalize();
                currentToi = smallestToi;
            }
        }

        for (int i = 0; i < myLevel.LineCount(); i++)
        {
            NLineSegment currentLine = myLevel.LineAtIndex(i);
            smallestToi = ToiLine(currentLine, currentToi);

            if (smallestToi != currentToi)
            {
                collisionDetected = true;
                firstColNormal = currentLine._normal.vector;
                Console.WriteLine(firstColNormal);
                currentToi = smallestToi;
            }
        }

        if (collisionDetected) return new CollisionInfo(firstColNormal, null, smallestToi);
        return null;
    }

    void ResolveCollision(CollisionInfo pCollision)
    {
        Vector2 desiredPos = _oldPosition + pCollision.timeOfImpact * _velocity;
        Position.SetXY(desiredPos);
        _velocity.Reflect(Ball.bounciness, pCollision.normal);
        //_velocity *= 0.995f; //friction
        _velocity.Reflect(-0.995f, pCollision.normal.Normal()); // funky but correct friction!

    }

    float ToiBall(Ball pOther, float pCurrentToi)
    {
        Vector2 oldRelativePos = this._oldPosition - pOther.Position;

        float distance = oldRelativePos.Length();
        float velocityLength = this._velocity.Length();
        float sumRadius = this.Radius + pOther.Radius;
        float a = velocityLength * velocityLength;
        float b = 2 * oldRelativePos.Dot(this._velocity);
        float c = distance * distance - sumRadius * sumRadius;
        float insideSqrt = b * b - 4 * a * c;

        // returns null because a negative number inside the sqrt would give no solution or the velocity is 0 (ball is not moving) 
        if (insideSqrt < 0 || a == 0) return pCurrentToi;

        if (c < 0)
        {
            if (b < 0) return 0;
            else return pCurrentToi;
        }
        else
        {
            float toi;
            float sqrtResult = Mathf.Sqrt(insideSqrt);

            toi = (-b - sqrtResult) / (2 * a);

            if (toi < 0 || toi > 1) return pCurrentToi; //time of impact its outside the possible scope
            if (pCurrentToi > toi) return toi;
            return pCurrentToi;
        }
    }

    float ToiLine(NLineSegment pOther, float pCurrentToi)
    {
        Vector2 lineVector = pOther.start - pOther.end;
        Vector2 diffVecBetweenEndPoint = this.Position - pOther.end;

        float lineVectorLength = lineVector.Length();
        float scalarProjection = diffVecBetweenEndPoint.ScalarProjection(lineVector);

        //returns the currentToi if the line is not between the line segment
        if (scalarProjection > lineVectorLength || scalarProjection < 0) return pCurrentToi;

        Vector2 vectorProjection = Vector2.VectorProjection(diffVecBetweenEndPoint, pOther._normal.vector);

        if (vectorProjection.Length() < this.Radius) //if ball collides with lineSegment
        {
            float toi;
            Vector2 lineNormal = pOther._normal.vector; //(currentLine.end - currentLine.start).Normal();
            Vector2 oldDiffVector = this._oldPosition - pOther.end;
            float a = Vector2.Dot(lineNormal, oldDiffVector) - this.Radius;
            float b = -Vector2.Dot(lineNormal, _velocity);
            toi = a / b;
            return toi < pCurrentToi ? toi : pCurrentToi;
        }
        return pCurrentToi;
    }
}
