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

        if (OnCollisionWithPlayer(entity))
        {
            OnControlRemove(entity);
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

        if (OnCollisionWithBullet(entity, bullet))
        {
            OnControlRemove(entity);
        }

        OnControlRemove(bullet);
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

    protected virtual bool OnCollisionWithPlayer(PictureBox entity) { throw new NotImplementedException(); }
    protected virtual bool OnCollisionWithBullet(PictureBox entity, PictureBox bullet) { throw new NotImplementedException(); }
    protected virtual PictureBox? GetSpawnEntity() { return null; }

    protected void OnControlAdd(PictureBox entity)
    {
        Form.AddControl(entity);
    }

    protected void OnControlRemove(PictureBox entity)
    {
        Form.bulletList.Remove(entity);
        Form.dropList.Remove(entity);
        Form.entityList.Remove(entity);
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
