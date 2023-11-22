using SeaBattleNew.Properties;

namespace SeaBattleNew  
{
    public partial class Form1 : Form
    {
        public static int[] fieldStatus = new int[100];  // Массив статусов клеток поля противника.
        public static int[] myFieldStatus = new int[100];  // Массив статусов клеток своего поля.

        /*         ОБОЗНАЧЕНИЯ КОДОВ-СТАТУСОВ КЛЕТОК:
         * Пустота: 01 - нетронутая пустота, 02 - пустота рядом с кораблем, 03 - битая пустота.
         * Катер: 11 - корабль, 13 - подбит.
         * Эсминец: 21 - корабль, 22 - ранен, 23 - подбит.
         * Крейсер: 31 - корабль, 32 - ранен, 33 - подбит.
         * Линкор: 41 - корабль, 42 - ранен, 43 - подбит. */


        bool dragging = false;  // Чтобы понять, перетаскивается что-нибудь или нет.        

        Linkor linkor1;
        Kreiser[] kreisers;
        Esminec[] esmineces;
        Kater kater1;  // Объекты-катера врага нужны, чтобы сработал конструктор и разместил эти корабли на карте. 
        Kater kater2;
        Kater kater3;
        Kater kater4;

        Linkor myLinkor1;
        Kreiser[] myKreisers;
        Esminec[] myEsmineces;

        int myKreisersInField;  // Счетчики уже поставленных кораблей.
        int myEsminecesInField;
        int myKatersInField;

        int x, y;  // Координаты нажатия мышкой.
        int oldex, oldey;  // Чтобы запомнить начальные координаты картинки (нужно при перетаскивании).

        PictureBox pic = new PictureBox();  // Картинка, которая появляется при перетаскивании кораблей на поле.
        PictureBox[] pic2 = new PictureBox[100];
        PictureBox[] myPic2 = new PictureBox[100];

        int enemyTargetCell;  // Поле, по которому стреляет противник.
        string regimeAttack = "normal";  // Режим, по которому противник будет ходить (зависит от того, подбил ли какой корабль).
        int axialCell;  // Осевая клетка, по которой противник определяет, в каком направлении добивать корабли.
        bool firstStrike = true;  // Показывает, было ли это первое удачное попадание у противника (т.е. первое ранение корабля).

        int shipsDestroyed;
        int myShipsDestroyed;

        Stream str;   //  Музыка.
        System.Media.SoundPlayer snd;


        public Form1()
        {
            InitializeComponent();

            str = Resources.silence;   //  Музыка. Делаем прогон, чтобы потом не подвисала музыка.            
            snd = new System.Media.SoundPlayer(str);            
            snd.Play();
            str = Resources.setship;   //  Ставим нужный звук.           
            snd = new System.Media.SoundPlayer(str);

            for (int i = 0; i < 100; i++)
            {
                fieldStatus[i] = 01;   // Задаем статус пустоты всем клеткам.
                myFieldStatus[i] = 01;
                pic2[i] = new PictureBox();  // Создаем массив пикча-боксов для графического представления статусов клеток. 
                myPic2[i] = new PictureBox();
            }

            myKreisers = new Kreiser[2];  // Инициализируем массивы наших кораблей (которые нужно).
            myEsmineces = new Esminec[3];

            CreateEnemyShips();           

            ShowShips(pictureBox2, pic2, fieldStatus);
        }


        void Win()   // Проверка выигрыша.
        {
            if (shipsDestroyed == 10)
            {
                MessageBox.Show("Уррра!!! Ты выиграл!!! :)", "Победа!");
                Application.Exit();
            }

            if (myShipsDestroyed == 10)
            {
                MessageBox.Show("К сожалению, в этот раз победил комп... :(", "Проигрыш...");
                Application.Exit();
            }
        }


        void CreateEnemyShips()  // Создаем вражьи корабли.
        {
            linkor1 = new();
            kreisers = new Kreiser[2];
            kreisers[0] = new Kreiser();
            kreisers[1] = new Kreiser();
            esmineces = new Esminec[3];
            esmineces[0] = new Esminec();
            esmineces[1] = new Esminec();
            esmineces[2] = new Esminec();
            kater1 = new();  // Объекты-катера врага нужны, чтобы сработал конструктор и разместил эти корабли на карте. 
            kater2 = new();
            kater3 = new();
            kater4 = new();
        }


        void ShowShips(PictureBox picture, PictureBox[] pict, int[] arrayStatus)  // Проявляем корабли на поле 
        {
            for (int i = 0; i < 100; i++)  // Очистка старых картинок (чтобы игра не висла).
            {
                this.Controls.Remove(pict[i]);
            }

            for (int i = 0; i < 100; i++)                             
            {
                int x = i % 10;        
                int y = i / 10;
                int coordCell = int.Parse(y.ToString() + x.ToString());

                if (((arrayStatus[i] != 01) && (arrayStatus[i] != 02) && (arrayStatus == myFieldStatus)) ||
                    ((arrayStatus[i] % 10 != 1) && (arrayStatus[i] != 02)))    // Добавляем картинку там, где это нужно.
                {
                    this.Controls.Add(pict[coordCell]);
                    pict[coordCell].Width = 40;
                    pict[coordCell].Height = 40;
                    pict[coordCell].Location = new Point(picture.Location.X + x * 40, picture.Location.Y + y * 40);
                    pict[coordCell].BringToFront();
                }

                if (((arrayStatus[i] == 41) || (arrayStatus[i] == 31) || (arrayStatus[i] == 21) || (arrayStatus[i] == 11)) && (arrayStatus == myFieldStatus))
                {
                    pict[coordCell].Image = Resources.FieldShip;
                }
                else if ((arrayStatus[i] == 42) || (arrayStatus[i] == 32) || (arrayStatus[i] == 22))
                {
                    pict[coordCell].Image = Resources.FieldStrikeShip;
                }
                else if ((arrayStatus[i] == 43) || (arrayStatus[i] == 33) || (arrayStatus[i] == 23) || (arrayStatus[i] == 13))
                {
                    pict[coordCell].Image = Resources.FieldFire;
                }
                else if (arrayStatus[i] == 03)
                {
                    pict[coordCell].Image = Resources.FieldStrike;
                }              
            }
        }


