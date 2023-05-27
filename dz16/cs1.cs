using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz16
{
    class Poem
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Text { get; set; }
        public string Theme { get; set; }
    }

    class PoemCollection
    {
        private List<Poem> poems = new List<Poem>();

        public void AddPoem(Poem poem)
        {
            poems.Add(poem);
        }

        public void RemovePoem(string title)
        {
            var poem = poems.FirstOrDefault(p => p.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (poem != null)
                poems.Remove(poem);
        }

        public void UpdatePoem(string title, Poem updatedPoem)
        {
            var poem = poems.FirstOrDefault(p => p.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (poem != null)
            {
                poem.Title = updatedPoem.Title;
                poem.Author = updatedPoem.Author;
                poem.Year = updatedPoem.Year;
                poem.Text = updatedPoem.Text;
                poem.Theme = updatedPoem.Theme;
            }
        }

        public Poem FindPoemByTitle(string title)
        {
            return poems.FirstOrDefault(p => p.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public List<Poem> FindPoemsByAuthor(string author)
        {
            return poems.Where(p => p.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Poem> FindPoemsByYear(int year)
        {
            return poems.Where(p => p.Year == year).ToList();
        }

        public List<Poem> FindPoemsByTheme(string theme)
        {
            return poems.Where(p => p.Theme.Equals(theme, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Poem> GetPoems()
        {
            return poems;
        }

        public void SaveToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var poem in poems)
                {
                    writer.WriteLine($"{poem.Title},{poem.Author},{poem.Year},{poem.Theme}");
                    writer.WriteLine(poem.Text);
                }
            }
        }

        public void LoadFromFile(string filePath)
        {
            poems.Clear();

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string titleAuthorLine = reader.ReadLine();
                    string text = reader.ReadLine();

                    string[] titleAuthorData = titleAuthorLine.Split(',');
                    if (titleAuthorData.Length >= 4)
                    {
                        string title = titleAuthorData[0];
                        string author = titleAuthorData[1];
                        int year;
                        int.TryParse(titleAuthorData[2], out year);
                        string theme = titleAuthorData[3];

                        Poem poem = new Poem
                        {
                            Title = title,
                            Author = author,
                            Year = year,
                            Text = text,
                            Theme = theme
                        };

                        poems.Add(poem);
                    }
                }
            }
        }
    }
    internal class cs1
    {
        static PoemCollection poemCollection = new PoemCollection();
        public static void task_1()
        {
            ClearFiles();
            poemCollection.LoadFromFile("poems.txt");

            // Додавання віршів
            Poem poem1 = new Poem
            {
                Title = "Title 1",
                Author = "Author 1",
                Year = 2021,
                Text = "Text 1",
                Theme = "Theme 1"
            };
            poemCollection.AddPoem(poem1);

            Poem poem2 = new Poem
            {
                Title = "Title 2",
                Author = "Author 2",
                Year = 2022,
                Text = "Text 2",
                Theme = "Theme 2"
            };
            poemCollection.AddPoem(poem2);

            // Видалення віршу
            poemCollection.RemovePoem("Title 1");

            // Оновлення інформації про вірш
            Poem updatedPoem = new Poem
            {
                Title = "Title 2",
                Author = "Updated Author",
                Year = 2022,
                Text = "Updated Text",
                Theme = "Updated Theme"
            };
            poemCollection.UpdatePoem("Title 2", updatedPoem);

            // Пошук віршу за назвою
            Poem foundPoem = poemCollection.FindPoemByTitle("Title 2");
            if (foundPoem != null)
                Console.WriteLine($"Found poem: {foundPoem.Title} by {foundPoem.Author}");

            // Пошук віршів за автором
            List<Poem> poemsByAuthor = poemCollection.FindPoemsByAuthor("Updated Author");
            Console.WriteLine("Poems by author:");
            foreach (var poem in poemsByAuthor)
                Console.WriteLine($"{poem.Title} by {poem.Author}");

            // Пошук віршів за роком
            List<Poem> poemsByYear = poemCollection.FindPoemsByYear(2022);
            Console.WriteLine("Poems by year:");
            foreach (var poem in poemsByYear)
                Console.WriteLine($"{poem.Title} by {poem.Author}");

            // Пошук віршів за темою
            List<Poem> poemsByTheme = poemCollection.FindPoemsByTheme("Updated Theme");
            Console.WriteLine("Poems by theme:");
            foreach (var poem in poemsByTheme)
                Console.WriteLine($"{poem.Title} by {poem.Author}");

            // Збереження колекції віршів у файл
            poemCollection.SaveToFile("poems.txt");

            // Генерування звітів
            GenerateReportByTitle("report_by_title.txt");
            GenerateReportByAuthor("report_by_author.txt");
            GenerateReportByTheme("report_by_theme.txt");
            GenerateReportByKeyword("Text", "report_by_keyword.txt");
            GenerateReportByYear(2022, "report_by_year.txt");
            GenerateReportByLength(10, "report_by_length.txt");
        }
        static void ClearFiles()
        {
            File.WriteAllText("report_by_title.txt", string.Empty);
            File.WriteAllText("report_by_author.txt", string.Empty);
            File.WriteAllText("report_by_theme.txt", string.Empty);
            File.WriteAllText("report_by_keyword.txt", string.Empty);
            File.WriteAllText("report_by_year.txt", string.Empty);
            File.WriteAllText("report_by_length.txt", string.Empty);
        }

        static void GenerateReportByTitle(string fileName)
        {
            List<string> report = new List<string>();
            report.Add("Report by Title:");

            foreach (var poem in poemCollection.GetPoems())
            {
                report.Add($"Title: {poem.Title}");
                report.Add($"Author: {poem.Author}");
                report.Add($"Year: {poem.Year}");
                report.Add($"Text: {poem.Text}");
                report.Add($"Theme: {poem.Theme}");
                report.Add("");
            }

            GenerateReport(report, fileName);
        }

        static void GenerateReportByAuthor(string fileName)
        {
            List<string> report = new List<string>();
            report.Add("Report by Author:");

            var groupedByAuthor = poemCollection.GetPoems().GroupBy(p => p.Author);
            foreach (var group in groupedByAuthor)
            {
                report.Add($"Author: {group.Key}");
                foreach (var poem in group)
                {
                    report.Add($"Title: {poem.Title}");
                    report.Add($"Year: {poem.Year}");
                    report.Add($"Text: {poem.Text}");
                    report.Add($"Theme: {poem.Theme}");
                }
                report.Add("");
            }

            GenerateReport(report, fileName);
        }

        static void GenerateReportByTheme(string fileName)
        {
            List<string> report = new List<string>();
            report.Add("Report by Theme:");

            var groupedByTheme = poemCollection.GetPoems().GroupBy(p => p.Theme);
            foreach (var group in groupedByTheme)
            {
                report.Add($"Theme: {group.Key}");
                foreach (var poem in group)
                {
                    report.Add($"Title: {poem.Title}");
                    report.Add($"Author: {poem.Author}");
                    report.Add($"Year: {poem.Year}");
                    report.Add($"Text: {poem.Text}");
                }
                report.Add("");
            }

            GenerateReport(report, fileName);
        }

        static void GenerateReportByKeyword(string keyword, string fileName)
        {
            List<string> report = new List<string>();
            report.Add($"Report by Keyword: '{keyword}'");

            var poemsWithKeyword = poemCollection.GetPoems().Where(p => p.Text.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            foreach (var poem in poemsWithKeyword)
            {
                report.Add($"Title: {poem.Title}");
                report.Add($"Author: {poem.Author}");
                report.Add($"Year: {poem.Year}");
                report.Add($"Text: {poem.Text}");
                report.Add($"Theme: {poem.Theme}");
                report.Add("");
            }

            GenerateReport(report, fileName);
        }

        static void GenerateReportByYear(int year, string fileName)
        {
            List<string> report = new List<string>();
            report.Add($"Report by Year: {year}");

            var poemsByYear = poemCollection.GetPoems().Where(p => p.Year == year);
            foreach (var poem in poemsByYear)
            {
                report.Add($"Title: {poem.Title}");
                report.Add($"Author: {poem.Author}");
                report.Add($"Text: {poem.Text}");
                report.Add($"Theme: {poem.Theme}");
                report.Add("");
            }

            GenerateReport(report, fileName);
        }

        static void GenerateReportByLength(int length, string fileName)
        {
            List<string> report = new List<string>();
            report.Add($"Report by Length: {length}");

            var poemsByLength = poemCollection.GetPoems().Where(p => p.Text.Length >= length);
            foreach (var poem in poemsByLength)
            {
                report.Add($"Title: {poem.Title}");
                report.Add($"Author: {poem.Author}");
                report.Add($"Year: {poem.Year}");
                report.Add($"Text: {poem.Text}");
                report.Add($"Theme: {poem.Theme}");
                report.Add("");
            }

            GenerateReport(report, fileName);
        }

        static void GenerateReport(List<string> report, string fileName)
        {
            Console.WriteLine(string.Join("\n", report));
            File.WriteAllLines(fileName, report);
        }
    }

    
}
