using System;

namespace task04
{
    public class Fighter : ISpaceship
    {
        public int Speed => 100;
        public int FirePower => 50;
        private int _angle = 0;

        public void MoveForward()
        {
            Console.WriteLine($"Истребитель летит вперёд со скоростью {Speed} км/ч");
        }

        public void Rotate(int angle)
        {
            _angle = (_angle + angle) % 360;
            Console.WriteLine($"Истребитель повернут на угол {_angle} градусов");
        }

        public void Fire()
        {
            Console.WriteLine($"Истребитель выпускает быструю, но слабую ракету. Мощность {FirePower} МВт");
        }
    }
}