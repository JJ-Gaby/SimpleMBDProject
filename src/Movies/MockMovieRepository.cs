namespace SimpleMDB;
using System;
using System.Collections.Generic;

public class MockMovieRepository : IMovieRepository
{
    private List<Movie> movies;
    private int idCount;

    public MockMovieRepository()
    {
        movies = movies = new List<Movie>();
        idCount = 0;
    {
        List<Movie> movieList = new List<Movie>();

        string[] titles = {
    "The Godfather", "The Dark Knight", "Pulp Fiction", "The Shawshank Redemption", "Forrest Gump",
    "Inception", "Fight Club", "The Matrix", "The Lord of the Rings: The Return of the King", "Star Wars: A New Hope",
    "The Empire Strikes Back", "The Godfather Part II", "The Silence of the Lambs", "Schindler's List", "Se7en",
    "The Social Network", "Gladiator", "Titanic", "Interstellar", "The Green Mile",
    "Saving Private Ryan", "La La Land", "Whiplash", "Joker", "Parasite",
    "The Revenant", "The Wolf of Wall Street", "The Big Short", "Django Unchained", "The Hateful Eight",
    "Avatar", "The Truman Show", "Catch Me If You Can", "A Beautiful Mind", "The Grand Budapest Hotel",
    "The Incredibles", "Inside Out", "Up", "Toy Story", "Spirited Away",
    "Princess Mononoke", "Your Name", "Akira", "Perfect Blue", "The Lion King",
    "Frozen", "Moana", "Coco", "Encanto", "Turning Red",
    "WALL·E", "Ratatouille", "Finding Nemo", "The Shining", "Her",
    "The Prestige", "The Departed", "Shutter Island", "Gone Girl", "The Big Lebowski",
    "The Breakfast Club", "Deadpool", "Guardians of the Galaxy", "Wonder Woman", "Black Panther",
    "No Country for Old Men", "The Usual Suspects", "12 Angry Men", "The Good, the Bad and the Ugly", "The Graduate",
    "City of God", "Drive", "The Pianist", "The Witch", "The Conjuring",
    "A Quiet Place", "It Follows", "Get Out", "Midsommar", "The Babadook",
    "Arrival", "Ex Machina", "Blade Runner 2049", "Looper", "Edge of Tomorrow",
    "Gravity", "Children of Men", "District 9", "Snowpiercer", "I Am Legend",
    "World War Z", "Contagion", "1917", "Hacksaw Ridge", "The Imitation Game",
    "The Theory of Everything", "Bohemian Rhapsody", "Rocketman", "Elvis", "Oppenheimer"
};

string[] descriptions = {
    "The aging patriarch of an organized crime dynasty transfers control to his reluctant son.",
    "Batman faces the Joker, a criminal mastermind spreading chaos in Gotham.",
    "The lives of two mob hitmen intertwine in tales of violence and redemption.",
    "Two imprisoned men bond over years, finding hope and freedom.",
    "Forrest Gump, a man with a low IQ, lives an extraordinary life.",
    "A thief steals corporate secrets through dreams to plant an idea.",
    "An office worker and a soap maker form an underground fight club.",
    "A hacker learns about the reality of his world and his role in it.",
    "The final confrontation between good and evil in Middle-earth.",
    "Luke Skywalker joins the Rebel Alliance to fight the Empire.",
    "The Rebels face defeat while Vader reveals a shocking truth.",
    "Michael Corleone expands his crime family amid tragedy and power.",
    "An FBI trainee seeks help from a cannibalistic killer to catch another.",
    "A businessman saves over a thousand Jews during the Holocaust.",
    "Detectives hunt a serial killer who uses the seven deadly sins.",
    "A young man creates a social networking site that changes the world.",
    "A Roman general seeks vengeance against the corrupt emperor.",
    "A romance blossoms aboard the ill-fated Titanic.",
    "A father explores space to save humanity and reconnect with his daughter.",
    "A gentle man on death row forms a bond with the guards.",
    "Soldiers fight for survival during the D-Day invasion of WWII.",
    "An aspiring actress and a jazz musician chase dreams in LA.",
    "A young drummer trains under a brutal music instructor.",
    "A failed comedian turns into a violent figure of chaos.",
    "A poor family infiltrates a wealthy household with tragic results.",
    "A frontiersman fights to survive and avenge his son's death.",
    "A stockbroker's rise and fall in a world of greed and excess.",
    "Financial outsiders profit from the 2008 housing collapse.",
    "A bounty hunter and his captive seek vengeance in the Old West.",
    "Strangers seek shelter during a blizzard with dark secrets.",
    "A paraplegic marine explores an alien world and its people.",
    "A man unknowingly lives his life on a reality TV show.",
    "A forger is pursued by the FBI across the globe.",
    "A brilliant mathematician battles schizophrenia and genius.",
    "The concierge of a grand hotel becomes involved in a murder mystery.",
    "A family of superheroes hides their powers to live a normal life.",
    "A young girl’s emotions come to life inside her mind.",
    "An old man ties balloons to his house and goes on an adventure.",
    "Toys come to life and form a close friendship.",
    "A girl enters the world of spirits to save her parents.",
    "A prince must protect his forest from destruction and demons.",
    "Two teenagers share a supernatural connection through dreams.",
    "A teenager discovers a secret weapon in post-apocalyptic Japan.",
    "A pop star’s career unravels in a psychological thriller.",
    "A young lion prince flees after the death of his father.",
    "A princess discovers her icy powers and embraces who she is.",
    "A young girl sets sail across the ocean to save her people.",
    "A boy visits the Land of the Dead to uncover his family’s history.",
    "A magical family in Colombia loses their powers.",
    "A girl navigates adolescence while turning into a red panda.",
    "A waste-collecting robot discovers love and a purpose.",
    "A rat dreams of becoming a chef in a French restaurant.",
    "A clownfish searches for his lost son across the ocean.",
    "A family encounters terrifying forces in an isolated hotel.",
    "A lonely man falls in love with an operating system.",
    "Two magicians compete with dangerous consequences.",
    "A cop infiltrates the mob while a gangster infiltrates the police.",
    "A man investigates strange events on a mysterious island.",
    "A man searches for his missing wife with dark revelations.",
    "Two friends get caught up in a bizarre crime story.",
    "Five teens form an unlikely bond during detention.",
    "A wisecracking anti-hero seeks revenge after a mutation.",
    "A group of misfits bands together to save the galaxy.",
    "An Amazonian princess fights to stop a world war.",
    "A Wakandan prince becomes king and protector of his nation.",
    "A hitman and his partner face a deadly pursuit.",
    "A con man’s elaborate scheme takes a dark turn.",
    "Jurors deliberate a murder case with hidden truths.",
    "Three gunmen seek a buried treasure in the Old West.",
    "A college graduate is seduced by an older woman.",
    "A boy rises in the favelas to become a crime boss.",
    "A stuntman falls for a woman with a violent boyfriend.",
    "A Polish-Jewish pianist struggles to survive WWII.",
    "A Puritan family faces evil forces in the woods.",
    "Paranormal investigators help a haunted family.",
    "A family must remain silent to survive deadly creatures.",
    "A teen is stalked by a mysterious entity after a curse.",
    "A black man uncovers a disturbing secret while visiting his girlfriend's family.",
    "A couple joins a pagan cult with horrifying rituals.",
    "A mother struggles with grief as supernatural forces appear.",
    "A linguist attempts to communicate with aliens to prevent war.",
    "A programmer evaluates a highly intelligent humanoid robot.",
    "A blade runner discovers secrets about replicants and himself.",
    "A hitman travels through time to eliminate targets.",
    "A soldier repeats a time loop during a war with aliens.",
    "An astronaut tries to return to Earth after an accident in space.",
    "A man protects a pregnant woman in a dystopian future.",
    "Aliens land in Johannesburg and are segregated from humans.",
    "A train travels around a frozen world after climate disaster.",
    "A man searches for a cure in a world overrun by infected.",
    "A former UN investigator races to stop a zombie pandemic.",
    "Doctors race to contain a deadly virus outbreak.",
    "A WWI soldier races against time to deliver a message.",
    "A medic saves lives during the Battle of Okinawa.",
    "A mathematician helps crack Nazi codes during WWII.",
    "A physicist overcomes ALS to become a scientific legend.",
    "A singer rises to fame as Queen's legendary frontman.",
    "A musical look at Elton John's breakthrough and struggles.",
    "The life and music of Elvis Presley in a dramatic retelling.",
    "A physicist oversees the creation of the atomic bomb."
};

int[] years = {
    1972, 2008, 1994, 1994, 1994,
    2010, 1999, 1999, 2003, 1977,
    1980, 1974, 1991, 1993, 1995,
    2010, 2000, 1997, 2014, 1999,
    1998, 2016, 2014, 2019, 2019,
    2015, 2013, 2015, 2012, 2015,
    2009, 1998, 2002, 2001, 2014,
    2004, 2015, 2009, 1995, 2001,
    1997, 2016, 1988, 1997, 1994,
    2013, 2016, 2017, 2021, 2022,
    2008, 2007, 2003, 1980, 2013,
    2006, 2006, 2010, 2014, 1998,
    1985, 2016, 2014, 2017, 2018,
    2007, 1995, 1957, 1966, 1967,
    2002, 2011, 2002, 2015, 2013,
    2018, 2014, 2017, 2019, 2014,
    2016, 2015, 2017, 2012, 2014,
    2013, 2006, 2009, 2013, 2007,
    2013, 2011, 2019, 2016, 2014,
    2014, 2018, 2019, 2022, 2023
};

float[] ratings = {
    9.2f, 9.0f, 8.9f, 9.3f, 8.8f,
    8.8f, 8.8f, 8.7f, 9.0f, 8.6f,
    8.7f, 9.0f, 8.6f, 8.9f, 8.6f,
    7.7f, 8.5f, 7.8f, 8.6f, 8.6f,
    8.6f, 8.0f, 8.5f, 8.4f, 8.6f,
    8.0f, 8.2f, 7.8f, 8.4f, 7.8f,
    7.8f, 8.1f, 8.1f, 8.2f, 8.1f,
    8.0f, 8.1f, 8.2f, 8.3f, 8.6f,
    8.4f, 8.4f, 8.1f, 8.2f, 8.5f,
    7.5f, 7.6f, 8.4f, 7.2f, 7.0f,
    8.4f, 8.1f, 8.1f, 8.4f, 8.0f,
    8.5f, 8.5f, 8.1f, 8.1f, 8.1f,
    7.8f, 7.6f, 8.0f, 8.0f, 7.3f,
    8.1f, 8.4f, 9.0f, 8.8f, 8.0f,
    8.6f, 7.8f, 8.5f, 6.9f, 7.5f,
    7.5f, 6.8f, 7.7f, 7.1f, 6.8f,
    7.9f, 7.7f, 8.0f, 7.4f, 7.6f,
    7.5f, 8.0f, 7.9f, 7.1f, 7.4f,
    7.0f, 6.7f, 8.3f, 8.1f, 8.0f,
    7.5f, 6.8f, 7.7f, 7.1f, 6.8f,
};

int count = new[] { titles.Length, descriptions.Length, years.Length, ratings.Length }.Min();

for (int i = 0; i < count; i++)
{
    movies.Add(new Movie(idCount++, titles[i], years[i], descriptions[i], ratings[i]));
}
    }
    }

