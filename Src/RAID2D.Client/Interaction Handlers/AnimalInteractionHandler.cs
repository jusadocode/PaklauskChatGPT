using RAID2D.Client.Drops;
using RAID2D.Client.Drops.Builders;
using RAID2D.Client.Entities.Animals;

namespace RAID2D.Client.Interaction_Handlers;

public class AnimalInteractionHandler : InteractionHandlerBase
{
    protected sealed override bool IsValidEntity(PictureBox animal)
    {
        return (animal.Tag as string) == Constants.AnimalTag;
    }

    protected sealed override void OnCollisionWithBullet(PictureBox animal, PictureBox bullet)
    {
        Form.player.RegisterKill(bullet.Bounds.Location);
        Form.entityList.Remove(animal);

        SpawnAnimalDrop(animal.Location, animal.Name);
        base.SpawnEntity();
    }

    protected sealed override PictureBox? GetSpawnEntity()
    {
        IAnimal animal = Form.entitySpawner.CreateAnimal();
        Form.entityList.Add(animal);

        return animal.PictureBox;
    }

    private void SpawnAnimalDrop(Point location, string animalName)
    {
        IDroppableItem animalDrop = Form.dropSpawner.CreateDrop(Constants.DropAnimalTag, location, animalName);
        Form.dropList.Add(animalDrop);

        PictureBox animalPictureBox = new AnimalDropBuilder()
            .SetTag(animalDrop)
            .SetName(animalDrop.Name)
            .SetImage(animalDrop.Image)
            .SetLocation(animalDrop.Location)
            .SetSize(animalDrop.Size)
            .SetSizeMode(Constants.SizeMode)
            .Build();

        animalDrop.PictureBox = animalPictureBox;

        base.OnControlAdd(animalPictureBox);
    }
}
