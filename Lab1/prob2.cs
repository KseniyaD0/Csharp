using System;
using System.Text;

struct GeneticData
{
    public string protein;
    public string organism;
    public string amino_acids;
}




class Program
{
    static List<GeneticData> geneticData = new List<GeneticData>();

    static void Main()
    {
        AddGeneticData();

        Console.WriteLine(" SEARCH: ");  //поиск куска цепочки среди всех цепочек
        Search("SIIK");
        Search("PLML");
        Search("FK3I");


        Console.WriteLine("\nDIFF:");  //во сколько символов отличаются две цепочки
        Diff("6.8 kDa mitochondrial proteolipid", "Alcohol dehydrogenase");
        Diff("RNA-dependent RNA polymerase [Fragment]", "Alcohol dehydrogenase");
        Diff("Cecropin", "Pre-T/NK cell associated protein 6H9A");


        Console.WriteLine("\nMODE:"); //определить самый чаще-встречающийся символ в цепочке (декодированный варик)
        Mode("Cecropin");
        Mode("Alcohol dehydrogenase");
    }



    static void AddGeneticData()  //лист белков
    {
        geneticData.Add(new GeneticData
        {
            protein = "6.8 kDa mitochondrial proteolipid",
            organism = "Homo sapiens (Human)",
            amino_acids = "MLQSIIKNIWIPMKPYYTKVYQEIWIGMGLMGFIVYKIRAADKRSKALKASAPAPGHH"
        });

        geneticData.Add(new GeneticData
        {
            protein = "Pre-T/NK cell associated protein 6H9A",
            organism = "Homo sapiens (Human)",
            amino_acids = "MRLSCLVIITITAELCVPLMLCAHGEQAQLPRGVCVLGTGTSPAWSPVLLGRLPFPH"
        });

        geneticData.Add(new GeneticData
        {
            protein = "Alcohol dehydrogenase",
            organism = "Brachydanio rerio",
            amino_acids = "MDTTGKVIKCKAAVAWEAGKPLTIEEVEVAPPKAHEVRVKIHATGVCHTDAYTLSGSDPEGLFPVILGHEGAGTVESVGEGVTK"
        });

        geneticData.Add(new GeneticData
        {
            protein = "RNA-dependent RNA polymerase [Fragment]",
            organism = "San Miguel sea lion virus",
            amino_acids = "PSGMPLTSIINSLNHCLMVGCAVVKALEDSGVQATWNIFDSMDLFTYGDDGVYIVPPLISSVMPKVFSNLRQFGLKPTRTDKTDAEITPIPADEPVEFLKRTIVRTENGIRALLDKSSII"
        });

        geneticData.Add(new GeneticData
        {
            protein = "Cecropin",
            organism = "Bombyx mori (Silk moth)",
            amino_acids = "RWKIFKKIEKVGQNIRDGIVKAGPAVAVVGQAATI"
        });
    }



    static void Search(string proteinPart)
    {
        //декодировать кусок цепочки
        string decoded = Decode(proteinPart); 
        bool found = false;

        foreach (var k in geneticData)
        {

            if (k.amino_acids.Contains(decoded))
            {

                Console.WriteLine("\nБелок: " + k.protein);
                Console.WriteLine("Организм: " + k.organism);
                Console.WriteLine("Фрагмент: " + proteinPart);
                found = true;
            }
        }

        if (!found) //если не нашло
        {
            Console.WriteLine("\n" + proteinPart + " NOT FOUND");
        }
    }





 static void Diff(string p1, string p2)
    {
    GeneticData protein1 = geneticData.FirstOrDefault(p => p.protein == p1);
    GeneticData protein2 = geneticData.FirstOrDefault(p => p.protein == p2);

        int minLength = Math.Min(protein1.amino_acids.Length, protein2.amino_acids.Length);
        int differences = 0;

        for (int i = 0; i < minLength; i++)
        {
            if (protein1.amino_acids[i] != protein2.amino_acids[i])
            {
                differences++;
            }
        }

        differences += Math.Abs(protein1.amino_acids.Length - protein2.amino_acids.Length);

        Console.WriteLine("\nРазличия между "  + p1 + " и " + p2 +" " + differences + " аминокислот");
    }







    static void Mode(string proteinName)
    {

        GeneticData protein = geneticData.FirstOrDefault(p => p.protein == proteinName);

        //словарь где ключ - буква, значение - кол-во этой буквы в цепочке
        Dictionary<char, int> acids = new Dictionary<char, int>();
        foreach (char aminoAcid in protein.amino_acids)
        {
            if (acids.ContainsKey(aminoAcid)) //ключ по букве
            {
                acids[aminoAcid]++;
            }
            else
            {
                acids[aminoAcid] = 1;
            }
        }

        char mostOften = ' '; //буква
        int maxCount = 0;

      foreach (var c in acids) //перебрать весь словарь
    {
        if (c.Value > maxCount)
        {
            mostOften = c.Key;  //буква
            maxCount = c.Value;  //кол-во этой буквы
        }
    }


        Console.WriteLine("\nБелок:" + protein.protein);
        Console.WriteLine("Организм: " + protein.organism);
        Console.WriteLine("Наиболее частая аминокислота: " + mostOften + ", " + maxCount);

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