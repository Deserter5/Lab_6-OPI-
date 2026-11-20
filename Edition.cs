using System;

namespace Lab7_Variant2
{
    public class Edition
    {
        protected string title;
        protected DateTime releaseDate;
        protected int circulation;

        public Edition(string title, DateTime releaseDate, int circulation)
        {
            this.title = title;
            this.releaseDate = releaseDate;
            Circulation = circulation; // через властивість, щоб спрацювала перевірка
        }

        public Edition() : this("Невідоме видання", DateTime.Today, 1000)
        {
        }

        public string Title
        {
            get => title;
            set => title = value;
        }

        public DateTime ReleaseDate
        {
            get => releaseDate;
            set => releaseDate = value;
        }

        public int Circulation
        {
            get => circulation;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Помилка: тираж видання не може бути від'ємним. Допустимі значення: 0 або більше.");
                }
                circulation = value;
            }
        }

        public virtual object DeepCopy()
        {
            return new Edition(title, releaseDate, circulation);
        }

        public override bool Equals(object obj)
        {
            if (obj is Edition other)
            {
                return title == other.title &&
                       releaseDate == other.releaseDate &&
                       circulation == other.circulation;
            }
            return false;
        }

        public static bool operator ==(Edition e1, Edition e2)
        {
            if (ReferenceEquals(e1, e2)) return true;
            if (e1 is null || e2 is null) return false;
            return e1.Equals(e2);
        }

        public static bool operator !=(Edition e1, Edition e2)
        {
            return !(e1 == e2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (title?.GetHashCode() ?? 0);
                hash = hash * 23 + releaseDate.GetHashCode();
                hash = hash * 23 + circulation.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"Видання: \"{title}\", Дата виходу: {releaseDate:dd.MM.yyyy}, Тираж: {circulation}";
        }
    }
}