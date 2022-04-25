using System;
using GXPEngine;

public struct Vector2
{
    public float x, y;

    public Vector2(float pX, float pY)
    {
        this.x = pX;
        this.y = pY;
    }

    public void SetXY(Vector2 pOther)
    {
        SetXY(pOther.x, pOther.y);
    }

    public void SetXY(float pX, float pY)
    {
        this.x = pX;
        this.y = pY;
    }

    public void Mult(float pScale)
    {
        this.x *= pScale;
        this.y *= pScale;
    }

    public void Div(float pNum)
    {
        if (pNum == 0) return;
        this.x /= pNum;
        this.y /= pNum;
    }

    public float Length() => Mathf.Sqrt(this.x * this.x + this.y * this.y);
    
    public void Normalize()
    {
        float length = this.Length();
        if (length == 0) return;
        this.Div(length);
    }

    //extra method
    public void SetLength(float pScale)
    {
        this.Normalize();
        this.Mult(pScale);
    }

    //extra method
    public void LimitLength(float pLimit)
    {
        float length = this.Length();
        if (length > pLimit) SetLength(pLimit);
    }

    public float GetAngleDegrees() => Rad2Deg(GetAngleRadians());

    public float GetAngleRadians() => Mathf.Atan2(this.y, this.x);

    public void SetAngleDegrees(float pDegrees)
    {
        this.SetAngleRadians(Deg2Rad(pDegrees));
    }

    public void SetAngleRadians(float pRadians)
    {
        float length = this.Length();
        this.SetXY(GetUnitVectorRad(pRadians));
        this.Mult(length);
    }

    public Vector2 Normalized()
    {
        float length = this.Length();
        if (length == 0) return new Vector2(0, 0);
        return new Vector2(this.x / length, this.y / length);
    }

    //extra method
    public static Vector2 COM(Vector2 pVelocity1, Vector2 pVelocity2, float pMass1, float pMass2) => (pVelocity1 * pMass1 + pVelocity2 * pMass2) / (pMass1 + pMass2);

    public void Reflect(float pBounciness, Vector2 pNormal, Vector2 pCom = new Vector2())
    {
        Vector2 vectorProjection = Vector2.VectorProjection(this - pCom, pNormal);
        this.SetXY(this - (1 + pBounciness) * vectorProjection);
    }

    public float Dot(Vector2 pOther) => Dot(this, pOther);

    public static float Dot(Vector2 pLeft, Vector2 pRight) => pLeft.x * pRight.x + pLeft.y * pRight.y;

    //extra method
    public float ScalarProjection(Vector2 pOther, bool pAlreadyUnitVec = false) => ScalarProjection(this, pOther, pAlreadyUnitVec);

    public static float ScalarProjection(Vector2 pLeft, Vector2 pRight, bool pAlreadyUnitVec = false)
    {
        return pAlreadyUnitVec ? Dot(pLeft, pRight) : Dot(pLeft, pRight.Normalized());
    }

    public Vector2 Normal() => new Vector2(-this.y, this.x).Normalized();

    //extra method
    public static Vector2 VectorProjection(Vector2 pLeft, Vector2 pRight, bool pAlreadyUnitVec = false)
    {
        float sp = ScalarProjection(pLeft, pRight, pAlreadyUnitVec);
        return pAlreadyUnitVec ? pRight * sp : pRight.Normalized() * sp;
    }

    public static float Deg2Rad(float pDegrees) => pDegrees * Mathf.PI / 180;

    public static float Rad2Deg(float pRadians) => pRadians * 180 / Mathf.PI;

    public static Vector2 GetUnitVectorDeg(float pDegrees)
    {
        return GetUnitVectorRad(Deg2Rad(pDegrees));
    }

    public static Vector2 GetUnitVectorRad(float pRad)
    {
        float x = Mathf.Cos(pRad);
        float y = Mathf.Sin(pRad);

        return new Vector2(x, y);
    }

    public static Vector2 RandomUnitVector()
    {
        float randomRadian = Utils.Random(0, Mathf.PI * 2);
        return GetUnitVectorRad(randomRadian);
    }

    public void RotateDegrees(float pDegrees)
    {
        RotateRadians(Deg2Rad(pDegrees));
    }

    public void RotateRadians(float pRadians)
    {
        float sin = Mathf.Sin(pRadians);
        float cos = Mathf.Cos(pRadians);
        SetXY(cos * this.x - sin * this.y, cos * this.y + sin * this.x);
    }

    public void RotateAroundDegrees(Vector2 pTarget, float pDegrees)
    {
        RotateAroundRadians(pTarget, Deg2Rad(pDegrees));
    }

    public void RotateAroundRadians(Vector2 pTarget, float pRadians)
    {
        this -= pTarget;
        RotateRadians(pRadians);
        this += pTarget;
    }

    //extra method
    public float DistanceBetween(Vector2 pOther)
    {
        return Mathf.Sqrt((pOther.x - this.x) * (pOther.x - this.x) + (pOther.y - this.y) * (pOther.y - this.y));
    }

    public static Vector2 operator -(Vector2 pFirst, Vector2 pSecond) => new Vector2(pFirst.x - pSecond.x, pFirst.y - pSecond.y);
    public static Vector2 operator *(Vector2 pVecToScale, float pScalar) => new Vector2(pVecToScale.x * pScalar, pVecToScale.y * pScalar);
    public static Vector2 operator *(float pScalar, Vector2 pVecToScale) => pVecToScale * pScalar;
    public static Vector2 operator /(Vector2 pVecToDiv, float pDivBy) => new Vector2(pVecToDiv.x / pDivBy, pVecToDiv.y / pDivBy);
    public static Vector2 operator +(Vector2 pFirst, Vector2 pSecond) => new Vector2(pFirst.x + pSecond.x, pFirst.y + pSecond.y);

    public override string ToString()
    {
        return string.Format("{0}, {1}", x, y);
    }

}

