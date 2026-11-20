using System;
using System.Text;

namespace Lab7_Variant2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("1. Порівняння двох об'єктів Edition:");
            Edition ed1 = new Edition("Науковий вісник", new DateTime(2024, 5, 10), 5000);
            Edition ed2 = new Edition("Науковий вісник", new DateTime(2024, 5, 10), 5000);

            Console.WriteLine($"Посилання рівні (ReferenceEquals): {ReferenceEquals(ed1, ed2)}");
            Console.WriteLine($"Об'єкти рівні (ed1 == ed2): {ed1 == ed2}");
            Console.WriteLine($"Об'єкти рівні (ed1.Equals(ed2)): {ed1.Equals(ed2)}");
            Console.WriteLine($"HashCode ed1: {ed1.GetHashCode()}");
            Console.WriteLine($"HashCode ed2: {ed2.GetHashCode()}");
            Console.WriteLine();

            Console.WriteLine("2. Перевірка виключення для тиражу:");
            try
            {
                ed1.Circulation = -100;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Перехоплено виключення: " + ex.Message);
            }
            Console.WriteLine();

            Console.WriteLine("3. Дані об'єкта Magazine:");
            Person p1 = new Person("Олександр", "Коваленко", new DateTime(1985, 3, 12));
            Person p2 = new Person("Марія", "Шевченко", new DateTime(1990, 7, 25));
            Person p3 = new Person("Тарас", "Бондар", new DateTime(1995, 11, 2));
            Person p4 = new Person("Ірина", "Мельник", new DateTime(1988, 1, 19));

            Magazine mag = new Magazine("IT Світ", Frequency.Monthly, new DateTime(2024, 10, 1), 15000);
            mag.AddEditors(p1, p2, p4);
            mag.AddArticles(
                new Article(p1, "Основи C# та ООП", 4.8),
                new Article(p2, "Штучний інтелект у 2024", 4.2),
                new Article(p3, "Хмарні технології на практиці", 3.5),
                new Article(p3, "Розробка ігор на C#", 4.9)
            );
            Console.WriteLine(mag.ToString());

            Console.WriteLine("4. Властивість Edition для Magazine:");
            Console.WriteLine(mag.Edition.ToString());
            Console.WriteLine();

            Console.WriteLine("5. Перевірка DeepCopy():");
            Magazine magCopy = (Magazine)mag.DeepCopy();

            mag.Title = "ЗМІНЕНА НАЗВА ЖУРНАЛУ";
            mag.Circulation = 999;
            ((Person)mag.Editors[0]).FirstName = "ЗМІНЕНО_ІМ'Я";
            ((Article)mag.Articles[0]).Title = "ЗМІНЕНА_СТАТТЯ";
            ((Article)mag.Articles[0]).Rating = 1.0;

            Console.WriteLine("Вихідний об'єкт (після змін):");
            Console.WriteLine(mag.ToString());

            Console.WriteLine("Повна копія (повинна залишитися без змін):");
            Console.WriteLine(magCopy.ToString());

            Console.WriteLine("6. Статті з рейтингом > 4.0 (у копії):");
            foreach (Article art in magCopy.GetArticlesWithRatingAbove(4.0))
            {
                Console.WriteLine(art);
            }
            Console.WriteLine();

            Console.WriteLine("7. Статті, у назві яких є \"C#\" (у копії):");
            foreach (Article art in magCopy.GetArticlesContainingTitle("C#"))
            {
                Console.WriteLine(art);
            }
            Console.WriteLine();

            Console.WriteLine("Додатково 1. foreach по Magazine (статті, автори яких НЕ редактори):");
            foreach (Article art in magCopy)
            {
                Console.WriteLine(art);
            }
            Console.WriteLine();

            Console.WriteLine("Додатково 2. Статті, автори яких Є редакторами:");
            foreach (Article art in magCopy.GetArticlesByEditors())
            {
                Console.WriteLine(art);
            }
            Console.WriteLine();

            Console.WriteLine("Додатково 3. Редактори, які НЕ мають статей у журналі:");
            foreach (Person ed in magCopy.GetEditorsWithoutArticles())
            {
                Console.WriteLine(ed);
            }

            Console.ReadKey();
        }
    }
}