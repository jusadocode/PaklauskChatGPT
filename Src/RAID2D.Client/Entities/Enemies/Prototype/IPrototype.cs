using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAID2D.Client.Entities.Enemies.Prototype;
public interface IPrototype
{
    IPrototype ShallowClone();
    IPrototype DeepClone();
}
