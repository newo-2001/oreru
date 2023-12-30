using System.Diagnostics.CodeAnalysis;

namespace Oreru.Domain;
public class Interpolated<T>
{
    public required T StartValue { get; set; }
    public required T EndValue { get; set; }

    public Interpolated() {}
    
    [SetsRequiredMembers]
    public Interpolated(T startValue, T endValue)
    {
        StartValue = startValue;
        EndValue = endValue;
    }
}
