enum State
{
    Winner,
    Looser,
    Playing,
    NotInGame
}

class Player
{
    public string name;
    public int location;
    public State state = State.NotInGame;

    public Player(string name)
    {
        this.name = name;
        this.location = 1;
    }

    public void Move(int steps, int fieldSize)
    {

        if (state == State.NotInGame)
        {
            state = State.Playing;
            Console.WriteLine(name + " входит в игру");
        }

      int newPosition = (location + steps - 1) % fieldSize + 1;
        location = newPosition;
    }
}











