namespace SimpleMDB;
using System;
using System.Collections.Generic;

public class MockActorRepository : IActorRepository
{
    private List<Actor> actors;
    private int idCount;

    public MockActorRepository()
    {
        actors = [];
        idCount = 0;

        string[] firstNames = {
            "Meryl", "Denzel", "Leonardo", "Kate", "Tom", "Viola", "Daniel", "Julia", "Robert", "Natalie",
            "Morgan", "Cate", "Joaquin", "Emma", "Mark", "Amy", "Christian", "Jennifer", "Brad", "Angelina",
            "Will", "Sandra", "Matthew", "Charlize", "Hugh", "Anthony", "Marion", "Robert", "Scarlett", "Al",
            "Halle", "Johnny", "Michelle", "Ryan", "Helen", "Javier", "Saoirse", "Eddie", "Glenn", "Gary",
            "Nicole", "Tom", "Keira", "Christoph", "Anne", "Samuel", "Rachel", "Jeff", "Emma", "Tom",
            "Salma", "Hugh", "Emily", "Chris", "Margot", "Mark", "Brie", "Idris", "Gwyneth", "Chris",
            "Natalie", "Michael", "Kate", "Chiwetel", "Zendaya", "Steve", "Florence", "Michael", "Jeremy", "Kate",
            "Jared", "Tilda", "Daniel", "Alicia", "Benedict", "Lupita", "Michael", "Jessica", "John", "Emma",
            "Cillian", "Helena", "Mahershala", "Octavia", "Robert", "Diane", "Ben", "Eva", "Florence", "Michael",
            "Emily", "Ryan", "Chadwick", "Gal", "Kirsten", "Oscar", "Marion", "Jake", "Emily", "Donald"
        };

        string[] lastNames = {
            "Streep", "Washington", "DiCaprio", "Winslet", "Hanks", "Davis", "Day-Lewis", "Roberts", "De Niro", "Portman",
            "Freeman", "Blanchett", "Phoenix", "Thompson", "Ruffalo", "Adams", "Bale", "Lawrence", "Pitt", "Jolie",
            "Smith", "Bullock", "McConaughey", "Theron", "Jackman", "Hopkins", "Cotillard", "Downey Jr.", "Johansson", "Pacino",
            "Berry", "Depp", "Williams", "Gosling", "Mirren", "Bardem", "Ronan", "Redmayne", "Close", "Oldman",
            "Kidman", "Cruise", "Knightley", "Waltz", "Hathaway", "Jackson", "McAdams", "Bridges", "Stone", "Hardy",
            "Hayek", "Grant", "Blunt", "Evans", "Robbie", "Wahlberg", "Larson", "Elba", "Paltrow", "Hemsworth",
            "Dormer", "Fassbender", "Beckinsale", "Ejiofor", "Nyong'o", "Carell", "Pugh", "Jordan", "Renner", "Hudson",
            "Leto", "Swinton", "Craig", "Vikander", "Cumberbatch", "Nyong'o", "Caine", "Chastain", "Boyega", "Watson",
            "Murphy", "Bonham Carter", "Ali", "Spencer", "Pattinson", "Kruger", "Affleck", "Green", "Williams", "Fassbender",
            "Thompson", "Gosling", "Boseman", "Johnson", "Biel", "Duvall", "Cotillard", "Gyllenhaal", "Stone", "Evans"
        };

        string[] bio = {
            "An award-winning actor known for transformative dramatic roles.",
            "Renowned for his powerful performances in both stage and screen.",
            "Breakout star celebrated for gritty, intense characters.",
            "Beloved for her emotional depth and period-piece work.",
            "Iconic leading man with a knack for heartfelt storytelling.",
            "Acclaimed for portraying complex, resilient characters.",
            "Method actor praised for his immersive approach.",
            "Known for her charisma and memorable romantic leads.",
            "Veteran actor with a prolific career spanning decades.",
            "Versatile performer adept at both drama and action.",
            "Legendary voice actor and charismatic screen presence.",
            "Two-time Oscar winner with a flair for the avant-garde.",
            "Critically acclaimed for intense character studies.",
            "Esteemed actress with a background in classical theatre.",
            "Character actor with a gift for nuanced supporting roles.",
            "Reliable scene-stealer in both blockbusters and indies.",
            "Award-nominated actor with a dynamic on-screen presence.",
            "Young star celebrated for breaking box-office records.",
            "Heartthrob known for his work in action and drama.",
            "Global icon admired for both film work and humanitarianism.",
            "Box-office titan with a prolific filmography.",
            "Comedy veteran praised for impeccable timing.",
            "Golden Globe winner with a talent for gritty roles.",
            "Dynamic actress known for powerful, raw performances.",
            "Musical theatre crossover acclaimed for stage and film.",
            "Legendary actor with a Nobel-level body of work.",
            "French cinema icon delivering lush, emotional roles.",
            "Superhero franchise lead with a vast fan following.",
            "Blockbuster and indie darling with wide range.",
            "Master of dramatic transformations and character work.",
            "Cult favorite known for eclectic, edgy roles.",
            "Independent film staple with an acute sense of humor.",
            "Leading man famed for romantic and biographical films.",
            "Royal Shakespeare Company alum turned film star.",
            "Oscar-winning performance in a touching biopic.",
            "Rising star known for ethereal, melodic voice roles.",
            "Tony Award winner seamlessly crossing to film.",
            "Veteran character actor with an enduring career.",
            "Emmy winner turned film heavyweight.",
            "Australian star with a gift for dramatic tension.",
            "Blockbuster fixture with unforgettable action scenes.",
            "Oscar-nominated actress shining in emotional dramas.",
            "Capable leading man equally at home in indie films.",
            "Golden Globe nominee with a loyal fan base.",
            "Seasoned actor celebrated for his naturalism.",
            "Groundbreaking performances in genre-defying films.",
            "Hollywood A-lister excelling in both drama and comedy.",
            "Physical actor known for intense, brooding roles.",
            "Cultured performer with an ear for classical nuance.",
            "Emmy-winning comedic actor with dramatic chops.",
            "Breakout star from critically acclaimed indie hits.",
            "Oscar-winner balancing mainstream and arthouse work.",
            "Versatile talent thriving in ensemble casts.",
            "Champion of method acting in modern cinema.",
            "Leading lady lauded for her emotional authenticity.",
            "Avengers staple and box-office draw.",
            "Heartfelt performances in both film and television.",
            "Prolific actor with commanding stage presence.",
            "Award-winning portrayals of real-life figures.",
            "Charismatic star with international appeal.",
            "Emmy and Oscar nominee crossing media seamlessly.",
            "Shakespearean training meets blockbuster franchise.",
            "Powerhouse actress known for transformative roles.",
            "Rising star who blends intensity with vulnerability.",
            "Veteran actor with a penchant for historical dramas.",
            "Energetic performer bringing youthful exuberance.",
            "Chameleon-like talent in both lead and character roles.",
            "Globally recognized actor with humanitarian awards.",
            "Broadway legend turned screen icon.",
            "Gripping performances in psychological thrillers.",
            "Beloved for heartfelt, family-oriented films.",
            "Dramatic turn in award-circuit favorites.",
            "Diverse roles from comedy to hard-hitting drama.",
            "Family-friendly star with a warm screen presence.",
            "Genre-defining performances in fantasy epics.",
            "Oscar-nominated actor known for transformative make-ups.",
            "Hollywood royalty with decades of experience.",
            "Fresh face from indie scene garnering praise.",
            "Action hero with a signature gruff intensity.",
            "Emotional portrayals resonating across generations.",
            "Versatile leading lady adapting to every genre.",
            "Periodic drama specialist with refined technique.",
            "Breakout performance in a major franchise hit.",
            "Critically lauded for fearless role choices.",
            "Reliable performer with a steady film output.",
            "International star bridging East and West cinema.",
            "Dramatic actor flourishing in low-budget films.",
            "Detective roles in top-rated television series.",
            "Emerging talent shaking up the industry narrative.",
            "Veteran presence anchoring big ensemble casts.",
            "kuhi",
            "kuhi",
            "kuhi",
            "kuhi",
            "kuhi",
            "kuhi",
            "kuhi",
            "kuhi",
            "kuhi",
            "kuhi",
            "kuhi",
        };

        float[] ratings = {
            9.8f, 9.5f, 9.3f, 9.1f, 9.0f, 8.9f, 8.8f, 8.7f, 8.6f, 8.5f,
            8.5f, 8.4f, 8.4f, 8.3f, 8.3f, 8.2f, 8.2f, 8.1f, 8.1f, 8.0f,
            8.0f, 7.9f, 7.9f, 7.8f, 7.8f, 7.7f, 7.7f, 7.6f, 7.6f, 7.5f,
            7.5f, 7.4f, 7.4f, 7.3f, 7.3f, 7.2f, 7.2f, 7.1f, 7.1f, 7.0f,
            7.0f, 6.9f, 6.9f, 6.8f, 6.8f, 6.7f, 6.7f, 6.6f, 6.6f, 6.5f,
            6.5f, 6.4f, 6.4f, 6.3f, 6.3f, 6.2f, 6.2f, 6.1f, 6.1f, 6.0f,
            6.0f, 5.9f, 5.9f, 5.8f, 5.8f, 5.7f, 5.7f, 5.6f, 5.6f, 5.5f,
            5.5f, 5.4f, 5.4f, 5.3f, 5.3f, 5.2f, 5.2f, 5.1f, 5.1f, 5.0f,
            5.0f, 4.9f, 4.9f, 4.8f, 4.8f, 4.7f, 4.7f, 4.6f, 4.6f, 4.5f,
            4.5f, 4.4f, 4.4f, 4.3f, 4.3f, 4.2f, 4.2f, 4.1f, 4.1f, 4.0f
        };

int count = Math.Min(Math.Min(firstNames.Length, lastNames.Length), Math.Min(bio.Length, ratings.Length));

        for (int i = 0; i < count; i++)
        {
            actors.Add(new Actor(
                idCount++,
                firstNames[i],
                lastNames[i],
                bio[i], 
                ratings[i]
            ));
        }
    }

