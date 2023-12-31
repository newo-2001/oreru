namespace Oreru.Domain.Storyboards;

/// <summary>
/// An individual object on a storyboard.
/// It functions as the leaf nodes of the <see cref="IEffect"/> graph.
/// </summary>
public class StoryboardObject : IEffect
{
    public required string ImagePath { get; set; }
    public required Vector2 InitialPosition { get; set; }
    public StoryboardLayer Layer { get; set; } = StoryboardLayer.Background;
    public Origin Origin { get; set; } = Origin.TopLeft;
    public List<ICommand> Commands { get; set; } = [];

    IEnumerable<ICommand> IEffect.Commands => Commands;
}
