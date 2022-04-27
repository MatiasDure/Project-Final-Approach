using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Planet:Ball
{
    CollisionInfo firstCollision = null;

    Vector2 acceleration;
    Vector2 velocity;
    Vector2 oldPosition;
    bool pull = false;
    Vector2 desVelocity = new Vector2(0,0); 

    public Planet(TiledObject obj = null):base("circle.png", 1, 1)
    {
        acceleration = new Vector2(0, 0);
        velocity = new Vector2(0, 0);
        if(obj != null)
        {
            _position = new Vector2(obj.X, obj.Y);
            Console.WriteLine(_position);
        }
    }

    public override void Step()
    {
        if(!pull) GravityChange();

        oldPosition = Position;
        velocity += acceleration;
        _position += velocity;

        velocity = velocity * 0.99f + desVelocity * 0.01f; 

        UpdateScreenPosition();
    }

    void UpdateScreenPosition()
    {
        x = _position.x;
        y = _position.y;
    }

    void GravityChange()
    {
        acceleration = new Vector2(0, 0);
        
        if (Input.GetKey(Key.UP)) acceleration += new Vector2(0, -.05f);
        if (Input.GetKey(Key.DOWN)) acceleration += new Vector2(0, .05f);
        if (Input.GetKey(Key.LEFT)) acceleration += new Vector2(-.05f, 0);
        if (Input.GetKey(Key.RIGHT)) acceleration += new Vector2(.05f, 0);
        acceleration.LimitLength(0.05f); 
    }

    public void SuckedIn(Vector2 pDifference)
    {
        Vector2 unitDifference = pDifference.Normalized();
        pull = true;
        acceleration = unitDifference * velocity.Length() * 0.05f;
        desVelocity = unitDifference;
    }

    CollisionInfo CheckCollision()
    {
        Level myLevel = ((MyGame)game).level;

        if(myLevel == null) return null;

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
                firstColNormal = (this.oldPosition + smallestToi * this.velocity) - other.Position; // Point of impact - mover.position
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
        Vector2 desiredPos = oldPosition + pCollision.timeOfImpact * velocity;
        Position.SetXY(desiredPos);
        velocity.Reflect(Ball.bounciness, pCollision.normal);
        //_velocity *= 0.995f; //friction
        velocity.Reflect(-0.995f, pCollision.normal.Normal()); // funky but correct friction!

    }

    float ToiBall(Ball pOther, float pCurrentToi)
    {
        Vector2 oldRelativePos = this.oldPosition - pOther.Position;

        float distance = oldRelativePos.Length();
        float velocityLength = this.velocity.Length();
        float sumRadius = this.Radius + pOther.Radius;
        float a = velocityLength * velocityLength;
        float b = 2 * oldRelativePos.Dot(this.velocity);
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
            Vector2 oldDiffVector = this.oldPosition - pOther.end;
            float a = Vector2.Dot(lineNormal, oldDiffVector) - this.Radius;
            float b = -Vector2.Dot(lineNormal, velocity);
            toi = a / b;
            return toi < pCurrentToi ? toi : pCurrentToi;
        }
        return pCurrentToi;
    }
}
