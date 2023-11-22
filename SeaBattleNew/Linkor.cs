namespace SeaBattleNew
{
    public class Linkor : Ship
    {
        int x1, y1;  // Координаты поля, от которго строится корабль.

        public int[] fields = new int[4];  // Массивы клеток корабля и окружения (чтобы определить раненые корабли и окружение). 
        public int[] aroundFields = new int[14];

        public Linkor()
        {
            Random rand = new Random();
            lives = 4;
            direction = rand.Next(2);
            int coordCell;  // Координаты, куда мы поставили корабль.

            for (int i = 0; i < 4; i++) { fields[i] = 100; }  // 100 - это битые/неиспользованные клетки.
            for (int i = 0; i < 14; i++) { aroundFields[i] = 100; }

            // Определяем координаты линкора и придаем клеткам статус, что на них стоит линкор.
            if (direction == 0)
            {
                x1 = rand.Next(7);
                y1 = rand.Next(10);
                coordCell = int.Parse(y1.ToString() + x1.ToString());

                for (int i = x1 - 1, k = 0; i <= x1 + 4; i++)  // Отмечаем клетки, соседствующие с кораблем.
                {
                    for (int j = y1 - 1; j <= y1 + 1; j++)
                    {
                        if ((i >= x1) && (i <= x1 + 3) && (j == y1))  // Исключаем клетки корабля из этого цикла.
                        {
                            continue;
                        }

                        if ((i < 10) && (j < 10) && (i >= 0) && (j >= 0))  // Ограничение, чтобы координаты клетки не были -1 или больше 9.
                        {
                            Form1.fieldStatus[int.Parse(string.Concat(j, i))] = 02;

                            aroundFields[k] = int.Parse(string.Concat(j, i));  // Заполняем массив клеток вокруг корабля.
                            k++;
                        }
                    }
                }

                Form1.fieldStatus[coordCell] = 41;  // Отмечаем клетки, на которых корабль.
                Form1.fieldStatus[coordCell + 1] = 41;
                Form1.fieldStatus[coordCell + 2] = 41;
                Form1.fieldStatus[coordCell + 3] = 41;

                for (int i = 0; i < 4; i++)  // Заполняем массив клеток корабля.
                {
                    fields[i] = coordCell + i;
                }
            }
            else
            {
                x1 = rand.Next(10);
                y1 = rand.Next(7);
                coordCell = int.Parse(y1.ToString() + x1.ToString());

                for (int i = x1 - 1, k = 0; i <= x1 + 1; i++)  // Отмечаем клетки, соседствующие с кораблем.
                {
                    for (int j = y1 - 1; j <= y1 + 4; j++)
                    {
                        if ((j >= y1) && (j <= y1 + 3) && (i == x1))  // Исключаем клетки корабля из этого цикла.
                        {
                            continue;
                        }

                        if ((i < 10) && (j < 10) && (i >= 0) && (j >= 0))  // Ограничение, чтобы координаты клетки не были -1 или больше 9.
                        {
                            Form1.fieldStatus[int.Parse(string.Concat(j, i))] = 02;

                            aroundFields[k] = int.Parse(string.Concat(j, i));  // Заполняем массив клеток вокруг корабля.
                            k++;
                        }
                    }
                }

                Form1.fieldStatus[coordCell] = 41;  // Отмечаем клетки, на которых корабль.
                Form1.fieldStatus[coordCell + 10] = 41;
                Form1.fieldStatus[coordCell + 20] = 41;
                Form1.fieldStatus[coordCell + 30] = 41;

                for (int i = 0; i < 4; i++)  // Заполняем массив клеток корабля.
                {
                    fields[i] = coordCell + i * 10;
                }
            }
        }

        public Linkor(bool a)  // Фиктивный конструктор - для того, чтобы конструктор не срабатывал для наших кораблей.
        {
            lives = 4;
        }
    }
}