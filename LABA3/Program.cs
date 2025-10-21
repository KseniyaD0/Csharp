using System;


class Program
{
  static void Main(string[] args)
  {
    string TextFromFile = " - Что такое переменная? Прежде чем перейти к рассмотрению потребности в" +
                         " использовании переменных в языке программирования, сделаем небольшое отступление" +
                         " и рассмотрим, как компьютер хранит и обрабатывает данные. Приведенные ниже" +
                         "примеры помогут понять, что такое переменные. Предположим, вы пишете" +
                         "программу для умножения двух чисел, предоставляемых пользователем.";

    Realization realization = new Realization();

    Text text = realization.Tokenization(TextFromFile);

    while (true)
    {

      Console.WriteLine("\n\nMenu:");
      Console.WriteLine("1) Вывести все предложения заданного текста в порядке возрастания количества слов в предложениях;\n" +
                        "2) Вывести все предложения заданного текста в порядке возрастания длины предложения;\n" +
                        "3) Во всех вопросительных предложениях текста найти слова заданной длины;\n" +
                        "4)  Удалить из текста все слова заданной длины, начинающиеся с согласной буквы;\n" +
                        "5) В некотором предложении текста заменить слова заданной длины на указанную подстроку, длина которой может не совпадать с длиной слова.;\n" +
                        "6) Удалить стоп-слова из текста.;\n" +
                        "7) Экспортировать текстовый объект в XML-документ;\n" +
                        "8) Выйти. \n");
      Console.WriteLine("Выбор: ");
      string a = Console.ReadLine();

      if (int.TryParse(a, out int choice))
      {
        switch (choice)
        {
        case 1:
            realization.First(text);
            break;



        case 2:
            realization.Second(text);
            break;



        case 3:
            Console.WriteLine("Введите длину слова: ");
            string b = Console.ReadLine();
            if (int.TryParse(b, out int wordLength) && wordLength > 0)
            {
              realization.Third(text, wordLength);
            }
            else
            {
              Console.WriteLine("Неккоректное значение длины.");            
            }
            break;



        case 4:
            realization.Fourth();
            break;


        case 5:
            realization.Fifth();
            break;


        case 6:
            realization.Sixth();
            break;


        case 7:
            realization.Seventh();
            break;


        case 8: return;
          default: Console.WriteLine("Неверный выбор.");
            break;

        }
      }
    }          
  }
}








