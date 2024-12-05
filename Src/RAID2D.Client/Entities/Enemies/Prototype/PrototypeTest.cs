using System.Diagnostics;

namespace RAID2D.Client.Entities.Enemies.Prototype;

public static class PrototypeTest
{
    public static void Run()
    {
        var creeper = new Creeper();
        creeper.Create(); // Initialize the original creeper's PictureBox

        // Create shallow and deep clones
        var shallowClone = (Creeper)creeper.ShallowClone();
        var deepClone = (Creeper)creeper.DeepClone();

        // Display hash codes (unique identifiers) for PictureBox in each instance
        Debug.WriteLine("Testing Creeper Clones (Shallow vs Deep Copy)");

        Debug.WriteLine("Original PictureBox HashCode: " + creeper.PictureBox.GetHashCode());
        Debug.WriteLine("Shallow Clone PictureBox HashCode: " + shallowClone.PictureBox.GetHashCode());
        Debug.WriteLine("Deep Clone PictureBox HashCode: " + deepClone.PictureBox.GetHashCode());

        // Compare PictureBox references directly
        Debug.WriteLine("Are PictureBoxes (Original vs Shallow) same reference? " + ReferenceEquals(creeper.PictureBox, shallowClone.PictureBox));
        Debug.WriteLine("Are PictureBoxes (Original vs Deep) same reference? " + ReferenceEquals(creeper.PictureBox, deepClone.PictureBox));

        // Modify properties to see effects in clones
        creeper.PictureBox.Location = new Point(100, 100);
        Debug.WriteLine("Modified Original PictureBox Location: " + creeper.PictureBox.Location);

        Debug.WriteLine("Shallow Clone PictureBox Location after original modification: " + shallowClone.PictureBox.Location);
        Debug.WriteLine("Deep Clone PictureBox Location after original modification: " + deepClone.PictureBox.Location);

        // Optional: if you need memory addresses, use GCHandle as mentioned previously
    }
}
