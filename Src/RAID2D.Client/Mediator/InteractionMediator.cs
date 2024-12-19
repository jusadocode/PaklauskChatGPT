using RAID2D.Client.Drops;
using RAID2D.Client.Entities.Animals;
using RAID2D.Client.Entities.Enemies;
using RAID2D.Client.Entities;
using RAID2D.Client.Entities.Spawners;
using RAID2D.Client.Interaction_Handlers;
using RAID2D.Client.Players;
using RAID2D.Client.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAID2D.Client.Effects;

namespace RAID2D.Client.Mediator;
public class InteractionMediator
{
    public InteractionHandlerBase ?AnimalHandlerColleague { get; set; }
    public InteractionHandlerBase ?EnemyHandlerColleague { get; set; }
    public InteractionHandlerBase ?DropHandlerColleague { get; set; }


    public void NotifyEntityCollision(PictureBox entity)
    {
        if (entity.Tag is Constants.EnemyTag)
        {
            EnemyHandlerColleague.HandleInteractionWithPlayer(entity);
        }
        else if (entity.Tag is Constants.AnimalTag)
        {
            AnimalHandlerColleague.HandleInteractionWithPlayer(entity);
        }
        else if (entity.Tag as string is
            Constants.DropAmmoTag or
            Constants.DropAnimalTag or
            Constants.DropMedicalTag or
            Constants.DropValuableTag)
        {
            DropHandlerColleague.HandleInteractionWithPlayer(entity);
        }
    }

    public void NotifyBulletCollision(PictureBox entity, PictureBox bullet)
    {
        AnimalHandlerColleague.HandleInteractionWithBullet(entity, bullet);
        EnemyHandlerColleague.HandleInteractionWithBullet(entity, bullet);
    }

    public void SpawnEntities()
    {
        for (int i = 0; i < Constants.AnimalCount; i++)
            AnimalHandlerColleague.SpawnEntity();

        for (int i = 0; i < Constants.EnemyCount; i++)
            EnemyHandlerColleague.SpawnEntity();
    }

}
