using RAID2D.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RAID2D;

namespace RAID2D
{
    public static class GameObjectFactory
    {
        private static Random random = new Random();
        private static Dictionary<string, IGameObject> valuableItems = new Dictionary<string, IGameObject>
        {
            { "gold", new ValuableItem("gold", 100, 10, Properties.Resources.gold) },
            { "rolex", new ValuableItem("rolex", 60, 20, Properties.Resources.rolex) },
            { "parcel_box", new ValuableItem("parcel_box", 20, 35, Properties.Resources.parcel_box) },
            { "cigarettes", new ValuableItem("cigarettes", 20, 35, Properties.Resources.cigarettes) }
        };
        private static Dictionary<string, IGameObject> medicalItems = new Dictionary<string, IGameObject>
        {
            { "small_medkit", new MedicalItem("small_medkit", 20, 90, Properties.Resources.small_medkit) },
            { "large_medkit", new MedicalItem("large_medkit", 50, 90, Properties.Resources.large_medkit) },
            { "health_potion", new MedicalItem("health_potion", 100, 90, Properties.Resources.large_medkit) }
        };

        public static IGameObject CreateGameObject(string objectType, Point location)
        {
            switch (objectType.ToLower())
            {
                case "valuable":
                    IGameObject valuableItem = valuableItems.ElementAt(random.Next(valuableItems.Count)).Value;
                    valuableItem.CreatePictureBox(location);
                    return valuableItem;
                case "medical":
                    IGameObject medicalItem = medicalItems.ElementAt(random.Next(medicalItems.Count)).Value;
                    medicalItem.CreatePictureBox(location);
                    return medicalItem;

                default:
                    throw new ArgumentException("Invalid object type");
            }

        }

    }

}
