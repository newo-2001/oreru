namespace Oreru.Domain.Storyboards;
public abstract record Command : ITemporal
{
    public abstract Interval Interval { get; }

    public sealed record Primitive(PrimitiveCommand Command) : Command
    {
        public override Interval Interval => Command.Interval;
    }

    public sealed record Composite(CompositeCommand Command) : Command
    {
        public override Interval Interval => Command.Interval;
    }

    private Command() {}
}
