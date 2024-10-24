namespace Client.Movement;

public class FleeMovement(PictureBox player, int speed) : IMovementStrategy
{
    private readonly int fleeDistance = 200; // Distance threshold for fleeing

    public void Move(PictureBox entity)
    {
        double distance = Math.Sqrt(Math.Pow(entity.Left - player.Left, 2) + Math.Pow(entity.Top - player.Top, 2));

        if (distance < fleeDistance)
        {
            if (entity.Left < player.Left)
                entity.Left -= speed; // Move left
            else
                entity.Left += speed; // Move right

            if (entity.Top < player.Top)
                entity.Top -= speed; // Move up
            else
                entity.Top += speed; // Move down
        }
    }
}
