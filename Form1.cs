using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace GutenbergBookViewer
{
    public partial class Form1 : Form
    {
        private const string TopBooksUrl = "https://www.gutenberg.org/browse/scores/top";
        private Dictionary<string, string> bookUrls = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();
            LoadPopularBooks();
        }

        private async void LoadPopularBooks()
        {
            try
            {
                await FetchPopularBooksAsync();
                listBoxBooks.DataSource = new List<string>(bookUrls.Keys);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке списка книг: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task FetchPopularBooksAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(TopBooksUrl);
                response.EnsureSuccessStatusCode();

                string html = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(html))
                {
                    throw new Exception("HTML-контент пустой.");
                }

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);

                var bookNodes = doc.DocumentNode.SelectNodes("//li[contains(@class, 'booklink')]");

                if (bookNodes == null)
                {
                    throw new Exception("Не удалось найти узлы с книгами.");
                }

                foreach (var bookNode in bookNodes)
                {
                    var titleNode = bookNode.SelectSingleNode(".//a[@class='link']");
                    var urlNode = bookNode.SelectSingleNode(".//a[@class='link']");

                    if (titleNode != null && urlNode != null)
                    {
                        string title = titleNode.InnerText.Trim();
                        string url = "https://www.gutenberg.org" + urlNode.GetAttributeValue("href", string.Empty);
                        bookUrls[title] = url;
                    }
                }
            }
        }

        private async void listBoxBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxBooks.SelectedItem != null)
            {
                string selectedBookTitle = listBoxBooks.SelectedItem.ToString();
                try
                {
                    string bookText = await FetchBookTextAsync(selectedBookTitle);
                    textBoxBookText.Text = bookText;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке текста книги: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task<string> FetchBookTextAsync(string bookTitle)
        {
            string bookPageUrl = bookUrls[bookTitle];

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(bookPageUrl);
                response.EnsureSuccessStatusCode();

                string html = await response.Content.ReadAsStringAsync();
                return ExtractBookText(html);
            }
        }

        private string ExtractBookText(string html)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            var textNode = doc.DocumentNode.SelectSingleNode("//body");

            return textNode != null ? textNode.InnerText.Trim() : "Текст не найден.";
        }
    }
}
