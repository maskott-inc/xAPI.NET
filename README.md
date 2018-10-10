# xAPI.NET
xAPI.NET is a free, open-source xAPI client for .NET. It allows a Learning Record Provider (LRP) to consume the REST API exposed by a Learning Record Store (LRS) compliant with [the xAPI specification](https://github.com/adlnet/xAPI-Spec). Currently, versions 1.0.* of xAPI are [partially supported](#missing-features).

## Getting Started
These steps will get you started with xAPI.NET.

### Prerequisites
In order to install xAPI.NET, you need to ensure you have the following things:

* An account on a Learning Record Store (such as [ADL's test LRS](https://lrs.adlnet.gov/), [Learning Locker](https://learninglocker.net/), or any other LRS including your own), and the associated credentials.
* An install of Visual Studio 2017 or newer.
* A .NET project from which you wish to consume the LRS API.
* A network connection.

### Installing
To install the client, you can either clone the repository and build the library from the source (you can even tweak the code to your needs), or install it with NuGet package manager:

```
Install-Package xAPI.NET
```
This will install the package and its dependencies. You can also use the integrated NuGet manager panel from Visual Studio.

### Sample code
```csharp
// Gather xAPI configuration from somewhere safe!
string ENDPOINT_URI = "https://www.example.org/xAPI/";
string VERSION = "1.0.3";
string USERNAME = "demo";
string PASSWORD = "demo";

// Choose your authentication provider.
var config = new BasicEndpointConfiguration()
{
    EndpointUri = new Uri(ENDPOINT_URI),
    Version = XApiVersion.Parse(VERSION),
    HttpClient = null, // use internal HttpClient
    Username = USERNAME,
    Password = PASSWORD
};

// Create the client
using (IXApiClient client = new XApiClient(config))
{
    // About resource
    About about = await client.About.Get();
    Console.WriteLine(string.Join(", ", about.Versions));

    // Activity profile resource
    var request = new GetActivityProfileRequest()
    {
        ActivityId = new Uri("http://www.example.org/activity"),
        ProfileId = "<profile_id>"
    };
    ActivityProfileDocument<CustomContent> activityProfile = await client.ActivityProfiles.Get<CustomContent>(request);
    Console.WriteLine(activityProfile.Content.CustomField);

    // Statement resource
    var statement = new Statement()
    {
        Id = Guid.NewGuid(),
        Actor = new Agent()
        {
            Name = "student",
            MBox = new Uri("mailto:student@example.org")
        },
        Verb = new Verb()
        {
            Id = new Uri("http://adlnet.gov/expapi/verbs/attended"),
            Display = new LanguageMap() { { "en-US", "attended" } }
        },
        Object = new Activity()
        {
            Id = new Uri("http://www.example.com/meetings/occurances/34534"),
            Definition = new ActivityDefinition()
            {
                Name = new LanguageMap() { { "en-US", "example meeting" } },
                Type = new Uri("http://adlnet.gov/expapi/activities/meeting")
            }
        }
    };
    var request = new PostStatementRequest(statement);
    bool statementCreated = await client.Statements.Post(request);
    if (statementCreated)
    {
        Console.WriteLine("Statement was successfully created!");
    }
    else
    {
        Console.WriteLine("A statement with this ID already exists.");
    }
}

```

## Running the tests

### Using dotnet test

* Clone the repository.
* Open a cmd or powershell terminal.
* cd into the test project.
* Run the command `dotnet test`.

## Missing features

* OAuth authentication (currently, the [`OAuthAuthenticator`](src/xAPI.Client/Authenticators/OAuthAuthenticator.cs) class is not implemented).
* Attachments in statements ([requests](https://github.com/adlnet/xAPI-Spec/blob/master/xAPI-Communication.md#152-multipartmixed) & [responses](https://github.com/adlnet/xAPI-Spec/blob/master/xAPI-Communication.md#213-get-statements)).
* Document resources with content types other than `application/json`.

## Contributing
1. `git config --global core.autocrlf false`
2. Hack!
3. Make a pull request.

## Versioning
We use [SemVer](http://semver.org/) for versioning. For the versions available, see the tags on this repository.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact & credits
This project is maintained by [Maskott](https://www.maskott.com/). You can contact us at [contact@maskott.com](mailto:contact@maskott.com).
