using RAID2D.Client.Players;

namespace RAID2D.Client.Interaction_Handlers;

public abstract class InteractionHandlerBase
{
    public MainForm? Form { get; set; }

    public void HandleInteractionWithPlayer(PictureBox entity)
    {
        if (this.Form == null ||
            !IsValidEntity(entity) ||
            !Intersects(entity, this.Form.player))
        {
            return;
        }

        try
        {
            OnCollisionWithPlayer(entity);
            OnControlRemove(entity);
        }
        catch (Exception e)
        {
            if (e is not NotImplementedException)
            {
                throw new Exception("This shouldnt happen");
            }
        }
    }

    public void HandleInteractionWithBullet(PictureBox entity, PictureBox bullet)
    {
        if (this.Form == null ||
            !IsValidEntity(entity) ||
            !IsValidBullet(bullet) ||
            !Intersects(entity, bullet))
        {
            return;
        }

        try
        {
            OnCollisionWithBullet(entity, bullet);
            OnControlRemove(entity);
            OnControlRemove(bullet);
        }
        catch (Exception e)
        {
            if (e is not NotImplementedException)
            {
                throw new Exception("This shouldnt happen");
            }
        }
    }

    public void SpawnEntity()
    {
        if (this.Form == null)
            return;

        PictureBox? entity = GetSpawnEntity();
        if (entity == null)
            return;

        OnControlAdd(entity);
    }

    protected abstract bool IsValidEntity(PictureBox entity);

    protected virtual void OnCollisionWithPlayer(PictureBox entity) { throw new NotImplementedException(); }
    protected virtual void OnCollisionWithBullet(PictureBox entity, PictureBox bullet) { throw new NotImplementedException(); }
    protected virtual PictureBox? GetSpawnEntity() { return null; }

    protected void OnControlAdd(PictureBox entity)
    {
        Form.AddControl(entity);
    }

    protected void OnControlRemove(PictureBox entity)
    {
        Form.RemoveControl(entity);
    }

    private bool Intersects(PictureBox box1, PictureBox box2)
    {
        return box1.Bounds.IntersectsWith(box2.Bounds);
    }

    private bool Intersects(PictureBox box, Player player)
    {
        return box.Bounds.IntersectsWith(player.PictureBox.Bounds);
    }

    private bool IsValidBullet(PictureBox bullet)
    {
        return bullet.Tag as string is Constants.BulletTag;
    }
}
