using System;
using System.Collections.Generic;
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
        //читаем файл с белками
        ReadSequencesFile("sequences.0.txt");

        //список комманд
        List<string> commandResults = ReadCommandsFile("commands.0.txt");

        //создаем выходной файл
        CreateOutputFile(commandResults, "genedata.0.txt");
    }



    static void ReadSequencesFile(string filename)
    {
        try
        {
            string content = File.ReadAllText(filename);
            string[] objects = content.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            //берем 1 объект из файла
            GeneticData currentData = new GeneticData();
            int lineType = 0; // 0 = protein, 1 = organism, 2 = amino_acids

            foreach (string line in objects)
            {
                string trimmed = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmed))
                    continue;

                if (lineType == 0) // Protein name
                {
                    currentData.protein = trimmed;
                    lineType = 1;
                }
                else if (lineType == 1) // Organism
                {
                    currentData.organism = trimmed;
                    lineType = 2;
                }
                else if (lineType == 2) // amino_acids
                {
                    currentData.amino_acids = trimmed;
                    geneticData.Add(currentData);
                    currentData = new GeneticData();
                    lineType = 0;
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка чтения файла sequences:" + ex.Message);
        }
    }




    static List<string> ReadCommandsFile(string inputFilename)
    {
        List<string> commandResults = new List<string>();

        try
        {
            string[] commands = File.ReadAllLines(inputFilename);
            int commandNumber = 1;

            foreach (string command in commands)
            {
                if (string.IsNullOrWhiteSpace(command))
                    continue;

                string[] parts = command.Split('\t');
                if (parts.Length < 2)
                    continue;

                string operation = parts[0].Trim();
                string result = "";

                switch (operation)
                {
                    case "search":
                        string searchPattern = parts[1].Trim();
                        result = Search(searchPattern, commandNumber);
                        break;

                    case "diff":
                        if (parts.Length >= 3)
                        {
                            string protein1 = parts[1].Trim();
                            string protein2 = parts[2].Trim();
                            result = Diff(protein1, protein2, commandNumber);
                        }
                        break;

                    case "mode":
                        string proteinName = parts[1].Trim();
                        result = Mode(proteinName, commandNumber);
                        break;
                }

                if (!string.IsNullOrEmpty(result))
                {
                    commandResults.Add(result);
                    commandNumber++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка чтения commands:" + ex.Message);
        }

        return commandResults;
    }




    static void CreateOutputFile(List<string> commandResults, string outputFilename)
    {
        try
        {
            List<string> output = new List<string>();
            output.Add("DK");
            output.Add("Genetic Searching");
            output.Add("--------------------------------------------------------------------------");

            foreach (string result in commandResults)
            {
                output.Add(result);
                output.Add("--------------------------------------------------------------------------");
            }


            File.WriteAllLines(outputFilename, output);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка создания файла:" + ex.Message);
        }
    }



    static string Search(string pattern, int commandNumber)
    {
        string decodedPattern = Decode(pattern);
        StringBuilder result = new StringBuilder();

        result.AppendLine("00" + commandNumber + "\tsearch\t" + pattern);
        result.AppendLine("organism\t\t\tprotein ");

        bool found = false;
        foreach (var data in geneticData)
        {
            if (data.amino_acids.Contains(decodedPattern))
            {
                result.AppendLine(data.organism + "\t\t" + data.protein);
                found = true;
                break;
            }
        }

        if (!found)
        {
            result.AppendLine("NOT FOUND");
        }

        return result.ToString();
    }



    static string Diff(string protein1, string protein2, int commandNumber)
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine("00" + commandNumber + "\tdiff" + protein1 + "\t" + protein2);
        result.AppendLine("amino-acids difference: ");

        GeneticData data1 = geneticData.FirstOrDefault(p => p.protein == protein1);
        GeneticData data2 = geneticData.FirstOrDefault(p => p.protein == protein2);

        if (data1.protein == null || data2.protein == null)
        {
            result.AppendLine("PROTEIN NOT FOUND");
        }
        else if (!IsValid(data1.amino_acids) || !IsValid(data2.amino_acids))
        {
            result.AppendLine("Protein is wrong written");
        }
        else
        {
            int differences = CalculateDifferences(data1.amino_acids, data2.amino_acids);
            result.AppendLine(differences.ToString());
        }

        return result.ToString();
    }



    static string Mode(string proteinName, int commandNumber)
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine("00" + commandNumber + "\tmode\t" + proteinName);
        result.AppendLine("amino-acid occurs: ");

        GeneticData protein = geneticData.FirstOrDefault(p => p.protein == proteinName);

        if (protein.protein == null)
        {
            result.AppendLine("PROTEIN NOT FOUND");
        }
          else if (!IsValid(protein.amino_acids))
        {
            result.AppendLine("Protein is wrong written");
        }
        else
        {
            var modeResult = CalculateMode(protein.amino_acids);
            result.AppendLine(modeResult.Item1 + "\t\t\t" + modeResult.Item2);
        }

        return result.ToString();
    }




    static int CalculateDifferences(string seq1, string seq2)
    {
        int differences = 0;

        for (int i = 0; i < Math.Min(seq1.Length, seq2.Length); i++)
        {
            if (seq1[i] != seq2[i])
            {
                differences++;
            }
        }

        differences += Math.Abs(seq1.Length - seq2.Length);
        return differences;
    }





    static (char, int) CalculateMode(string sequence)
    {
        Dictionary<char, int> acidCounts = new Dictionary<char, int>();
        foreach (char a in sequence)
        {
            if (acidCounts.ContainsKey(a))
            {
                acidCounts[a]++;
            }
            else
            {
                acidCounts[a] = 1;
            }
        }

        char mostOften = ' ';
        int maxCount = 0;

        foreach (var c in acidCounts)
        {
            if (c.Value > maxCount)
            {
                mostOften = c.Key;
                maxCount = c.Value;
            }
        }
        return (mostOften, maxCount);
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





    public static bool IsValid(string acid)
    {
        char[] ValidChars = {
        'A', 'C', 'D', 'E', 'F', 'G', 'H', 'I',
        'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S',
        'T', 'V', 'W', 'Y'};
        foreach (char c in acid)
        {
            if (!ValidChars.Contains(c))
            {
                return false;
            }
        }
        return true;
    }
}
