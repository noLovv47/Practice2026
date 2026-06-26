using System;

namespace task04
{
    public class Cruiser : ISpaceship
    {
        public int Speed => 50;
        public int FirePower => 100;
        private int _angle = 0;

        public void MoveForward()
        {
            Console.WriteLine($"Крейсер движется вперёд со скоростью {Speed} км/ч");
        }

        public void Rotate(int angle)
        {
            _angle = (_angle + angle) % 360;
            Console.WriteLine($"Крейсер повернут на угол {_angle} градусов");
        }

        public void Fire()
        {
            Console.WriteLine($"Крейсер выпускает мощную фотонную ракету! Мощность {FirePower} МВт");
        }
    }
}