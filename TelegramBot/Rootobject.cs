
public class Rootobject
{
    public Data data { get; set; }
}

public class Data
{
    public int mal_id { get; set; }
    public string url { get; set; }
    public Images images { get; set; }
    public Trailer trailer { get; set; }
    public bool approved { get; set; }
    public Title[] titles { get; set; }
    public string title { get; set; }
    public object title_english { get; set; }
    public string title_japanese { get; set; }
    public string[] title_synonyms { get; set; }
    public string type { get; set; }
    public string source { get; set; }
    public int episodes { get; set; }
    public string status { get; set; }
    public bool airing { get; set; }
    public Aired aired { get; set; }
    public string duration { get; set; }
    public string rating { get; set; }
    public object score { get; set; }
    public object scored_by { get; set; }
    public int rank { get; set; }
    public int popularity { get; set; }
    public int members { get; set; }
    public int favorites { get; set; }
    public object synopsis { get; set; }
    public object background { get; set; }
    public object season { get; set; }
    public object year { get; set; }
    public Broadcast broadcast { get; set; }
    public Producer[] producers { get; set; }
    public object[] licensors { get; set; }
    public object[] studios { get; set; }
    public Genre[] genres { get; set; }
    public object[] explicit_genres { get; set; }
    public object[] themes { get; set; }
    public object[] demographics { get; set; }
}

public class Images
{
    public Jpg jpg { get; set; }
    public Webp webp { get; set; }
}

public class Jpg
{
    public string image_url { get; set; }
    public string small_image_url { get; set; }
    public string large_image_url { get; set; }
}

public class Webp
{
    public string image_url { get; set; }
    public string small_image_url { get; set; }
    public string large_image_url { get; set; }
}

public class Trailer
{
    public object youtube_id { get; set; }
    public object url { get; set; }
    public object embed_url { get; set; }
    public Images1 images { get; set; }
}

public class Images1
{
    public object image_url { get; set; }
    public object small_image_url { get; set; }
    public object medium_image_url { get; set; }
    public object large_image_url { get; set; }
    public object maximum_image_url { get; set; }
}

public class Aired
{
    public DateTime from { get; set; }
    public object to { get; set; }
    public Prop prop { get; set; }
    public string _string { get; set; }
}

public class Prop
{
    public From from { get; set; }
    public To to { get; set; }
}

public class From
{
    public int day { get; set; }
    public int month { get; set; }
    public int year { get; set; }
}

public class To
{
    public object day { get; set; }
    public object month { get; set; }
    public object year { get; set; }
}

public class Broadcast
{
    public object day { get; set; }
    public object time { get; set; }
    public object timezone { get; set; }
    public object _string { get; set; }
}

public class Title
{
    public string type { get; set; }
    public string title { get; set; }
}

public class Producer
{
    public int mal_id { get; set; }
    public string type { get; set; }
    public string name { get; set; }
    public string url { get; set; }
}

public class Genre
{
    public int mal_id { get; set; }
    public string type { get; set; }
    public string name { get; set; }
    public string url { get; set; }
}
