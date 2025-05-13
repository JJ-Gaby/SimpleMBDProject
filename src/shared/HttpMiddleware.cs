namespace SimpleMDB;
using System.Net;
using System.Collections;

public delegate Task HttpMiddleware(HttpListenerRequest req, HttpListenerResponse res, Hashtable options);