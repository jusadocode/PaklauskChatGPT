using RAID2D.Client.Drops.Builders;
using RAID2D.Client.Drops;
using RAID2D.Client.Entities.Animals;

namespace RAID2D.Client.Interaction_Handlers;

public class AnimalInteractionHandler : InteractionHandlerBase
{
    protected sealed override bool IsValidEntity(PictureBox entity)
    {
        return (entity.Tag as string) == Constants.AnimalTag;
    }

    protected sealed override void OnCollisionWithBullet(PictureBox entity, PictureBox bullet)
    {
        Player!.RegisterKill(bullet.Bounds.Location);

        SpawnAnimalDrop(entity.Location, entity.Name);
        base.SpawnEntity();
    }

    protected sealed override PictureBox? GetSpawnEntity() 
    {
        IAnimal animal = EntitySpawner!.CreateAnimal();

        return animal.PictureBox;
    }

    private void SpawnAnimalDrop(Point location, string animalName)
    {
        IDroppableItem animalDrop = DropSpawner!.CreateDrop(Constants.DropAnimalTag, location, animalName);

        PictureBox animalPictureBox = new AnimalDropBuilder()
            .SetTag(animalDrop)
            .SetName(animalDrop.Name)
            .SetImage(animalDrop.Image)
            .SetLocation(animalDrop.Location)
            .SetSize(animalDrop.Size)
            .SetSizeMode(Constants.SizeMode)
            .Build();

        base.OnControlAdd(animalPictureBox);
    }
}
