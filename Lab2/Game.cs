enum GameState
{
    Start,
    End
}



class Game
{
    public int size;
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





    public (char[] commands, int[] steps) ReadGameDataFromFile(string filename)
    {
        List<char> commandList = new List<char>();
        List<int> stepList = new List<int>();


        string[] lines = File.ReadAllLines(filename);



        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);



            if (parts.Length == 2)  // 'C' и 'M'
            {
                string a = parts[0].Trim();
                char command = Convert.ToChar(a);
                commandList.Add(command);


                string b = parts[1].Trim();
                int step = Convert.ToInt32(b);
                stepList.Add(step);

            }

            else if (parts.Length == 1) // 'P'
            {
                string a = parts[0].Trim();
                char command = Convert.ToChar(a);
                int step = 0;
                commandList.Add(command);
                stepList.Add(step);

            }
        }
        return (commandList.ToArray(), stepList.ToArray());
    }






    public void Run()
    {

        var (commands, steps) = ReadGameDataFromFile("1.ChaseData.txt");

        mouse.location = 1;
        cat.location = 1;

        Console.WriteLine("Поле: " + size);

        for (int turn = 0; turn < commands.Length; turn++)
        {
            Console.WriteLine("Ход " + (turn + 1) + ", Команда " + commands[turn] + ", Шаг: " + steps[turn]);

            if (commands[turn] == 'P')
            {
                int distance = CalculateDistance(cat, mouse);
                if (distance == -1)
                {
                    // Кто-то не в игре
                    if (mouse.state == State.NotInGame && cat.state == State.NotInGame)
                        Console.WriteLine("Оба игрока не в игре");
                    else if (mouse.state == State.NotInGame)
                        Console.WriteLine("Мышь не в игре");
                    else if (cat.state == State.NotInGame)
                        Console.WriteLine("Кот не в игре");
                }
                else
                {
                    Console.WriteLine("Позиции: Мышь = " + mouse.location + ", Кот = " + cat.location + ", Дистанция = " + distance);
                }
            }
            else
            {
                DoMoveCommand(commands[turn], steps[turn]);
            }

            if (CheckCatch(mouse, cat))
            {
                mouse.state = State.Looser;
                cat.state = State.Winner;
                state = GameState.End;
                Console.WriteLine("Кот поймал мышь! Игра окончена.");
                break;
            }
            Console.WriteLine();
        }

        if (state != GameState.End)
        {
            mouse.state = State.Winner;
            cat.state = State.Looser;
            state = GameState.End;
            Console.WriteLine("Мышь убежала! Игра окончена.");
        }

        Console.WriteLine("Финальные позиции -- Мышь:" + mouse.location + ", Кот:" + cat.location + "\n");
        Console.WriteLine("Дистанции -- Мышь:" + mouse.distance + ", Кот:" + cat.distance + "\n");
        Console.WriteLine("Победитель: " + (mouse.state == State.Winner ? "Мышь" : "Кот"));
    }






    private void DoMoveCommand(char command, int step)
    {
        switch (command)
        {
            case 'M':
                if (mouse.state == State.NotInGame)
                {
                    mouse.state = State.Playing;
                    Console.WriteLine("Мышь входит в игру!");
                }
                if (mouse.state == State.Playing)
                {
                    mouse.Move(step, size);
                }
                break;
            case 'C':
                if (cat.state == State.NotInGame)
                {
                    cat.state = State.Playing;
                    Console.WriteLine("Кот входит в игру!");
                }
                if (cat.state == State.Playing)
                {
                    cat.Move(step, size);
                }
                break;
        }
    }



    public int CalculateDistance(Player cat, Player mouse)
    {
        // Если кто-то не в игре
        if (mouse.state != State.Playing || cat.state != State.Playing)
        {
            return -1;
        }

        return Math.Abs(mouse.location - cat.location);
    }

    public bool CheckCatch(Player mouse, Player cat)
    {
        return (mouse.state == State.Playing && cat.state == State.Playing && mouse.location == cat.location);
    }
}