        void Start()  // Начало боя (все корабли уже расставлены).
        {
            if (
               (pictureBoxLinkor.Visible == false) &&
               (pictureBoxKreiser.Visible == false) &&
               (pictureBoxEsminec.Visible == false) &&
               (pictureBoxKater.Visible == false)
               )
            {
                labelMessage.Text = "Выбери поле, по которому стреляем";
                pictureBox2.Enabled = true;
            }
        }


        async void EnemyStrike()  // Ход противника.
        {
            int x1 = enemyTargetCell % 10;
            int y1 = enemyTargetCell / 10;

            switch (regimeAttack)  // Определяем, по какой клетке бить.
            {
                case "normal":  // Обычный рандомный режим (если попал мимо).
                    {
                        do
                        {
                            Random rand = new();
                            enemyTargetCell = rand.Next(100);
                        }
                        while (((myFieldStatus[enemyTargetCell] % 10 == 2) && (myFieldStatus[enemyTargetCell] > 02)) ||   
                                (myFieldStatus[enemyTargetCell] % 10 == 3));      // Чтобы нельзя было по уже битым клеткам ходить.
                        break;
                    }
                case "strikeRight":  // Пробуем атаковать вправо от удачного выстрела.
                    {
                        if (x1 < 9)  // Ограничение, чтобы нельзя было выходить за край поля.
                        {
                            if (((myFieldStatus[enemyTargetCell + 1] % 10 != 2) || (myFieldStatus[enemyTargetCell + 1] == 02)) &&
                                (myFieldStatus[enemyTargetCell + 1] % 10 != 3))      // Чтобы нельзя было по уже битым клеткам стрелять.
                            {
                                enemyTargetCell = enemyTargetCell + 1;
                            }
                            else
                            {
                                enemyTargetCell = axialCell;
                                regimeAttack = "strikeLeft";
                                goto case "strikeLeft";
                            }
                        }
                        else
                        {
                            enemyTargetCell = axialCell;
                            regimeAttack = "strikeLeft";
                            goto case "strikeLeft";
                        }

                        break;
                    }
                case "strikeLeft":  // Пробуем атаковать влево от удачного выстрела.
                    {
                        if (x1 > 0)  // Ограничение, чтобы нельзя было выходить за край поля.
                        {
                            if (((myFieldStatus[enemyTargetCell - 1] % 10 != 2) || (myFieldStatus[enemyTargetCell - 1] == 02)) &&
                                (myFieldStatus[enemyTargetCell - 1] % 10 != 3))      // Чтобы нельзя было по уже битым клеткам стрелять.
                            {
                                enemyTargetCell = enemyTargetCell - 1;
                            }
                            else
                            {
                                enemyTargetCell = axialCell;
                                regimeAttack = "strikeDown";
                                goto case "strikeDown";
                            }
                        }
                        else
                        {
                            enemyTargetCell = axialCell;
                            regimeAttack = "strikeDown";
                            goto case "strikeDown";
                        }

                        break;
                    }
                case "strikeDown":  // Пробуем атаковать вниз от удачного выстрела.
                    {
                        if (y1 < 9)  // Ограничение, чтобы нельзя было выходить за край поля.
                        {
                            if (((myFieldStatus[enemyTargetCell + 10] % 10 != 2) || (myFieldStatus[enemyTargetCell + 10] == 02)) &&
                                (myFieldStatus[enemyTargetCell + 10] % 10 != 3))      // Чтобы нельзя было по уже битым клеткам стрелять.
                            {
                                enemyTargetCell = enemyTargetCell + 10;
                            }
                            else
                            {
                                enemyTargetCell = axialCell;
                                regimeAttack = "strikeUp";
                                goto case "strikeUp";
                            }
                        }
                        else
                        {
                            enemyTargetCell = axialCell;
                            regimeAttack = "strikeUp";
                            goto case "strikeUp";
                        }

                        break;
                    }
                case "strikeUp":  // Пробуем атаковать вверх от удачного выстрела.
                    {
                        enemyTargetCell = enemyTargetCell - 10;
                        break;
                    }

                default : 
                    goto case "normal";
            }

            x1 = enemyTargetCell % 10;  // После определения цели обновляем x и y.
            y1 = enemyTargetCell / 10;

            switch (myFieldStatus[enemyTargetCell])  // Собственно последствия хода.
            {
                case 01:
                    {
                        myFieldStatus[enemyTargetCell] = 03;

                        if (regimeAttack == "strikeRight") { regimeAttack = "strikeLeft"; }  // Смена направления атаки, если промах.
                        else if (regimeAttack == "strikeLeft") { regimeAttack = "strikeDown"; }
                        else if (regimeAttack == "strikeDown") { regimeAttack = "strikeUp"; }

                        enemyTargetCell = axialCell;  // Возвращаемся к первому удачному попаданию по кораблю.

                        ShowShips(pictureBox1, myPic2, myFieldStatus);
                        str = Properties.Resources._111;
                        snd = new System.Media.SoundPlayer(str);
                        snd.Play();
                        await Task.Delay(200);
                        break;
                    }

                case 02:
                    {
                        myFieldStatus[enemyTargetCell] = 03;

                        if (regimeAttack == "strikeRight") { regimeAttack = "strikeLeft"; }  // Смена направления атаки, если промах.
                        else if (regimeAttack == "strikeLeft") { regimeAttack = "strikeDown"; }
                        else if (regimeAttack == "strikeDown") { regimeAttack = "strikeUp"; }

                        enemyTargetCell = axialCell;  // Возвращаемся к первому удачному попаданию по кораблю.

                        ShowShips(pictureBox1, myPic2, myFieldStatus);
                        str = Properties.Resources._111;
                        snd = new System.Media.SoundPlayer(str);
                        snd.Play();
                        await Task.Delay(200);
                        break;
                    }

                case 11:
                    {
                        for (int i = x1 - 1; i <= x1 + 1; i++)  // Отмечаем клетки, соседствующие с кораблем.
                        {
                            for (int j = y1 - 1; j <= y1 + 1; j++)
                            {
                                if ((i >= 0) && (i <= 9) && (j >= 0) && (j <= 9))
                                {
                                    myFieldStatus[int.Parse(string.Concat(j, i))] = 03;
                                }
                            }
                        }

                        myFieldStatus[enemyTargetCell] = 13;
                        myShipsDestroyed++;
                        

                        ShowShips(pictureBox1, myPic2, myFieldStatus);
                        str = Properties.Resources._333;
                        snd = new System.Media.SoundPlayer(str);
                        snd.Play();
                        Win();
                        await Task.Delay(200);
                        EnemyStrike();
                        break;
                    }

                case 21:
                    {
                        for (int i = 0; i < 3; i++)  // Проверка на то, какой это эсминец.
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                if (myEsmineces[i].fields[j] == enemyTargetCell)
                                {
                                    myEsmineces[i].lives--;

                                    if (myEsmineces[i].lives > 0)  // Если ранен.
                                    {
                                        myFieldStatus[enemyTargetCell] = 22;

                                        if (regimeAttack == "normal")  // Включается "умный" режим у противника.
                                        {
                                            regimeAttack = "strikeRight";  
                                        }                                        

                                        if (firstStrike == true)  // Запоминаем первый удачный выстрел (осевую клетку).
                                        {
                                            axialCell = enemyTargetCell;
                                            firstStrike = false;
                                        }

                                        str = Properties.Resources._222;
                                    }
                                    else                           // Если убит.
                                    {
                                        for (int k = 0; k < 2; k++)  // Отмечаем все клетки корабля.
                                        {
                                            myFieldStatus[myEsmineces[i].fields[k]] = 23;
                                        }
                                        for (int m = 0; m < 10; m++)  // Отмечаем все клетки вокруг корабля.
                                        {
                                            if (myEsmineces[i].aroundFields[m] != 100)
                                            {
                                                myFieldStatus[myEsmineces[i].aroundFields[m]] = 03;
                                            }
                                        }

                                        regimeAttack = "normal";  // Противник переключается в рандомный режим атаки.
                                        firstStrike = true;

                                        myShipsDestroyed++;
                                        str = Properties.Resources._333;
                                    }
                                    break;
                                }
                            }
                        }

                        snd = new System.Media.SoundPlayer(str);
                        snd.Play();
                        ShowShips(pictureBox1, myPic2, myFieldStatus);
                        Win();
                        await Task.Delay(200);
                        EnemyStrike();
                        break;
                    }

