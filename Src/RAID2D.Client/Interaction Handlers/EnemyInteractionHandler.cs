using RAID2D.Client.Entities.Enemies.Decorators;
using RAID2D.Client.Entities.Enemies;
using RAID2D.Client.Handlers;
using RAID2D.Client.Utils;
using RAID2D.Client.Drops.Builders;
using RAID2D.Client.Drops;

namespace RAID2D.Client.Interaction_Handlers;

public class EnemyInteractionHandler : InteractionHandlerBase
{

    private static readonly Dictionary<PictureBox, int> shieldedEnemiesHealth = [];

    private static InteractionHandler enemyHandlerChain = new EnemyHandler();

    public EnemyInteractionHandler()
    {
        var enemyHandler = new EnemyHandler();
        var pulsingEnemyHandler = new PulsingEnemyHandler();
        var shieldedEnemyHandler = new ShieldedEnemyHandler();
        enemyHandler.SetNext(pulsingEnemyHandler);
        pulsingEnemyHandler.SetNext(shieldedEnemyHandler);
        enemyHandlerChain = enemyHandler;
    }

    protected override bool IsValidEntity(PictureBox enemy)
    {
        return enemy.Tag is string tag && tag.Contains(Constants.EnemyTag);
    }

    protected override void OnCollisionWithPlayer(PictureBox enemy)
    {
        enemyHandlerChain.Handle(enemy, base.Player!);
    }

    protected override void OnCollisionWithBullet(PictureBox enemy, PictureBox bullet)
    {
        if (IsShieldedEnemy(enemy))
        {
            if (!ManageShieldedEnemyHealth(enemy))
                return;
        }

        base.Player!.RegisterKill(bullet.Bounds.Location);

        SpawnValuableDrop(enemy.Location);
        base.SpawnEntity();
    }

    private bool ManageShieldedEnemyHealth(PictureBox enemy)
    {
        bool killed = false;

        if (shieldedEnemiesHealth.TryGetValue(enemy, out int currentHealth))
        {
            currentHealth -= 10;

            shieldedEnemiesHealth[enemy] = currentHealth;

            if (currentHealth <= 0)
            {
                killed = true;
                shieldedEnemiesHealth.Remove(enemy);
            }
        }

        return killed;
    }

    protected sealed override PictureBox? GetSpawnEntity()
    {
        IEnemy enemy = EntitySpawner!.CreateEnemy();

        if (Rand.Next(0, 101) < Constants.MutatedEnemySpawnChance)
        {
            switch (Rand.Next(0, 4))
            {
                case 0:
                    enemy = new ShieldedEnemyDecorator(enemy);
                    break;
                case 1:
                    enemy = new PulsingEnemyDecorator(enemy);
                    break;
                case 2:
                    enemy = new ShieldedEnemyDecorator(new PulsingEnemyDecorator(enemy));
                    break;
                case 3:
                    enemy = new CloakedEnemyDecorator(enemy);
                    break;
                default:
                    break;
            }
        }

        PictureBox pictureBox = enemy.PictureBox;

        if (IsShieldedEnemy(pictureBox))
            shieldedEnemiesHealth[pictureBox] = Constants.ShieldedEnemyMaxHealth;

        return pictureBox;
    }

    private void SpawnValuableDrop(Point location)
    {
        IDroppableItem valuableDrop = DropSpawner.CreateDrop(Constants.DropValuableTag, location);

        PictureBox valuablePictureBox = new ValuableDropBuilder()
            .SetTag(valuableDrop)
            .SetName(valuableDrop.Name)
            .SetImage(valuableDrop.Image)
            .SetLocation(valuableDrop.Location)
            .SetSize(valuableDrop.Size)
            .SetSizeMode(Constants.SizeMode)
            .Build();

        base.OnControlAdd(valuablePictureBox);
    }

    private static bool IsShieldedEnemy(Control enemy) => enemy.Tag is string tag && tag.Contains(Constants.ShieldedEnemyTag);
}

