using System;
using System.Collections;

namespace Lab7_Variant2
{
    public class MagazineEnumerator : IEnumerator
    {
        private readonly ArrayList articles;
        private readonly ArrayList editors;
        private int position = -1;

        public MagazineEnumerator(ArrayList articles, ArrayList editors)
        {
            this.articles = articles;
            this.editors = editors;
        }

        public object Current
        {
            get
            {
                if (position < 0 || position >= articles.Count)
                    throw new InvalidOperationException();
                return articles[position];
            }
        }

        public bool MoveNext()
        {
            while (++position < articles.Count)
            {
                if (articles[position] is Article article)
                {
                    bool isEditor = false;
                    foreach (Person editor in editors)
                    {
                        if (article.Author != null && article.Author.Equals(editor))
                        {
                            isEditor = true;
                            break;
                        }
                    }

                    if (!isEditor)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Reset()
        {
            position = -1;
        }
    }
}