                case 31:
                    {
                        for (int i = 0; i < 2; i++)  // Проверка на то, какой это крейсер.
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                if (myKreisers[i].fields[j] == enemyTargetCell)
                                {
                                    myKreisers[i].lives--;

                                    if (myKreisers[i].lives > 0)  // Если ранен.
                                    {
                                        myFieldStatus[enemyTargetCell] = 32;

                                        if (regimeAttack == "normal")  // Включается "умный" режим у противника.
                                        {
                                            regimeAttack = "strikeRight";
                                        }

                                        if (firstStrike == true)  // Запоминаем первый удачный выстрел (осевую клетку).
                                        {
                                            axialCell = enemyTargetCell;
                                            firstStrike = false;
                                        }

                                        str = Properties.Resources._222;
                                    }
                                    else                           // Если убит.
                                    {
                                        for (int k = 0; k < 3; k++)  // Отмечаем все клетки корабля.
                                        {
                                            myFieldStatus[myKreisers[i].fields[k]] = 33;
                                        }
                                        for (int m = 0; m < 12; m++)  // Отмечаем все клетки вокруг корабля.
                                        {
                                            if (myKreisers[i].aroundFields[m] != 100)
                                            {
                                                myFieldStatus[myKreisers[i].aroundFields[m]] = 03;
                                            }
                                        }

                                        regimeAttack = "normal";  // Противник переключается в рандомный режим атаки.
                                        firstStrike = true;

                                        myShipsDestroyed++;
                                        str = Properties.Resources._333;
                                    }
                                    break;
                                }
                            }
                        }

