using RAID2D.Client.Drops.Spawners;
using RAID2D.Client.Entities.Spawners;
using RAID2D.Client.Players;

namespace RAID2D.Client.Interaction_Handlers;

public abstract class InteractionHandlerBase
{
    public IEntitySpawner? EntitySpawner { get; set; }
    public IDropSpawner? DropSpawner { get; set; }
    public Player? Player { get; set; }

    public void HandleInteractionWithPlayer(PictureBox entity)
    {
        if (this.Player == null ||
            !IsValidEntity(entity) ||
            !Intersects(entity, this.Player))
        {
            return;
        }

        OnCollisionWithPlayer(entity);
        OnControlRemove(entity);
    }

    public void HandleInteractionWithBullet(PictureBox entity, PictureBox bullet)
    {
        if (this.Player == null ||
            !IsValidEntity(entity) || 
            !IsValidBullet(bullet) ||
            !Intersects(entity, bullet))
        {
            return;
        }

        OnCollisionWithBullet(entity, bullet);
        OnControlRemove(entity);
        OnControlRemove(entity);
    }

    public void SpawnEntity()
    {
        if (EntitySpawner == null)
            return;

        PictureBox? entity = GetSpawnEntity();
        if (entity == null)
            return;

        OnControlAdd(entity);
    }

    protected abstract bool IsValidEntity(PictureBox entity);

    protected virtual void OnCollisionWithPlayer(PictureBox entity) { }
    protected virtual void OnCollisionWithBullet(PictureBox entity, PictureBox bullet) { }
    protected virtual void OnControlAdd(PictureBox entity) { }
    protected virtual void OnControlRemove(PictureBox entity) { }
    protected virtual PictureBox? GetSpawnEntity() { return null; }

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
