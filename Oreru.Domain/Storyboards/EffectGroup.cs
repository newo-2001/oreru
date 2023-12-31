

namespace Oreru.Domain.Storyboards;
public class EffectGroup : IEffect
{
    public List<IEffect> Children { get; set; } = [];

    public List<ICommand> Commands { get; set; } = [];

    IEnumerable<ICommand> IEffect.Commands => Commands;
}