                        snd = new System.Media.SoundPlayer(str);
                        snd.Play();
                        ShowShips(pictureBox1, myPic2, myFieldStatus);
                        Win();
                        await Task.Delay(200);
                        EnemyStrike();
                        break;
                    }

                case 41:
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (myLinkor1.fields[j] == enemyTargetCell)
                            {
                                myLinkor1.lives--;

                                if (myLinkor1.lives > 0)  // Если ранен.
                                {
                                    myFieldStatus[enemyTargetCell] = 42;

                                    if (regimeAttack == "normal")  // Включается "умный" режим у противника.
                                    {
                                        regimeAttack = "strikeRight";
                                    }

                                    if (firstStrike == true)  // Запоминаем первый удачный выстрел (осевую клетку).
                                    {
                                        axialCell = enemyTargetCell;
                                        firstStrike = false;
                                    }

                                    str = Properties.Resources._222;
                                }
                                else                           // Если убит.
                                {
                                    for (int k = 0; k < 4; k++)  // Отмечаем все клетки корабля.
                                    {
                                        myFieldStatus[myLinkor1.fields[k]] = 43;
                                    }
                                    for (int m = 0; m < 14; m++)  // Отмечаем все клетки вокруг корабля.
                                    {
                                        if (myLinkor1.aroundFields[m] != 100)
                                        {
                                            myFieldStatus[myLinkor1.aroundFields[m]] = 03;
                                        }
                                    }

                                    regimeAttack = "normal";  // Противник переключается в рандомный режим атаки.
                                    firstStrike = true;

                                    myShipsDestroyed++;
                                    str = Properties.Resources._333;
                                }
                                break;
                            }
                        }

                        snd = new System.Media.SoundPlayer(str);
                        snd.Play();
                        ShowShips(pictureBox1, myPic2, myFieldStatus);
                        Win();
                        await Task.Delay(200);
                        EnemyStrike();
                        break;
                    }
            }
        }


        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)  // Стреляем по вражескому полю.
        {
            int x1 = int.Parse(((e.X - 1) / 40).ToString());  // Определяем координаты поля, по которому мы попали.
            int y1 = int.Parse(((e.Y - 1) / 40).ToString());
            int coordCell = int.Parse(y1.ToString() + x1.ToString());
             
            switch (fieldStatus[coordCell])
            {
                case 01:
                    {
                        fieldStatus[coordCell] = 03;
                        EnemyStrike();
                        break;
                    }

                case 02:
                    {
                        fieldStatus[coordCell] = 03;
                        EnemyStrike();
                        break;
                    }

                case 11:
                    {
                        for (int i = x1 - 1; i <= x1 + 1; i++)  // Отмечаем клетки, соседствующие с кораблем.
                        {
                            for (int j = y1 - 1; j <= y1 + 1; j++)
                            {
                                if ((i >= 0) && (i <= 9) && (j >= 0) && (j <= 9))
                                {
                                    fieldStatus[int.Parse(string.Concat(j, i))] = 03;
                                }
                            }
                        }

                        fieldStatus[coordCell] = 13;
                        shipsDestroyed++;
                        str = Properties.Resources._333;
                        snd = new System.Media.SoundPlayer(str);
                        snd.Play();
                        break;
                    }

                case 21:
                    {                        
                        for (int i = 0; i < 3; i++)  // Проверка на то, какой это эсминец.
                        {
                            for (int j = 0; j < 2; j++) 
                            {
                                if (esmineces[i].fields[j] == coordCell)
                                {
                                    esmineces[i].lives--;

                                    if (esmineces[i].lives > 0)  // Если ранен.
                                    {
                                        fieldStatus[coordCell] = 22;
                                        str = Properties.Resources._222;
                                    }
                                    else                           // Если убит.
                                    {
                                        for (int k = 0; k < 2; k++)  // Отмечаем все клетки корабля.
                                        {
                                            fieldStatus[esmineces[i].fields[k]] = 23;
                                        }
                                        for (int m = 0; m < 10; m++)  // Отмечаем все клетки вокруг корабля.
                                        {
                                            if (esmineces[i].aroundFields[m] != 100)
                                            {
                                                fieldStatus[esmineces[i].aroundFields[m]] = 03;
                                            }
                                        }

                                        shipsDestroyed++;
                                        str = Properties.Resources._333;
                                    }
                                
                                    break;
                                }
                            }
                        }

                        snd = new System.Media.SoundPlayer(str);
                        snd.Play();
                        break;
                    }

                case 31:
                    {
                        for (int i = 0; i < 2; i++)  // Проверка на то, какой это крейсер.
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                if (kreisers[i].fields[j] == coordCell)
                                {
                                    kreisers[i].lives--;

                                    if (kreisers[i].lives > 0)  // Если ранен.
                                    {
                                        fieldStatus[coordCell] = 32;
                                        str = Properties.Resources._222;
                                    }
                                    else                           // Если убит.
                                    {
                                        for (int k = 0; k < 3; k++)  // Отмечаем все клетки корабля.
                                        {
                                            fieldStatus[kreisers[i].fields[k]] = 33;
                                        }
                                        for (int m = 0; m < 12; m++)  // Отмечаем все клетки вокруг корабля.
                                        {
                                            if (kreisers[i].aroundFields[m] != 100)
                                            {
                                                fieldStatus[kreisers[i].aroundFields[m]] = 03;
                                            }
                                        }

                                        shipsDestroyed++;
                                        str = Properties.Resources._333;
                                    }
                                    
                                    break;
                                }
                            }
                        }

                        snd = new System.Media.SoundPlayer(str);
                        snd.Play();
                        break;
                    }

                case 41:                                        
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (linkor1.fields[j] == coordCell)
                            {
                                linkor1.lives--;

                                if (linkor1.lives > 0)  // Если ранен.
                                {
                                    fieldStatus[coordCell] = 42;
                                    str = Properties.Resources._222;
                                }
                                else                           // Если убит.
                                {
                                    for (int k = 0; k < 4; k++)  // Отмечаем все клетки корабля.
                                    {
                                        fieldStatus[linkor1.fields[k]] = 43;
                                    }
                                    for (int m = 0; m < 14; m++)  // Отмечаем все клетки вокруг корабля.
                                    {
                                        if (linkor1.aroundFields[m] != 100)
                                        {
                                            fieldStatus[linkor1.aroundFields[m]] = 03;
                                        }
                                    }

                                    shipsDestroyed++;
                                    str = Properties.Resources._333;
                                }
                                
                                break;
                            }
                        }

                        snd = new System.Media.SoundPlayer(str);
                        snd.Play();
                        break;
                    }
            }

            ShowShips(pictureBox2, pic2, fieldStatus);  // Проявляем на форме статусы полей.
            Win();
        }


        private void pictureBoxLinkor_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;  // Фиксируем координаты нажатия.
            y = e.Y;

            if (e.Button.ToString() == "Right") 
            {
                if (pictureBoxLinkor.Width == 31)
                {
                    pictureBoxLinkor.Height = 31;
                    pictureBoxLinkor.Width = 121;
                    pictureBoxLinkor.Location = new Point(pictureBoxLinkor.Location.X - 45, pictureBoxLinkor.Location.Y + 45);
                    pictureBoxLinkor.BackgroundImage = Resources.LinkorHor;
                }
                else
                {
                    pictureBoxLinkor.Height = 121;
                    pictureBoxLinkor.Width = 31;
                    pictureBoxLinkor.Location = new Point(pictureBoxLinkor.Location.X + 45, pictureBoxLinkor.Location.Y - 45);
                    pictureBoxLinkor.BackgroundImage = Resources.LinkorVert;
                }

                this.Controls.Remove(pic);
            }

            if (e.Button.ToString() == "Left")
            {
                dragging = true;
                
                this.Controls.Add(pic);

                if (pictureBoxLinkor.Width == 31)
                {
                    pic.Width = 41;
                    pic.Height = 161;
                    pic.Image = Resources.LinkorVert;
                }
                else
                {
                    pic.Width = 161;
                    pic.Height = 41;
                    pic.Image = Resources.LinkorHor;
                }

                pic.Location = new Point(pictureBoxLinkor.Location.X + x, pictureBoxLinkor.Location.Y + y); 
                pic.BringToFront();
            }
        }


        private void pictureBoxKreiser_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;  // Фиксируем координаты нажатия.
            y = e.Y;

            if (e.Button.ToString() == "Right")
            {
                if (pictureBoxKreiser.Width == 31)
                {
                    pictureBoxKreiser.Height = 31;
                    pictureBoxKreiser.Width = 91;
                    pictureBoxKreiser.Location = new Point(pictureBoxKreiser.Location.X - 30, pictureBoxKreiser.Location.Y + 30);
                    pictureBoxKreiser.BackgroundImage = Resources.KreiserHor;
                }
                else
                {
                    pictureBoxKreiser.Height = 91;
                    pictureBoxKreiser.Width = 31;
                    pictureBoxKreiser.Location = new Point(pictureBoxKreiser.Location.X + 30, pictureBoxKreiser.Location.Y - 30);
                    pictureBoxKreiser.BackgroundImage = Resources.KreiserVert;
                }

                this.Controls.Remove(pic);
            }

            if (e.Button.ToString() == "Left")
            {
                dragging = true;

                this.Controls.Add(pic);

                if (pictureBoxKreiser.Width == 31)
                {
                    pic.Width = 41;
                    pic.Height = 121;
                    pic.Image = Resources.KreiserVert;
                }
                else
                {
                    pic.Width = 121;
                    pic.Height = 41;
                    pic.Image = Resources.KreiserHor;
                }

                pic.Location = new Point(pictureBoxKreiser.Location.X + x, pictureBoxKreiser.Location.Y + y);
                pic.BringToFront();
            }
        }


        private void pictureBoxEsminec_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;  // Фиксируем координаты нажатия.
            y = e.Y;

            if (e.Button.ToString() == "Right")
            {
                if (pictureBoxEsminec.Width == 31)
                {
                    pictureBoxEsminec.Height = 31;
                    pictureBoxEsminec.Width = 61;
                    pictureBoxEsminec.Location = new Point(pictureBoxEsminec.Location.X - 15, pictureBoxEsminec.Location.Y + 15);
                    pictureBoxEsminec.BackgroundImage = Resources.EsminecHor;
                }
                else
                {
                    pictureBoxEsminec.Height = 61;
                    pictureBoxEsminec.Width = 31;
                    pictureBoxEsminec.Location = new Point(pictureBoxEsminec.Location.X + 15, pictureBoxEsminec.Location.Y - 15);
                    pictureBoxEsminec.BackgroundImage = Resources.EsminecVert;
                }

                this.Controls.Remove(pic);
            }

            if (e.Button.ToString() == "Left")
            {
                dragging = true;

                this.Controls.Add(pic);

                if (pictureBoxEsminec.Width == 31)
                {
                    pic.Width = 41;
                    pic.Height = 81;
                    pic.Image = Resources.EsminecVert;
                }
                else
                {
                    pic.Width = 81;
                    pic.Height = 41;
                    pic.Image = Resources.EsminecHor;
                }

                pic.Location = new Point(pictureBoxEsminec.Location.X + x, pictureBoxEsminec.Location.Y + y);
                pic.BringToFront();
            }
        }


        private void pictureBoxKater_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;  // Фиксируем координаты нажатия.
            y = e.Y;

            if (e.Button.ToString() == "Left")
            {
                dragging = true;

                this.Controls.Add(pic);

                pic.Width = 41;
                pic.Height = 41;
                pic.Image = Resources.Kater;
                pic.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                pic.BringToFront();
            }

            if (e.Button.ToString() == "Right")
            {
                this.Controls.Remove(pic);
            }
        }


        private void Form1_MouseMove(object sender, MouseEventArgs e)  // Перетаскиваем корабли. Применяется для всех контролов.
        {
            if (dragging == true)
            {
                pic.Location = new Point(Cursor.Position.X - Location.X - 8, Cursor.Position.Y - Location.Y - 31);
                pic.BringToFront();

                x += e.X - oldex;
                y += e.Y - oldey;
                oldex = e.X;
                oldey = e.Y;
            }
        }

        
        private void pictureBoxLinkor_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button.ToString() == "Left")
            {
                int coordCell;  // Координаты, куда мы поставили корабль.
                if
                (
                    (pic.Location.X > pictureBox1.Location.X) &&
                    (pic.Location.X < pictureBox1.Location.X + 401) &&
                    (pic.Location.Y > pictureBox1.Location.Y) &&
                    (pic.Location.Y < pictureBox1.Location.Y + 401)
                )
                {
                    int x1 = int.Parse(((e.X + pictureBoxLinkor.Location.X - pictureBox1.Location.X - 1) / 40).ToString());
                    int y1 = int.Parse(((e.Y + pictureBoxLinkor.Location.Y - pictureBox1.Location.Y - 1) / 40).ToString());
                    coordCell = int.Parse(y1.ToString() + x1.ToString());

                    if ((pictureBoxLinkor.Height == 31) && ((e.X + pictureBoxLinkor.Location.X - pictureBox1.Location.X - 1) / 40 < 7))
                    {
                        if ((myFieldStatus[coordCell] == 01) && 
                            (myFieldStatus[coordCell + 1] == 01) && 
                            (myFieldStatus[coordCell + 2] == 01) && 
                            (myFieldStatus[coordCell + 3] == 01))
                        {
                            myLinkor1 = new(true);  // Фиксируем переменные корабля.
                            myLinkor1.direction = 0;
                            for (int i = 0; i < 4; i++) { myLinkor1.fields[i] = 100; }  // 100 - это битые/неиспользованные клетки.
                            for (int i = 0; i < 14; i++) { myLinkor1.aroundFields[i] = 100; }

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
                                        myFieldStatus[int.Parse(string.Concat(j, i))] = 02;
                                        
                                        myLinkor1.aroundFields[k] = int.Parse(string.Concat(j, i));  // Заполняем массив клеток вокруг корабля.
                                        k++;
                                    }
                                }
                            }

                            myFieldStatus[coordCell] = 41;  // Отмечаем клетки, где корабль.
                            myFieldStatus[coordCell + 1] = 41;
                            myFieldStatus[coordCell + 2] = 41;
                            myFieldStatus[coordCell + 3] = 41;

                            for (int i = 0; i < 4; i++)  // Заполняем массив клеток корабля.
                            {
                                myLinkor1.fields[i] = coordCell + i;
                            }

                            pictureBoxLinkor.Visible = false;
                                                        
                            snd.Play();

                            Start();
                        }
                    }
                    else if ((pictureBoxLinkor.Width == 31) && ((e.Y + pictureBoxLinkor.Location.Y - pictureBox1.Location.Y - 1) / 40 < 7))
                    {
                        if ((myFieldStatus[coordCell] == 01) &&
                            (myFieldStatus[coordCell + 10] == 01) &&
                            (myFieldStatus[coordCell + 20] == 01) &&
                            (myFieldStatus[coordCell + 30] == 01))
                        {
                            myLinkor1 = new(true);  // Фиксируем переменные корабля.
                            myLinkor1.direction = 1;
                            for (int i = 0; i < 4; i++) { myLinkor1.fields[i] = 100; }  // 100 - это битые/неиспользованные клетки.
                            for (int i = 0; i < 14; i++) { myLinkor1.aroundFields[i] = 100; }

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
                                        myFieldStatus[int.Parse(string.Concat(j, i))] = 02;

                                        myLinkor1.aroundFields[k] = int.Parse(string.Concat(j, i));  // Заполняем массив клеток вокруг корабля.
                                        k++;
                                    }
                                }                                
                            }

                            myFieldStatus[coordCell] = 41;  // Отмечаем клетки, где корабль.
                            myFieldStatus[coordCell + 10] = 41;
                            myFieldStatus[coordCell + 20] = 41;
                            myFieldStatus[coordCell + 30] = 41;

                            for (int i = 0; i < 4; i++)  // Заполняем массив клеток корабля.
                            {
                                myLinkor1.fields[i] = coordCell + i * 10;
                            }

                            pictureBoxLinkor.Visible = false;

                            snd.Play();

                            Start();
                        }
                    }

                    ShowShips(pictureBox1, myPic2, myFieldStatus);  // Проявляем корабли.
                }

                dragging = false;  // Завершаем перетаскивание.
            }

            this.Controls.Remove(pic);
        }


        private void pictureBoxKreiser_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button.ToString() == "Left")
            {
                int coordCell;  // Координаты, куда мы поставили корабль.
                if
                (
                    (pic.Location.X > pictureBox1.Location.X) &&
                    (pic.Location.X < pictureBox1.Location.X + 401) &&
                    (pic.Location.Y > pictureBox1.Location.Y) &&
                    (pic.Location.Y < pictureBox1.Location.Y + 401)
                )
                {
                    int x1 = int.Parse(((e.X + pictureBoxKreiser.Location.X - pictureBox1.Location.X - 1) / 40).ToString());
                    int y1 = int.Parse(((e.Y + pictureBoxKreiser.Location.Y - pictureBox1.Location.Y - 1) / 40).ToString());
                    coordCell = int.Parse(y1.ToString() + x1.ToString());

                    if ((pictureBoxKreiser.Height == 31) && ((e.X + pictureBoxKreiser.Location.X - pictureBox1.Location.X - 1) / 40 < 8))
                    {
                        if ((myFieldStatus[coordCell] == 01) &&
                            (myFieldStatus[coordCell + 1] == 01) &&
                            (myFieldStatus[coordCell + 2] == 01))
                        {
                            myKreisers[myKreisersInField] = new(true);  // Фиксируем переменные корабля.
                            myKreisers[myKreisersInField].direction = 0;
                            for (int i = 0; i < 3; i++) { myKreisers[myKreisersInField].fields[i] = 100; }  // 100 - это битые/неиспользованные клетки.
                            for (int i = 0; i < 12; i++) { myKreisers[myKreisersInField].aroundFields[i] = 100; }

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
                                        myFieldStatus[int.Parse(string.Concat(j, i))] = 02;

                                        myKreisers[myKreisersInField].aroundFields[k] = int.Parse(string.Concat(j, i));  // Заполняем массив клеток вокруг корабля.
                                        k++;
                                    }
                                }
                            }

                            myFieldStatus[coordCell] = 31;  // Отмечаем клетки, где корабль.
                            myFieldStatus[coordCell + 1] = 31;
                            myFieldStatus[coordCell + 2] = 31;

                            for (int i = 0; i < 3; i++)  // Заполняем массив клеток корабля.
                            {
                                myKreisers[myKreisersInField].fields[i] = coordCell + i;
                            }

                            myKreisersInField++;

                            snd.Play();
                        }
                    }
                    else if ((pictureBoxKreiser.Width == 31) && ((e.Y + pictureBoxKreiser.Location.Y - pictureBox1.Location.Y - 1) / 40 < 8))
                    {
                        if ((myFieldStatus[coordCell] == 01) &&
                            (myFieldStatus[coordCell + 10] == 01) &&
                            (myFieldStatus[coordCell + 20] == 01))
                        {
                            myKreisers[myKreisersInField] = new(true);  // Фиксируем переменные корабля.
                            myKreisers[myKreisersInField].direction = 1;
                            for (int i = 0; i < 3; i++) { myKreisers[myKreisersInField].fields[i] = 100; }  // 100 - это битые/неиспользованные клетки.
                            for (int i = 0; i < 12; i++) { myKreisers[myKreisersInField].aroundFields[i] = 100; }

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
                                        myFieldStatus[int.Parse(string.Concat(j, i))] = 02;

                                        myKreisers[myKreisersInField].aroundFields[k] = int.Parse(string.Concat(j, i));  // Заполняем массив клеток вокруг корабля.
                                        k++;
                                    }
                                }                                
                            }

                            myFieldStatus[coordCell] = 31;  // Отмечаем клетки, где корабль.
                            myFieldStatus[coordCell + 10] = 31;
                            myFieldStatus[coordCell + 20] = 31;

                            for (int i = 0; i < 3; i++)  // Заполняем массив клеток корабля.
                            {
                                myKreisers[myKreisersInField].fields[i] = coordCell + i * 10;
                            }

                            myKreisersInField++;
 
                            snd.Play();
                        }
                    }

                    if (myKreisersInField == 2) 
                    { 
                        pictureBoxKreiser.Visible = false;
                        Start();
                    }

                    ShowShips(pictureBox1, myPic2, myFieldStatus);  // Проявляем корабли.
                }

                dragging = false;  // Завершаем перетаскивание.
            }

            this.Controls.Remove(pic);
        }


        private void pictureBoxEsminec_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button.ToString() == "Left")
            {
                int coordCell;  // Координаты, куда мы поставили корабль.
                if
                (
                    (pic.Location.X > pictureBox1.Location.X) &&
                    (pic.Location.X < pictureBox1.Location.X + 401) &&
                    (pic.Location.Y > pictureBox1.Location.Y) &&
                    (pic.Location.Y < pictureBox1.Location.Y + 401)
                )
                {
                    int x1 = int.Parse(((e.X + pictureBoxEsminec.Location.X - pictureBox1.Location.X - 1) / 40).ToString());
                    int y1 = int.Parse(((e.Y + pictureBoxEsminec.Location.Y - pictureBox1.Location.Y - 1) / 40).ToString());
                    coordCell = int.Parse(y1.ToString() + x1.ToString());

                    if ((pictureBoxEsminec.Height == 31) && ((e.X + pictureBoxEsminec.Location.X - pictureBox1.Location.X - 1) / 40 < 9))
                    {
                        if ((myFieldStatus[coordCell] == 01) &&
                            (myFieldStatus[coordCell + 1] == 01))
                        {
                            myEsmineces[myEsminecesInField] = new(true);  // Фиксируем переменные корабля.
                            myEsmineces[myEsminecesInField].direction = 0;
                            for (int i = 0; i < 2; i++) { myEsmineces[myEsminecesInField].fields[i] = 100; }  // 100 - это битые/неиспользованные клетки.
                            for (int i = 0; i < 10; i++) { myEsmineces[myEsminecesInField].aroundFields[i] = 100; }

                            for (int i = x1 - 1, k = 0; i <= x1 + 2; i++)  // Отмечаем клетки, соседствующие с кораблем.
                            {
                                for (int j = y1 - 1; j <= y1 + 1; j++)
                                {
                                    if ((i >= x1) && (i <= x1 + 1) && (j == y1))  // Исключаем клетки корабля из этого цикла.
                                    {
                                        continue;
                                    }

                                    if ((i < 10) && (j < 10) && (i >= 0) && (j >= 0))  // Ограничение, чтобы координаты клетки не были -1 или больше 9.
                                    {
                                        myFieldStatus[int.Parse(string.Concat(j, i))] = 02;

                                        myEsmineces[myEsminecesInField].aroundFields[k] = int.Parse(string.Concat(j, i));  // Заполняем массив клеток вокруг корабля.
                                        k++;
                                    }
                                }
                            }

                            myFieldStatus[coordCell] = 21;  // Отмечаем клетки, где корабль.
                            myFieldStatus[coordCell + 1] = 21;

                            for (int i = 0; i < 2; i++)  // Заполняем массив клеток корабля.
                            {
                                myEsmineces[myEsminecesInField].fields[i] = coordCell + i;
                            }

                            myEsminecesInField++;

                            snd.Play();
                        }
                    }
                    else if ((pictureBoxEsminec.Width == 31) && ((e.Y + pictureBoxEsminec.Location.Y - pictureBox1.Location.Y - 1) / 40 < 9))
                    {
                        if ((myFieldStatus[coordCell] == 01) &&
                            (myFieldStatus[coordCell + 10] == 01))
                        {
                            myEsmineces[myEsminecesInField] = new(true);  // Фиксируем переменные корабля.
                            myEsmineces[myEsminecesInField].direction = 1;
                            for (int i = 0; i < 2; i++) { myEsmineces[myEsminecesInField].fields[i] = 100; }  // 100 - это битые/неиспользованные клетки.
                            for (int i = 0; i < 10; i++) { myEsmineces[myEsminecesInField].aroundFields[i] = 100; }

                            for (int i = x1 - 1, k = 0; i <= x1 + 1; i++)  // Отмечаем клетки, соседствующие с кораблем.
                            {
                                for (int j = y1 - 1; j <= y1 + 2; j++)
                                {
                                    if ((j >= y1) && (j <= y1 + 1) && (i == x1))  // Исключаем клетки корабля из этого цикла.
                                    {
                                        continue;
                                    }

                                    if ((i < 10) && (j < 10) && (i >= 0) && (j >= 0))  // Ограничение, чтобы координаты клетки не были -1 или больше 9.
                                    {
                                        myFieldStatus[int.Parse(string.Concat(j, i))] = 02;

                                        myEsmineces[myEsminecesInField].aroundFields[k] = int.Parse(string.Concat(j, i));  // Заполняем массив клеток вокруг корабля.
                                        k++;
                                    }
                                }
                            }

                            myFieldStatus[coordCell] = 21;  // Отмечаем клетки, где корабль.
                            myFieldStatus[coordCell + 10] = 21;

                            for (int i = 0; i < 2; i++)  // Заполняем массив клеток корабля.
                            {
                                myEsmineces[myEsminecesInField].fields[i] = coordCell + i * 10;
                            }

                            myEsminecesInField++;

                            snd.Play();
                        }
                    }

                    if (myEsminecesInField == 3) 
                    { 
                        pictureBoxEsminec.Visible = false;
                        Start();
                    }

                    ShowShips(pictureBox1, myPic2, myFieldStatus);  // Проявляем корабли.
                }

                dragging = false;  // Завершаем перетаскивание.
            }

            this.Controls.Remove(pic);
        }

        
        private void pictureBoxKater_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button.ToString() == "Left")
            {
                int coordCell;  // Координаты, куда мы поставили корабль.
                if
                (
                    (pic.Location.X > pictureBox1.Location.X) &&
                    (pic.Location.X < pictureBox1.Location.X + 401) &&
                    (pic.Location.Y > pictureBox1.Location.Y) &&
                    (pic.Location.Y < pictureBox1.Location.Y + 401)
                )
                {
                    int x1 = int.Parse(((e.X + pictureBoxKater.Location.X - pictureBox1.Location.X - 1) / 40).ToString());
                    int y1 = int.Parse(((e.Y + pictureBoxKater.Location.Y - pictureBox1.Location.Y - 1) / 40).ToString());
                    coordCell = int.Parse(y1.ToString() + x1.ToString());
                                        
                    if (myFieldStatus[coordCell] == 01)
                    {
                        for (int i = x1 - 1; i <= x1 + 1; i++)  // Отмечаем клетки, соседствующие с кораблем.
                        {
                            for (int j = y1 - 1; j <= y1 + 1; j++)
                            {
                                if ((i >= 0) && (i <= 9) && (j >= 0) && (j <= 9))
                                {
                                    myFieldStatus[int.Parse(string.Concat(j, i))] = 02;
                                }
                            }
                        }

                        myFieldStatus[coordCell] = 11;  // Отмечаем клетки, где корабль.

                        myKatersInField++;

                        snd.Play();
                    }
                    
                    if (myKatersInField == 4) 
                    { 
                        pictureBoxKater.Visible = false;
                        Start();
                    }

                    ShowShips(pictureBox1, myPic2, myFieldStatus);  // Проявляем корабли.
                }

                dragging = false;  // Завершаем перетаскивание.
            }

            this.Controls.Remove(pic);
        }


        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
            this.Controls.Remove(pic);
        }
    }
}