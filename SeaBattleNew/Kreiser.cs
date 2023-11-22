namespace SeaBattleNew
{
    internal class Kreiser : Ship
    {
        int x1, y1;  // Координаты поля, от которго строится корабль.

        public int[] fields = new int[3];  // Массивы клеток корабля и окружения (чтобы определить раненые корабли и окружение). 
        public int[] aroundFields = new int[12];

        public Kreiser()
        {
            Random rand = new Random();
            lives = 3;
            direction = rand.Next(2);
            int coordCell;  // Координаты, куда мы поставили корабль.

            for (int i = 0; i < 3; i++) { fields[i] = 100; }  // 100 - это битые/неиспользованные клетки.
            for (int i = 0; i < 12; i++) { aroundFields[i] = 100; }

            // Определяем координаты крейсера и придаем клеткам статус, что на них стоит крейсер.
            if (direction == 0)
            {
                do
                {
                    x1 = rand.Next(8);
                    y1 = rand.Next(10);
                    coordCell = int.Parse(y1.ToString() + x1.ToString());
                }
                while (
                    (Form1.fieldStatus[coordCell] != 01) ||
                    (Form1.fieldStatus[coordCell + 1] != 01) ||
                    (Form1.fieldStatus[coordCell + 2] != 01)
                );

                for (int i = x1 - 1, k = 0; i <= x1 + 3; i++)  // Отмечаем клетки, соседствующие с кораблем.
                {
                    for (int j = y1 - 1; j <= y1 + 1; j++)
                    {
                        if ((i >= x1) && (i <= x1 + 2) && (j == y1))  // Исключаем клетки корабля из этого цикла.
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

                Form1.fieldStatus[coordCell] = 31;  // Отмечаем клетки, на которых корабль.
                Form1.fieldStatus[coordCell + 1] = 31;
                Form1.fieldStatus[coordCell + 2] = 31;

                for (int i = 0; i < 3; i++)  // Заполняем массив клеток корабля.
                {
                    fields[i] = coordCell + i;
                }
            }
            else
            {
                do
                {
                    x1 = rand.Next(10);
                    y1 = rand.Next(8);
                    coordCell = int.Parse(y1.ToString() + x1.ToString());
                }
                while (
                    (Form1.fieldStatus[coordCell] != 01) ||
                    (Form1.fieldStatus[coordCell + 10] != 01) ||
                    (Form1.fieldStatus[coordCell + 20] != 01)
                );

                for (int i = x1 - 1, k = 0; i <= x1 + 1; i++)  // Отмечаем клетки, соседствующие с кораблем.
                {
                    for (int j = y1 - 1; j <= y1 + 3; j++)
                    {
                        if ((j >= y1) && (j <= y1 + 2) && (i == x1))  // Исключаем клетки корабля из этого цикла.
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

                Form1.fieldStatus[coordCell] = 31;  // Отмечаем клетки, на которых корабль.
                Form1.fieldStatus[coordCell + 10] = 31;
                Form1.fieldStatus[coordCell + 20] = 31;

                for (int i = 0; i < 3; i++)  // Заполняем массив клеток корабля.
                {
                    fields[i] = coordCell + i * 10;
                }
            }
        }

        public Kreiser(bool a)  // Фиктивный конструктор - для того, чтобы конструктор не срабатывал для наших кораблей.
        {
            lives = 3;
        }
    }
}