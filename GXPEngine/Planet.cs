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

        for (int i = 0; i < myLevel.GetPointsCount(); i++)
        {
            Ball other = myLevel.GetPointAtIndex(i);
            smallestToi = ToiBall(other, currentToi);

            if (smallestToi != currentToi)
            {
                collisionDetected = true;
                firstColNormal = (this._oldPosition + smallestToi * this._velocity) - other._position; // Point of impact - mover.position
                firstColNormal.Normalize();
                currentToi = smallestToi;
            }
        }

        for (int i = 0; i < myLevel.GetLinesCount(); i++)
        {
            NLineSegment currentLine = myLevel.GetLineAtIndex(i);
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

    float ToiLine()
    {
        return 0;
    }
}
