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

        Console.WriteLine("Всего предложений: " + text.Sentences.Count);

    }
}

