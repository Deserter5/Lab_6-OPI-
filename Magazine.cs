using System;
using System.Collections;
using System.Text;

namespace Lab7_Variant2
{
    public class Magazine : Edition, IRateAndCopy, IEnumerable
    {
        private Frequency frequency;
        private ArrayList editors;
        private ArrayList articles;

        public Magazine(string title, Frequency frequency, DateTime releaseDate, int circulation)
            : base(title, releaseDate, circulation)
        {
            this.frequency = frequency;
            this.editors = new ArrayList();
            this.articles = new ArrayList();
        }

        public Magazine() : base()
        {
            this.frequency = Frequency.Monthly;
            this.editors = new ArrayList();
            this.articles = new ArrayList();
        }

        public Frequency Frequency
        {
            get => frequency;
            set => frequency = value;
        }

        public ArrayList Editors
        {
            get => editors;
            set => editors = value ?? new ArrayList();
        }

        public ArrayList Articles
        {
            get => articles;
            set => articles = value ?? new ArrayList();
        }

        public double AverageRating
        {
            get
            {
                if (articles == null || articles.Count == 0)
                    return 0.0;

                double sum = 0.0;
                int count = 0;
                foreach (var item in articles)
                {
                    if (item is Article article)
                    {
                        sum += article.Rating;
                        count++;
                    }
                }
                return count > 0 ? sum / count : 0.0;
            }
        }

        public double Rating => AverageRating;

        public Edition Edition
        {
            get => new Edition(title, releaseDate, circulation);
            set
            {
                if (value != null)
                {
                    title = value.Title;
                    releaseDate = value.ReleaseDate;
                    Circulation = value.Circulation;
                }
            }
        }

        public void AddArticles(params Article[] newArticles)
        {
            if (newArticles != null)
            {
                articles.AddRange(newArticles);
            }
        }

        public void AddEditors(params Person[] newEditors)
        {
            if (newEditors != null)
            {
                editors.AddRange(newEditors);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString() + $", Періодичність: {frequency}");
            sb.AppendLine("Редактори:");
            if (editors.Count == 0)
                sb.AppendLine("  (немає редакторів)");
            else
                foreach (Person editor in editors)
                    sb.AppendLine("  - " + editor);

            sb.AppendLine("Статті:");
            if (articles.Count == 0)
                sb.AppendLine("  (немає статей)");
            else
                foreach (Article article in articles)
                    sb.AppendLine("  - " + article);

            return sb.ToString();
        }

        public virtual string ToShortString()
        {
            return $"{base.ToString()}, Періодичність: {frequency}, Середній рейтинг: {AverageRating:F2}";
        }

        public override object DeepCopy()
        {
            Magazine copy = new Magazine(title, frequency, releaseDate, circulation);

            foreach (var item in editors)
            {
                if (item is Person person)
                    copy.editors.Add(person.DeepCopy());
            }

            foreach (var item in articles)
            {
                if (item is Article article)
                    copy.articles.Add(article.DeepCopy());
            }

            return copy;
        }

        public IEnumerable GetArticlesWithRatingAbove(double minRating)
        {
            foreach (var item in articles)
            {
                if (item is Article article && article.Rating > minRating)
                {
                    yield return article;
                }
            }
        }

        public IEnumerable GetArticlesContainingTitle(string searchText)
        {
            foreach (var item in articles)
            {
                if (item is Article article &&
                    article.Title != null &&
                    article.Title.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    yield return article;
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new MagazineEnumerator(articles, editors);
        }

        public IEnumerable GetArticlesByEditors()
        {
            foreach (var item in articles)
            {
                if (item is Article article)
                {
                    foreach (Person editor in editors)
                    {
                        if (article.Author != null && article.Author.Equals(editor))
                        {
                            yield return article;
                            break;
                        }
                    }
                }
            }
        }

        public IEnumerable GetEditorsWithoutArticles()
        {
            foreach (var edItem in editors)
            {
                if (edItem is Person editor)
                {
                    bool hasArticle = false;
                    foreach (var artItem in articles)
                    {
                        if (artItem is Article article &&
                            article.Author != null &&
                            article.Author.Equals(editor))
                        {
                            hasArticle = true;
                            break;
                        }
                    }

                    if (!hasArticle)
                    {
                        yield return editor;
                    }
                }
            }
        }
    }
}