    public async Task<PageResult<Movie>> ReadAll(int page, int size)
    {
     int totalCount = movies.Count;
     int start = Math.Clamp((page-1)*size, 0, totalCount);
     int length = Math.Clamp(size, 0, totalCount - start);
     List<Movie> values = movies.Slice(start, length);
     var pagedResult = new PageResult<Movie>(values, totalCount);

     return await Task.FromResult(pagedResult);
    }
    public async Task<Movie?> Create(Movie newMovie)
    {
        newMovie.Id = idCount++;
        movies.Add(newMovie);
        

        return await Task.FromResult(newMovie);
    }
    public async Task<Movie?> Read(int id)
    {
        Movie? movie = movies.FirstOrDefault((u) => u.Id == id);
        return await Task.FromResult(movie);
    }
    public async Task<Movie?> Update(int id, Movie newMovie)
    {
        Movie? movie = movies.FirstOrDefault((u) => u.Id == id);
        if (movie != null)
        {
            movie.Title = newMovie.Title;
            movie.Year = newMovie.Year;
            movie.Description = newMovie.Description;
            movie.Rating = newMovie.Rating;
        }
        return await Task.FromResult(movie);
    }    
    public async Task<Movie?> Delete(int id)
    {
        Movie? movie = movies.FirstOrDefault((u) => u.Id == id);
        if (movie != null)
        {
            movies.Remove(movie);
        }
        return await Task.FromResult(movie);
    }
    
    }
