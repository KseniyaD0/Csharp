using System;

class Program
{
    static void Main(string[] args)
    {
        string TextFromFile = " - Что такое переменная. Прежде чем перейти к рассмотрению потребности в" +
                             " использовании переменных в языке программирования, сделаем небольшое отступление" +
                             " и рассмотрим, как компьютер хранит и обрабатывает данные.";
        Realization realization = new Realization();
        Text text = realization.Tokenization(TextFromFile);


        Console.WriteLine("Всего предложений: " + text.Sentences.Count + "\n");
    


      for (int i = 0; i < text.Sentences.Count; i++)
        {
            Console.WriteLine("Предложение " + (i + 1) + " :");
            Console.WriteLine("Полное предложение: " + text.Sentences[i] );
            Console.WriteLine("Количество слов: " + text.Sentences[i].WordCount());
            Console.WriteLine("Длина предложения: " + text.Sentences[i].Length());
            Console.WriteLine();
        }

    }
}


