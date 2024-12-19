using RAID2D.Client.Managers;

namespace RAID2D.Client.Interaction_Handlers;

public class DropInteractionHandler : InteractionHandlerBase
{
    protected sealed override bool IsValidEntity(PictureBox drop)
    {
        return
            drop.Tag as string is
            Constants.DropAmmoTag or
            Constants.DropAnimalTag or
            Constants.DropMedicalTag or
            Constants.DropValuableTag;
    }

    protected sealed override bool OnCollisionWithPlayer(PictureBox drop)
    {
        switch (drop.Tag as string)
        {
            case Constants.DropAmmoTag:
                AmmoDropData ammoDrop = DropManager.GetAmmoDropData(drop.Name);
                Form.player.PickupAmmo(ammoDrop.AmmoAmount);
                break;
            case Constants.DropAnimalTag:
                AnimalDropData animalDrop = DropManager.GetAnimalDropData(drop.Name);
                Form.player.PickupHealable(animalDrop.HealthAmount);
                break;
            case Constants.DropMedicalTag:
                MedicalDropData medicalDrop = DropManager.GetMedicalDropData(drop.Name);
                Form.player.PickupHealable(medicalDrop.HealthAmount);
                break;
            case Constants.DropValuableTag:
                ValuableDropData valuableDrop = DropManager.GetValuableDropData(drop.Name);
                Form.player.PickupValuable(valuableDrop.CashAmount);
                break;
            default:
                throw new NotImplementedException();
        }

        return true;
    }
}
