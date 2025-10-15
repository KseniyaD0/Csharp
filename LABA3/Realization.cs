using System.Text;

class Realization
{



    public Text Tokenization(string TextFromFile)
    {
        Text text = new Text();
        Sentence sentence = new Sentence();
        StringBuilder word = new StringBuilder();

        for (int i = 0; i < TextFromFile.Length; i++)
        {
            char c = TextFromFile[i];

            bool NOWInWord = false;

            //буква
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') ||
                (c >= 'а' && c <= 'я') || (c >= 'А' && c <= 'Я'))
            {
                word.Append(c);
                NOWInWord = true;
            }



            //если пробел
            else if (char.IsWhiteSpace(c))
            {
                if (NOWInWord)
                {
                    sentence.AddWord(new Word(word.ToString()));
                    word.Clear();
                    NOWInWord = false;
                }
            }



            //пунктуация
            else if ((c == ',') || (c == ';') || (c == ':') ||
                    (c == '-') || (c == '(') || (c == ')') || (c == '"'))
            {
                if (NOWInWord)
                {
                    sentence.AddWord(new Word(word.ToString()));
                    word.Clear();
                    NOWInWord = false;
                }

                sentence.AddPunctuation(new Punctuation(c));
            }

            else if ((c == '.') || (c == '!') || (c == '?'))
            {
                text.AddSentence(sentence);
                sentence = new Sentence();
            }

        }

        if (sentence.Elements.Count > 0)
        {
            text.AddSentence(sentence);
        }

        return text;
    }

}
