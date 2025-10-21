using System.Text;

public class Text : IText
{
    public List<Sentence> Sentences = new List<Sentence>();

    public void AddSentence(Sentence sentence)
    {
        Sentences.Add(sentence);
    }


    public override string ToString()
    {
        var text = new StringBuilder();
        foreach (var sentence in Sentences)
        {
            text.AppendLine(sentence.ToString());
        }
        return text.ToString();
    }


    public int Length()
    {
        int length = 0;
        foreach (var sentence in Sentences)
        {
            length += sentence.Length();
        }
        return length;
    }



    public int WordCount()
    {
        int count = 0;
        foreach (var sentence in Sentences)
        {
            count += sentence.WordCount();
        }
        return count;
    }

}

