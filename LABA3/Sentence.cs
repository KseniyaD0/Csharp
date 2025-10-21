
using System.Text;

public class Sentence : IText
    {
        public List<object> Elements = new List<object>();  // слова и пунктуация

    public void AddWord(Word word)
        {
            Elements.Add(word);
        }
        
        
    
    public void AddPunctuation(Punctuation punctuation)
        {
            Elements.Add(punctuation);
        }





    public int WordCount()
    {
            int count = 0;
            foreach (var element in Elements)
            {
                if (element is Word word)
                    count++;
            }
            return count;
    }


    public int Length()
    {
        int length = 0;
        foreach (var element in Elements)
        {
            if (element is Word word)
                length += word.Length();
            else if (element is Punctuation punctuation)
                length += 1;
        }
        return length;
    }
        
        

      

        public override string ToString()
        {
            var stroka = new StringBuilder();
            foreach (var element in Elements)
            {
                if (element is Word word) { stroka.Append(word.Slovo); }
                else if (element is Punctuation punctuation) { stroka.Append(punctuation.Symbol); }
            }
            
        return stroka.ToString();
        }
    }



