using System.Numerics;

namespace Oreru.Domain;
public record Vector2(double X, double Y) :
    IAdditionOperators<Vector2, Vector2, Vector2>,
    IAdditionOperators<Vector2, double, Vector2>,
    ISubtractionOperators<Vector2, Vector2, Vector2>,
    ISubtractionOperators<Vector2, double, Vector2>,
    IMultiplyOperators<Vector2, double, Vector2>,
    IDivisionOperators<Vector2, double, Vector2>,
    IUnaryNegationOperators<Vector2, Vector2>,
    IMultiplicativeIdentity<Vector2, Vector2>,
    IAdditiveIdentity<Vector2, Vector2>
{

    public static readonly Vector2 One = new Vector2(1, 1);
    public static Vector2 MultiplicativeIdentity => One;

    public static readonly Vector2 Zero = new Vector2(0, 0);
    public static Vector2 AdditiveIdentity => Zero;

    public static implicit operator Vector2((double, double) values) => new Vector2(values.Item1, values.Item2);

    public static Vector2 operator +(Vector2 left, Vector2 right)
    {
        return new Vector2(left.X + right.X, left.Y + right.Y);
    }

    public static Vector2 operator +(Vector2 left, double right)
    {
        return new Vector2(left.X + right, left.Y + right);
    }

    public static Vector2 operator -(Vector2 left, Vector2 right)
    {
        return new Vector2(left.X - right.X, left.Y - right.Y);
    }

    public static Vector2 operator -(Vector2 left, double right)
    {
        return new Vector2(left.X - right, left.Y - right);
    }

    public static Vector2 operator *(Vector2 left, double right)
    {
        return new Vector2(left.X * right, left.Y * right);
    }

    public static Vector2 operator /(Vector2 left, double right)
    {
        return new Vector2(left.X / right, left.Y / right);
    }

    public static Vector2 operator -(Vector2 value)
    {
        return new Vector2(-value.X, -value.Y);
    }

    public double Length => Math.Sqrt(X * X + Y * Y);
    public Vector2 Normalized() => this / Length;
}
