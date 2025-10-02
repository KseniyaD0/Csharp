enum GameState
{
    Start,
    End
}

class Game
{
    public int size; //размер поля
    public Player cat;
    public Player mouse;
    public GameState state;


    public Game(int size)
    {
        this.size = size; //задаем размер игрового поля
        cat = new Player("Cat");
        mouse = new Player("Mouse");
        state = GameState.Start;
    }


    public void Run()
    {
        while (state != GameState.End) 
        {
            int[] MouseCommands = { 14, -4, 10, -9, 10, -12 };
            int[] CatCommands = { 24, 0, 0, -10, 12, 0 };
            mouse.location = MouseCommands[0];
            cat.location = CatCommands[0];


            //метод игры


            if (CheckCatch(mouse, cat))
            {
                mouse.state = State.Looser;
                cat.state = State.Winner;
                state = GameState.End;
            }
            else
            {
                mouse.state = State.Winner;
                cat.state = State.Looser;
                state = GameState.End;
            }

        }
    }




    private void DoMoveCommand(char command, int step)
    {
        switch (command)
        {
            case 'M': mouse.Move(step, size); break;
            case 'C': cat.Move(step, size); break;
        }
    }

    public int CalculateDistance(Player cat, Player mouse, int size)
    {
        if (mouse.state != State.Playing || cat.state != State.Playing)
            return -1;

        int pos1 = mouse.location;
        int pos2 = cat.location;

        int distance1 = Math.Abs(pos1 - pos2);
        int distance2 = size - distance1;

        return Math.Min(distance1, distance2);
    }



    public bool CheckCatch(Player mouse, Player cat)
    {
        return (mouse.state == State.Playing && cat.state == State.Playing && mouse.location == cat.location);
                
    }
}