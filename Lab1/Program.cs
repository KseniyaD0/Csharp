using System;
using System.Text;
using System.Text.RegularExpressions;

struct GeneticData
{
    public string protein;
    public string organism;
    public string amino_acids;
}




class PROB2
{
    static List<GeneticData> geneticData = new List<GeneticData>();

    static void Main()
    {
        AddGeneticData();

//поиск куска цепочки среди всех цепочек
        Console.WriteLine(" SEARCH: ");  
        Console.WriteLine("SIIK :" + Search("SIIK"));
        Console.WriteLine("PLML :" + Search("PLML"));
        Console.WriteLine("FK3I :" + Search("FK3I"));


//во сколько символов отличаются две цепочки
        Console.WriteLine("\nDIFF:");  

        Console.WriteLine("Белки: 6.8 kDa mitochondrial proteolipid, Alcohol dehydrogenase");
        Console.WriteLine("Цепочки отличаются в " +  Diff("6.8 kDa mitochondrial proteolipid", "Alcohol dehydrogenase"));

        Console.WriteLine("Белки: RNA-dependent RNA polymerase [Fragment], Alcohol dehydrogenase");
        Console.WriteLine("Цепочки отличаются в " +  Diff("RNA-dependent RNA polymerase [Fragment]", "Alcohol dehydrogenase"));


        Console.WriteLine("Белки: Cecropin, Pre-T/NK cell associated protein 6H9A");
        Console.WriteLine("Цепочки отличаются в " +  Diff("Cecropin", "Pre-T/NK cell associated protein 6H9A"));  



//определить самый чаще-встречающийся символ в цепочке (декодированный варик)
        Console.WriteLine("\nMODE:");
        var result = Mode("Cecropin");
        Console.WriteLine("В Cecropin: " + result.Item1 + ", " + result.Item2);
        result = Mode("Alcohol dehydrogenase");
        Console.WriteLine("В Alcohol dehydrogenase: " + result.Item1 + ", " + result.Item2);
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



static string Search(string proteinPart)
{
    // декодировать кусок цепочки
    string decoded = Decode(proteinPart);
    
    foreach (var data in geneticData)
    {
        if (data.amino_acids.Contains(decoded))
        {
            return data.protein; // Возвращаем название белка
        }
    }
    return "NOT FOUND";
}




 static int Diff(string p1, string p2)
    {
    GeneticData protein1 = geneticData.FirstOrDefault(p => p.protein == p1);
    GeneticData protein2 = geneticData.FirstOrDefault(p => p.protein == p2);

        int differences = 0;

        for (int i = 0; i < Math.Min(protein1.amino_acids.Length, protein2.amino_acids.Length); i++)
        {
            if (protein1.amino_acids[i] != protein2.amino_acids[i])
            {
                differences++;
            }
        }

        differences += Math.Abs(protein1.amino_acids.Length - protein2.amino_acids.Length);
        return differences;
    }







    static (char, int) Mode(string proteinName)
    {

        GeneticData protein = geneticData.FirstOrDefault(p => p.protein == proteinName);

        //словарь где ключ - буква, значение - кол-во этой буквы в цепочке
        Dictionary<char, int> acids = new Dictionary<char, int>();
        foreach (char a in protein.amino_acids)
        {
            if (acids.ContainsKey(a)) //ключ по букве
            {
                acids[a]++;
            }
            else
            {
                acids[a] = 1;
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

        (char, int) result = (mostOften, maxCount);
        return result; 
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