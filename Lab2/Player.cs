enum State
{
    Winner,
    Looser,
    Playing,
    NotInGame
}

class Player
{
    public string name; //имя игрока
    public int location; //позиция на поле
    public State state = State.NotInGame; //состояние

    public Player(string name)
    {
        this.name = name;
        this.location = -1; //не в игре
    }




    public void Move(int steps, int fieldSize)
    {
        if (state == State.Playing)
        {

            int oldPosition = location;


            int newPosition = (location + steps - 1) % fieldSize + 1;

            if (newPosition <= 0)
            {
                newPosition += fieldSize;
            }

            location = newPosition;
        }
    }
}











