namespace SeaBattleNew
{
    public abstract class Ship
    {
        public int direction;  // 0 - горизонтальное, 1 - вертикальное.
        public int lives;  // "Жизни" корабля, т.е. клеточки, которые он занимает.
    }
}