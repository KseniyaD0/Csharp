using System.Text;

class Realization
{


    public Text Tokenization(string TextFromFile)
    {
        Text text = new Text();
        Sentence sentence = new Sentence();
        StringBuilder word = new StringBuilder();
        bool NOWInWord = false;

        for (int i = 0; i < TextFromFile.Length; i++)
        {
            char c = TextFromFile[i];

            //буква
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') ||
                (c >= 'а' && c <= 'я') || (c >= 'А' && c <= 'Я'))
            {
                word.Append(c);
                NOWInWord = true;
            }

            //если пробел
            else if (char.IsWhiteSpace(c))
            {
                if (NOWInWord)
                {
                    sentence.AddWord(new Word(word.ToString()));
                    word.Clear();
                    NOWInWord = false;
                }
            }

            //пунктуация
            else if ((c == ',') || (c == ';') || (c == ':') ||
                    (c == '-') || (c == '(') || (c == ')') || (c == '"'))
            {
                if (NOWInWord)
                {
                    sentence.AddWord(new Word(word.ToString()));
                    word.Clear();
                    NOWInWord = false;
                }

                sentence.AddPunctuation(new Punctuation(c));
            }


            else if ((c == '.') || (c == '!') || (c == '?'))
            {
                if (NOWInWord)
                {
                    sentence.AddWord(new Word(word.ToString()));
                    word.Clear();
                    NOWInWord = false;
                }
                text.AddSentence(sentence);
                 sentence.AddPunctuation(new Punctuation(c));
                sentence = new Sentence();
                
                
            }

        }
        return text;
    }



//1

    public void First(Text text)
    {
        Console.WriteLine("\n Предложения в порядке возрастания количества слов:");
        List<Sentence> sorted = new List<Sentence>();
        sorted.AddRange(text.Sentences);


        // Сортировка пузырьком 
        for (int i = 0; i < sorted.Count - 1; i++)
        {
            for (int j = 0; j < sorted.Count - i - 1; j++)
            {
                if (sorted[j].WordCount() > sorted[j + 1].WordCount())
                {

                    Sentence abc = sorted[j];
                    sorted[j] = sorted[j + 1];
                    sorted[j + 1] = abc;
                }
            }
        }
    
    
           for (int i = 0; i < sorted.Count; i++)
        {
            var sentence = sorted[i];
            Console.WriteLine("Предложение " + (i + 1) + " :");
             Console.WriteLine("Предложение : " + sentence);
            Console.WriteLine("Количество слов: " + sentence.WordCount());
            Console.WriteLine();
        }
    }






//2

    public void Second(Text text)
    {
        Console.WriteLine("\n Предложения в порядке возрастания длины: ");
        List<Sentence> sorted = new List<Sentence>();
        sorted.AddRange(text.Sentences);
    
    for (int i = 0; i < sorted.Count - 1; i++)
        {
            for (int j = 0; j < sorted.Count - i - 1; j++)
            {
                if (sorted[j].Length() > sorted[j + 1].Length())
                {
                    Sentence abc = sorted[j];
                    sorted[j] = sorted[j + 1];
                    sorted[j + 1] = abc;
                }
            }
        }


        for (int i = 0; i < sorted.Count; i++)
        {
            var sentence = sorted[i];
            Console.WriteLine("Предложение" + (i + 1) + ":");
            Console.WriteLine("Предложение : " + sentence);
            Console.WriteLine("Длина предложения: " + sentence.Length());
            Console.WriteLine();
        }
    }



//3 
    public void Third(Text text, int wordLength)
    {
        Console.WriteLine($"\n Слова длиной " + wordLength + " в вопросительных предложениях: ");
        Console.WriteLine("Список слов: ");
        foreach (var sentence in text.Sentences)
        {
            if (IsQuestionSentence(sentence))
            {
                foreach (var element in sentence.Elements)
                {

                    if (element is IText component && component is Word && component.Length() == wordLength)
                    {
                        Console.WriteLine(component);
                    }
                }
            }
        }
    }



    private bool IsQuestionSentence(Sentence sentence)
    {
        if (sentence.Elements.Count > 0)
        {
            var lastElement = sentence.Elements[sentence.Elements.Count - 1];
            if (lastElement is Punctuation punctuation && punctuation.Symbol == '?')
            {
                return true;
            }
        }
        return false;
    }




    //4


    public void Fourth()
    {

    }



    //5

    public void Fifth()
    {

    }




    //6
    public void Sixth()
    {

    }
    


    //7
    public void Seventh()
    {

    }

}

