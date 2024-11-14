namespace RAID2D.Client.Entities.Enemies.Decorators;

public class CloakedEnemyDecorator : EnemyDecorator
{
    private Timer cloakTimer;
    private Timer visibilityTimer;
    private Timer implosionTimer;
    private bool isCloaked;
    private readonly int cloakDuration = 2000;
    private readonly int visibilityDuration = 3000;
    private readonly int implosionDuration = 1000;
    private readonly PictureBox enemyPictureBox;
    private Size originalSize;
    private readonly int implosionStep = 10;

    public CloakedEnemyDecorator(IEnemy baseEnemy) : base(baseEnemy)
    {
        enemyPictureBox = baseEnemy.PictureBox;
        originalSize = enemyPictureBox.Size;
        InitializeCloakingBehavior();
    }

    private void InitializeCloakingBehavior()
    {
        cloakTimer = new Timer { Interval = visibilityDuration };
        visibilityTimer = new Timer { Interval = cloakDuration };
        implosionTimer = new Timer { Interval = implosionDuration / (originalSize.Width / implosionStep) };

        cloakTimer.Tick += (s, e) => StartImplosion();
        visibilityTimer.Tick += (s, e) => Uncloak();
        implosionTimer.Tick += (s, e) => Implode();

        cloakTimer.Start();
    }

    private void StartImplosion()
    {
        if (isCloaked)
            return;

        isCloaked = true;
        implosionTimer.Start();
        cloakTimer.Stop();
    }

    private void Implode()
    {
        if (enemyPictureBox.Width > implosionStep && enemyPictureBox.Height > implosionStep)
        {
            enemyPictureBox.Width -= implosionStep;
            enemyPictureBox.Height -= implosionStep;

            enemyPictureBox.Left += implosionStep / 2;
            enemyPictureBox.Top += implosionStep / 2;
        }
        else
        {
            enemyPictureBox.Visible = false;
            implosionTimer.Stop();
            visibilityTimer.Start();
        }
    }

    private void Uncloak()
    {
        if (!isCloaked)
            return;

        isCloaked = false;
        enemyPictureBox.Visible = true;
        enemyPictureBox.Size = originalSize;
        enemyPictureBox.Left -= (originalSize.Width - enemyPictureBox.Width) / 2;
        enemyPictureBox.Top -= (originalSize.Height - enemyPictureBox.Height) / 2;
        cloakTimer.Start();
        visibilityTimer.Stop();
    }

    public override void UpdateAppearance()
    {
        if (isCloaked)
        {
            enemyPictureBox.BackColor = Color.Transparent;
        }
        else
        {
            enemyPictureBox.BackColor = Color.Red;
        }
    }
}

