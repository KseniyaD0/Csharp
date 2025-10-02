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
        this.size = size;
        cat = new Player("Cat");
        mouse = new Player("Mouse");
        state = GameState.Start;
    }


public void Run()
{

    char[] commands = { 'M', 'M', 'P', 'C', 'M', 'P', 'C', 'P', 'M', 'M', 'C', 'P', 'M', 'M', 'P', 'C', 'P', 'C', 'C', 'P', 'M', 'P' };
    int[] steps = { 7, -5, 0, 6, -7, 0, 6, 0, 4, 6, 0, 0, 0, 6, 0, -1, 0, 1, 4, 0, -4, 0 };



        //начальная позиция мыши
        for (int i = 0; i <= commands.Length; i++)
        {
            if (commands[i] == 'M')
            {
                mouse.location = steps[i];
                break;
            }
        }


        //начальная позиция кота
        for (int i = 0; i <= commands.Length; i++)
        {
            if (commands[i] == 'C')
            {
                cat.location = steps[i];
                break;
            }
        }


    mouse.state = State.Playing;
    cat.state = State.Playing;

 
    Console.WriteLine("Поле: " + size);
    Console.WriteLine("Начальные позиции - Мышь:" + mouse.location + ", Кот: " + cat.location + "\n");




        for (int turn = 1; turn < commands.Length; turn++)
        {
            Console.WriteLine("Ход " +  turn +  ", Команда " + commands[turn] + ", Шаг: " + steps[turn]);

         
            if (commands[turn] == 'P')
            {
                int distance = CalculateDistance(cat, mouse, size);
                Console.WriteLine("Позиции: Мышь = " + mouse.location + ", Кот = " + cat.location + ", Дистанция = " + distance);
            }
            else
            {
                DoMoveCommand(commands[turn], steps[turn]);
         
            }

            // Проверяем, поймана ли мышь
            if (CheckCatch(mouse, cat))
            {
                mouse.state = State.Looser;
                cat.state = State.Winner;
                state = GameState.End;
                Console.WriteLine("Кот поймал мышь! Игра закончена.");
                break;
            }
            Console.WriteLine();
        }



  
        if (state != GameState.End)
        {
            mouse.state = State.Winner;
            cat.state = State.Looser;
            state = GameState.End;
            Console.WriteLine("Мышь убежала! Игра закончена.");
        }

    // Финальные позиции
    Console.WriteLine($"Финальные позиции -- Мышь:" + mouse.location + ", Кот: " + cat.location + "\n");
    Console.WriteLine("Победитель:" + (mouse.state == State.Winner ? "Мышь" : "Кот"));
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
        {
            return -1;
        }


        int pos1 = mouse.location;
        int pos2 = cat.location;

        int distance = Math.Abs(pos1 - pos2);
        return distance;
    }



    public bool CheckCatch(Player mouse, Player cat)
    {
        return (mouse.state == State.Playing && cat.state == State.Playing && mouse.location == cat.location);          
    }
}
