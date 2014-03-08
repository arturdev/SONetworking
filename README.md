SONetworking
============

Networking Static Library  (for Windows phone 7.1)

USAGE
=====

Add all *.dll files containing in SONetworking/Bin/Release/ folder to your project's references.

Add SONetworking to using list.

<pre>
using SONetworking;
</pre>

Example of how to get facebook's user friend info

```C# 
SOJsonRequestOperation.BaseUrl = "https://graph.facebook.com/";

List<KeyValuePair<string,string>> parameters = new List<KeyValuePair<string, string>>();
parameters.Add(new KeyValuePair<string, string>("access_token", "_your_fb_access_token_"));

SOJsonRequestOperation.StartJSONRequestOperation(HttpMethod.Get, "me/friends",parameters,
    delegate(bool success, int statusCode, JObject fbObject)
    {
        if (success)
        {
            foreach (JObject friend in fbObject["data"])
            {
                Debug.WriteLine(friend["name"]);
            }
        }
    });
```

