# xAPI.NET
xAPI.NET is a free, open-source xAPI client for .NET. It allows a Learning Record Provider (LRP) to consume the REST API exposed by a Learning Record Store (LRS) compliant with [the xAPI specification](https://github.com/adlnet/xAPI-Spec). Currently, versions 1.0.* of xAPI are partially supported.

## Getting Started
These steps will get you started with xAPI.NET.

### Prerequisites
In order to install xAPI.NET, you need to ensure you have the following things:

* An account on a Learning Record Store (such as [ADL's test LRS](https://lrs.adlnet.gov/), [Learning Locker](https://learninglocker.net/), or any other LRS including your own), and the associated credentials.
* An install of Visual Studio 2015 or newer.
* A .NET project from which you wish to consume the LRS.
* A network connection.

### Installing
To install the client, you can either clone the repository and build the library from the source (you can even tweak the code to your needs), or install it with NuGet package manager:

```
Install-Package xAPI.NET
```
This will install the package and its dependencies ([Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) & [Microsoft.AspNet.WebApi.Client](https://www.asp.net/web-api)). You can also use the integrated NuGet manager panel from Visual Studio.

### Sample code
```
#!c#
// Gather xAPI configuration from somewhere safe!
string ENDPOINT_URI = "https://www.example.org/xAPI/";
string VERSION = "1.0.3";
string USERNAME = "demo";
string PASSWORD = "demo";

// Choose your authentication provider.
// Currently, only anonymous clients and Basic HTTP are supported.
var config = new BasicEndpointConfiguration()
{
    Username = USERNAME,
    Password = PASSWORD
};

// Create the client
IXApiClient client = XApiClientFactory.Create(config);

// Call the LRS!
About about = await this._client.About.Get();
```

## Running the tests

### Using Visual Studio test explorer and NUnit 3 test adapter

* Clone the repository.
* Install the [NUnit 3 test adapter extension for Visual Studio](http://nunit.org/?p=download).
* Start Visual Studio.
* Build the solution.
* `Ctrl-R, A` and check out the Test Explorer panel!

### Using NUnit 3 console

* Clone the repository.
* Start Visual Studio.
* Build the solution.
* Install the [NUnit 3 console app](http://nunit.org/?p=download).
* `nunit3-console.exe path\to\xAPI.Client.Tests.dll`

## Missing features

* OAuth authentication (currently, the [`OAuthAuthenticator`](xAPI.Client/Authenticators/OAuthAuthenticator.cs) class is not implemented).
* Attachments in statements ([requests](https://github.com/adlnet/xAPI-Spec/blob/master/xAPI-Communication.md#152-multipartmixed) & [responses](https://github.com/adlnet/xAPI-Spec/blob/master/xAPI-Communication.md#213-get-statements)).

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