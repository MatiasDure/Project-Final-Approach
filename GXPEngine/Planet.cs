using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class Planet:Ball
{
    Vector2 acceleration;
    Vector2 velocity;
    Vector2 oldPosition;
    Vector2 gravity; 
    Vector2 desVelocity = new Vector2(0,0);
    Vector2 beltAcceleration;
    Vector2 ufoPosition; 

    public NotMarble[] notMarbles;
    public ConveyorBelt[] belts;

    bool _started = false;
    bool _pull = false;
    bool _lost = false;
    bool _win = false;
    public bool riding = false;
    bool ridingSound = false;
    bool stopping = false;
    public bool teleporting = false;
    bool ufoSucked;
    bool check2 = false;
    bool rolling; 
    List<String> images;
    Ship ufo; 

    float _width, _height;

    int timesLost = 10;
    int timesWon = 150;

    public Vector2 Gravity { get => gravity; }
    public float Width { get => _width; }
    public float Height { get => _height; }
    public Vector2 Velocity { get => velocity; }
    public bool Lost { get => _lost; }
    public bool Win { get => _win; }
    public bool Pull { get => _pull; }
    public bool Started { get => _started; }
    Sound[] sounds;
    //SoundChannel ufoSound;
    bool ufoPlay = false;

    public Planet(TiledObject obj = null):base("planet1.png", 1, 1, true)
    {
        images = new List<String>();
        images.Add("planet1.png");
        images.Add("planet2.png");
        images.Add("planet3.png");
        images.Add("planet4.png");
        String random = images[Utils.Random(0, 4)];

        sounds = new Sound[] { new Sound("sounds/ballbounce1.wav", false,true), 
            new Sound("sounds/ballbounce2.wav", false, true), 
            new Sound("sounds/ballbounce3.wav", false, true),
            new Sound("sounds/belt.wav", false, true),
            new Sound("sounds/teleport.wav",false,true),
            new Sound("sounds/UFO_pickup_v2.wav",false,true)};

        //ufoSound = sounds[5].Play();
        //ufoSound.IsPaused = true;



        acceleration = new Vector2(0, 0);
        velocity = new Vector2(0, 0);
        Initialize(obj);
        base._texture = new Sprite(random).texture;
    }

    protected override void Initialize(TiledObject obj = null)
    {
        base.Initialize(obj);
        _width = obj.Width;
        _height = obj.Height;
    }

    public override void Step()
    {
        oldPosition = Position;
        if(!_pull && !ufoSucked) GravityChange();
        CollisionWithBelt();
        velocity += acceleration;
        bool firstTime = true;

        for (int i = 0; i < 2; i++)
        {
            _position += velocity;
            CollisionInfo firstCollision = FindEarliestCollision();
            if (firstCollision != null)
            {
                rolling = firstCollision.timeOfImpact < 0.41f;
                ResolveCollision(firstCollision);
                if (firstTime && Approximate(firstCollision.timeOfImpact)) //rolling
                {
                    continue;
                }
            }
            break;
        }

        if (!riding) velocity = velocity * 0.99f + desVelocity * 0.01f;
        else
        {
            velocity = velocity * 0.95f;
        }
        Ufo();

        UpdateScreenPosition();
    }

    bool Approximate(float pA, float margin = 0.001f) => pA <= margin;

    void UpdateScreenPosition()
    {
        x = _position.x;
        y = _position.y;
    }

    void GravityChange()
    {
        ResetAcceleration();
        
        if (Input.GetKey(Key.UP)) acceleration += new Vector2(0, -.1f);
        if (Input.GetKey(Key.DOWN)) acceleration += new Vector2(0, .1f);
        if (Input.GetKey(Key.LEFT)) acceleration += new Vector2(-.1f, 0);
        if (Input.GetKey(Key.RIGHT)) acceleration += new Vector2(.1f, 0);
        gravity = acceleration;
        if (riding && !stopping) acceleration += beltAcceleration;
        if(!Started && (acceleration.x > 0 || acceleration.y > 0)) _started = true;
        acceleration.LimitLength(0.1f); 
    }
    
    void OnCollision(GameObject pOther)
    {
        if (pOther is Door d)
        {
            if (!d.disableCollider)
            {
                _position = oldPosition + velocity * pOther.collider.GetCollisionInfo(pOther.collider).timeOfImpact;
                velocity.Reflect(bounciness,pOther.collider.GetCollisionInfo(pOther.collider).normal);
            }
        }

        if (pOther is Ship s)
        {
            ufo = s;
            if(!s.used) ufoSucked = true;

        }

    }
    void UfoSuck()
    {     
        if (ufoSucked)
        {
            if (!ufoPlay) sounds[5].Play();
            ufoPosition += new Vector2(0, 32);
            Vector2 desPosition = ufoPosition;
            Vector2 diff = desPosition - _position;
            diff.SetLength(0.01f);
            _position += diff;
            if (diff.Length() < 0.1f || check2)
            {
                check2 = true;
                desPosition += new Vector2(6, -53);
                if (height > 10)
                {
                    width--;
                    height--;
                }
                else timesWon--;
                _win = timesWon < 1; 
            }
        }
    }

    void Ufo()
    {
        //if (ufoSucked && !ufo.used) //ufoSound.IsPaused = false;
        if (ufoSucked && !ufo.used) 
        {
            if (!ufoPlay)
            {
                sounds[5].Play();
                ufoPlay = true;
            }
            ufo.SetCycle(5, 13);
            if(ufo.currentFrame > 15) ufo.SetCycle(16, 1);

            Vector2 desPosition = ufo.Position + new Vector2(0, 20);
            if ((desPosition - _position).Length() < 0.1f || check2)
            {
                check2 = true; 
                desPosition += new Vector2(6, -42);
                if (height > 10)
                {
                    width--;
                    height--;
                }
                if (ufo.currentFrame > 15 && ufo != null)
                {
                    _win = true;
                    ufo.used = true;
                }
            }
            Vector2 diff = desPosition - _position ;
            diff.LimitLength(1f);
            _position += diff;
            acceleration *= 0;
            velocity *= 0;
        }
    }

    public void SuckedIn(Vector2 pDifference, GameObject pOther)
    {
        if(!_pull) _pull = true;
        
        Vector2 unitDifference = pDifference.Normalized();
        acceleration = unitDifference * velocity.Length() * 0.5f;
        desVelocity = unitDifference;

        if (pDifference.Length() < 5 && !Lost) timesLost--;
        if (timesLost < 0) _lost = true;
    }

    public void RidingConveyorBelt(Vector2 pDirection)
    {
        beltAcceleration = pDirection;
    }

    void CollisionWithBelt()
    {
        int amountNotColliding = 0;

        foreach (ConveyorBelt belt in belts)
        {
            float distance = Position.DistanceBetween(belt.Position);
            float differenceFromCenter = Mathf.Abs(this.Width/2 + belt.Width/2);

            if (distance < differenceFromCenter)
            {
                RidingConveyorBelt(belt.Movement);
            }
            else
            {
                amountNotColliding++;
            }
                
        }
        riding = !(amountNotColliding == belts.Length);
    }

    public void Teleport(Vector2 pPos)
    {
        _position = pPos;
        sounds[4].Play();
    }

    void ResetAcceleration()
    {
        acceleration = new Vector2(0, 0);
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

            if (toi < 0 || toi > 1) 
            {
                return pCurrentToi; //time of impact is outside the possible scope
            }
            if (pCurrentToi > toi) return toi;
            return pCurrentToi;
        }
    }

    CollisionInfo FindEarliestCollision()
    {
        Level myLevel = ((MyGame)game).level;

        float smallestToi = 100;
        float currentToi = smallestToi;
        bool collisionDetected = false;
        Vector2 firstColNormal = new Vector2();

        for (int i = 0; i < myLevel.BallCount(); i++)
        {
            Ball other = myLevel.BallAtIndex(i);
            if (other == this) continue;
            smallestToi = ToiBall(other, currentToi);

            if (smallestToi != currentToi)
            {
                collisionDetected = true;
                firstColNormal = (this.oldPosition + smallestToi * this.velocity) - other.Position; // Point of impact - mover.position
                firstColNormal.Normalize();
                currentToi = smallestToi;
            }
        }

            for (int i = 0; i < myLevel.CapsCount(); i++)
            {
                Ball other = myLevel.CapsAtIndex(i);
                if (other == this) continue;
                smallestToi = ToiPoint(other, currentToi);

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
                smallestToi = this.ToiLine(currentLine, currentToi);

                if (smallestToi != currentToi)
                {
                    collisionDetected = true;
                    firstColNormal = currentLine._normal.vector;
                    //Console.WriteLine(firstColNormal);
                    currentToi = smallestToi;
                }
            }

        if (collisionDetected) return new CollisionInfo(firstColNormal, null, smallestToi);
        return null;
    }

    void ResolveCollision(CollisionInfo pCollision)
    {
        Vector2 desiredPos = oldPosition + pCollision.timeOfImpact * velocity;
        _position.SetXY(desiredPos);
        velocity.Reflect(Ball.bounciness, pCollision.normal);
        Console.WriteLine("ROlong " + rolling );
        if(!rolling)sounds[Utils.Random(0,3)].Play();
    }

    float ToiPoint(Ball pOther, float pCurrentToi)
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
        Vector2 diffVecBetweenEndPoint = this._position - pOther.end;

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
