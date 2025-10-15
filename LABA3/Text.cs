using System.Text;

public class Text
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
    }