    public async Task<PageResult<Actor>> ReadAll(int page, int size)
    {
     int totalCount = actors.Count;
     int start = Math.Clamp((page-1)*size, 0, totalCount);
     int length = Math.Clamp(size, 0, totalCount - start);
     List<Actor> values = actors.Slice(start, length);
     var pagedResult = new PageResult<Actor>(values, totalCount);

     return await Task.FromResult(pagedResult);
    }
    public async Task<Actor?> Create(Actor newActor)
    {
        newActor.Id = idCount++;
        actors.Add(newActor);
        

        return await Task.FromResult(newActor);
    }
    public async Task<Actor?> Read(int id)
    {
        Actor? actor = actors.FirstOrDefault((u) => u.Id == id);
        return await Task.FromResult(actor);
    }
    public async Task<Actor?> Update(int id, Actor newActor)
    {
        Actor? actor = actors.FirstOrDefault((u) => u.Id == id);
        if (actor != null)
        {
            actor.FirstName = newActor.FirstName;
            actor.LastName = newActor.LastName;
            actor.Bio = newActor.Bio;
            actor.Rating = newActor.Rating;
        }
        return await Task.FromResult(actor);
    }    
    public async Task<Actor?> Delete(int id)
    {
        Actor? actor = actors.FirstOrDefault((u) => u.Id == id);
        if (actor != null)
        {
            actors.Remove(actor);
        }
        return await Task.FromResult(actor);
    }
    
    }
