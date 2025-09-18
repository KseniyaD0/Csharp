//кодирование и декадирование строк

using System;
using System.Text;

class PROB2
{
    static void Main()
    {
        string stroka = "AAAAAAAATATTTCGCTTTTCAAAAATTGTCAGATGAGAGAAAAAATAAAA";
        string encoded = Encode(stroka);
        string decoded = Decode(encoded);

        Console.WriteLine("\n" + stroka);
        Console.WriteLine("Закодировано: " + encoded);
        Console.WriteLine("Декодировано: " + decoded);
    }



    static string Encode(string stroka)
    {
        StringBuilder result = new StringBuilder();
        int count = 1;
        char currentChar = stroka[0];

        for (int i = 1; i < stroka.Length; i++)
        {
            if (stroka[i] == currentChar)
            {
                count++;
            }
            else
            {
            
                if (count > 2)
                {
                    result.Append(count).Append(currentChar);
                }
                else
                {
                    result.Append(new string(currentChar, count));
                }
                
                currentChar = stroka[i];
                count = 1;
            }
        }

        // последний символ
        if (count > 2)
        {
            result.Append(count).Append(currentChar);
        }
        else
        {
            result.Append(new string(currentChar, count));
        }
        
        return result.ToString();
    }



    static string Decode(string stroka)
    {

        StringBuilder result = new StringBuilder();
       

        for (int i = 0; i < stroka.Length; i++)
        {
            if (char.IsDigit(stroka[i]))
            {
                int count = stroka[i] - '0';  //преобразовать из символа в число
                char bukva = stroka[i + 1];
                for (int j = 1; j < count; j++)
                {
                    result.Append(bukva);
                }
 
            }
            else
            {
                result.Append(stroka[i]);
        
            }
        }
        return result.ToString();

    }
}
