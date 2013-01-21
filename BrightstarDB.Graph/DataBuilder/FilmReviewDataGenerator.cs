using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightstarDB.Graph.DataBuilder
{
    public class FilmReviewDataGenerator
    {
        private string _dataDirectory;
        private string _connectionString;

        public FilmReviewDataGenerator(string dataDirectory)
        {
            _dataDirectory = dataDirectory;
        }

        private static string UriPrefix = "http://brightstardb.com/samples/movielens/";

        public void CreateData()
        {
            try
            {
                using (var sw = new StreamWriter(new FileStream(_dataDirectory + "\\" + "movielens.rdf", FileMode.Create)))
                {
                    // process users - u.data
                    string line;
                    using (var sr = new StreamReader(new FileStream(_dataDirectory + "\\" + "u.data", FileMode.Open)))
                    {
                        line = sr.ReadLine();
                        while (line != null && !string.IsNullOrEmpty(line))
                        {                            
                            var columns = line.Split('\t');
                            var ratingId = Guid.NewGuid();
                            sw.WriteLine("<" + UriPrefix + "rating/" + ratingId + "> <" + UriPrefix + "model/type> <" + UriPrefix + "Rating> . ");
                            sw.WriteLine("<" + UriPrefix + "rating/" + ratingId + "> <" + UriPrefix + "model/user> <" + UriPrefix + "user/" + columns[0] + "> .");
                            sw.WriteLine("<" + UriPrefix + "rating/" + ratingId + "> <" + UriPrefix + "model/value> \"" + columns[2] + "\" .");
                            sw.WriteLine("<" + UriPrefix + "rating/" + ratingId + "> <" + UriPrefix + "model/timestamp> \"" + columns[2] + "\" .");
                            sw.WriteLine("<" + UriPrefix + "rating/" + ratingId + "> <" + UriPrefix + "model/item> <" + UriPrefix + "item/" + columns[1] + "> .");
                            line = sr.ReadLine();
                        }
                    }

                    // process items - u.item
                    using (var sr = new StreamReader(new FileStream(_dataDirectory + "\\" + "u.item", FileMode.Open)))
                    {
                        line = sr.ReadLine();
                        while (line != null && !string.IsNullOrEmpty(line))
                        {
                            var columns = line.Split('|');
                            sw.WriteLine("<" + UriPrefix + "movie/" + columns[0] + "> <" + UriPrefix + "model/type> <" + UriPrefix + "Movie> . ");
                            sw.WriteLine("<" + UriPrefix + "movie/" + columns[0] + "> <" + UriPrefix + "model/title> \"" + columns[1] + "\" .");
                            sw.WriteLine("<" + UriPrefix + "movie/" + columns[0] + "> <" + UriPrefix + "model/releasedate> \"" + columns[2] + "\" .");
                            sw.WriteLine("<" + UriPrefix + "movie/" + columns[0] + "> <" + UriPrefix + "model/videoreleasedate> \"" + columns[3] + "\" .");
                            sw.WriteLine("<" + UriPrefix + "movie/" + columns[0] + "> <" + UriPrefix + "model/imdburl> \"" + columns[4] + "\" .");

                            for (int i = 0; i < 19; i++)
                            {
                                var mark = columns[i + 5];
                                if (mark.Equals("1"))
                                {
                                    sw.WriteLine("<" + UriPrefix + "movie/" + columns[0] + "> <" + UriPrefix + "model/genre> <" + UriPrefix + "genre/" + i + "> . ");
                                }
                            }
                            line = sr.ReadLine();
                        }
                    }

                    // process genres - u.genre
                    using (var sr = new StreamReader(new FileStream(_dataDirectory + "\\" + "u.genre", FileMode.Open)))
                    {
                        line = sr.ReadLine();
                        while (line != null && !string.IsNullOrEmpty(line))
                        {
                            var columns = line.Split('|');
                            sw.WriteLine("<" + UriPrefix + "genre/" + columns[1] + "> <" + UriPrefix + "model/type> <" + UriPrefix + "Genre> . ");
                            sw.WriteLine("<" + UriPrefix + "genre/" + columns[1] + "> <" + UriPrefix + "model/title> \"" + columns[0] + "\" .");
                            line = sr.ReadLine();
                        }
                    }

                    // process users - u.user
                    using (var sr = new StreamReader(new FileStream(_dataDirectory + "\\" + "u.user", FileMode.Open)))
                    {
                        line = sr.ReadLine();
                        while (line != null && !string.IsNullOrEmpty(line))
                        {
                            var columns = line.Split('|');
                            sw.WriteLine("<" + UriPrefix + "user/" + columns[0] + "> <" + UriPrefix + "model/type> <" + UriPrefix + "User> . ");
                            sw.WriteLine("<" + UriPrefix + "user/" + columns[0] + "> <" + UriPrefix + "model/age> \"" + columns[1] + "\" .");
                            sw.WriteLine("<" + UriPrefix + "user/" + columns[0] + "> <" + UriPrefix + "model/gender> \"" + columns[2] + "\" .");
                            sw.WriteLine("<" + UriPrefix + "user/" + columns[0] + "> <" + UriPrefix + "model/occupation> <" + UriPrefix + "occupation/" + columns[3] + "> .");
                            sw.WriteLine("<" + UriPrefix + "user/" + columns[0] + "> <" + UriPrefix + "model/zipcode> \"" + columns[4] + "\" .");
                            line = sr.ReadLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
