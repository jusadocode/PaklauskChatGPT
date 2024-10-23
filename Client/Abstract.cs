//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RAID2D
//{
//    // Abstract Product A
//    public interface IButton
//    {
//        void Render();
//    }

//    // Abstract Product B
//    public interface ICheckbox
//    {
//        void Check();
//    }

//    // Concrete Product A1
//    public class WindowsButton : IButton
//    {
//        public void Render() => Console.WriteLine("Rendering Windows Button");
//    }

//    // Concrete Product B1
//    public class WindowsCheckbox : ICheckbox
//    {
//        public void Check() => Console.WriteLine("Checking Windows Checkbox");
//    }

//    // Concrete Product A2
//    public class MacOSButton : IButton
//    {
//        public void Render() => Console.WriteLine("Rendering MacOS Button");
//    }

//    // Concrete Product B2
//    public class MacOSCheckbox : ICheckbox
//    {
//        public void Check() => Console.WriteLine("Checking MacOS Checkbox");
//    }

//    // Abstract Factory
//    public interface IGUIFactory
//    {
//        IButton CreateButton();
//        ICheckbox CreateCheckbox();
//    }

//    // Concrete Factory 1
//    public class WindowsFactory : IGUIFactory
//    {
//        public IButton CreateButton() => new WindowsButton();
//        public ICheckbox CreateCheckbox() => new WindowsCheckbox();
//    }

//    // Concrete Factory 2
//    public class MacOSFactory : IGUIFactory
//    {
//        public IButton CreateButton() => new MacOSButton();
//        public ICheckbox CreateCheckbox() => new MacOSCheckbox();
//    }

//    // Client
//    public class Application
//    {
//        private readonly IButton _button;
//        private readonly ICheckbox _checkbox;

//        public Application(IGUIFactory factory)
//        {
//            _button = factory.CreateButton();
//            _checkbox = factory.CreateCheckbox();
//        }

//        public void Run()
//        {
//            _button.Render();
//            _checkbox.Check();
//        }
//    }

//    // Main method
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            IGUIFactory factory;

//            // You can switch factories here (Windows or MacOS)
//            factory = new WindowsFactory();
//            // factory = new MacOSFactory();

//            Application app = new Application(factory);
//            app.Run();
//        }
//    }
//}
