using System;

namespace Lab7_Variant2
{
    public class Article : IRateAndCopy
    {
        public Person Author { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }

        public Article(Person author, string title, double rating)
        {
            Author = author;
            Title = title;
            Rating = rating;
        }

        public Article() : this(new Person(), "Назва статті за замовчуванням", 0.0)
        {
        }

        public override string ToString()
        {
            return $"Стаття: \"{Title}\", Автор: [{Author}], Рейтинг: {Rating:F2}";
        }

        public virtual object DeepCopy()
        {
            Person authorCopy = Author != null ? (Person)Author.DeepCopy() : null;
            return new Article(authorCopy, Title, Rating);
        }
    }
}