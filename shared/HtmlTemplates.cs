namespace SimpleMDB;

public class HtmlTemplates
{
    public static string Base(string title, string header, string content){
     {
        return $@"
        <html>
        <head>
            <title>{title}</title>
            <link rel=""icon"" type=""image/x-icon"" href=""favicon.ico"" />
            <link rel=""stylesheet"" type=""text/css"" href= ""style/main.css"" />
            <script type=""text/javascript"" src=""script/main.js"" defer></script>
        </head>
        <body>
            <h1>{header}</h1>
            <div>{content}</div>
        </body>
        </html>
        ";
     }
    }
}