namespace SeaBattleNew
{
    internal class Kater : Ship
    {
        int x1, y1;  // Координаты поля, от которго строится корабль.

        public Kater()
        {
            Random rand = new Random();

            // Определяем координаты катера и придаем клеткам статус, что на них стоит катер.
            do
            {
                x1 = rand.Next(10);
                y1 = rand.Next(10);
            }
            while (
                Form1.fieldStatus[int.Parse(string.Concat(y1, x1))] != 01 
            );

            for (int i = x1 - 1; i <= x1 + 1; i++)  // Отмечаем клетки, соседствующие с кораблем.
            {
                for (int j = y1 - 1; j <= y1 + 1; j++)
                {
                    if ((i >= 0) && (i <= 9) && (j >= 0) && (j <= 9))
                    { 
                        Form1.fieldStatus[int.Parse(string.Concat(j, i))] = 02;
                    }
                }
            }

            Form1.fieldStatus[int.Parse(string.Concat(y1, x1))] = 11;  // Отмечаем клетки, на которых корабль.
        }
    }
}