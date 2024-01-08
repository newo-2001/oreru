using System.Diagnostics.CodeAnalysis;

namespace Oreru.Domain;
public struct Interpolated<T>
{
    public required T StartValue { get; set; }
    public required T EndValue { get; set; }

    [SetsRequiredMembers]
    public Interpolated(T startValue, T endValue)
    {
        StartValue = startValue;
        EndValue = endValue;
    }
}
