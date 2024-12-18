using RAID2D.Client.Players;

namespace RAID2D.Client.Handlers;

public abstract class InteractionHandler
{
    protected InteractionHandler? NextHandler { get; private set; }

    public void SetNext(InteractionHandler next)
    {
        NextHandler = next;
    }

    public abstract void Handle(PictureBox entity, Player player);
}
