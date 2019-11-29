# Protest GO: Unity client library

Implements rest api client for protest-go backed.

**Important:** You must have valid AppKey and AppSecret to use this api (please contact to the authors).

## API

### Import library:

```csharp
using ProtestGoClient;
```

### Initialize client with `AppKey` and `AppSecret`:

```csharp
Client.Init(appKey, appSecret)            // mandatory
Client.SetBaseUrl("http://example.com/")  // optional, defaul https://pgo.panki.ru/
Client.SetDebug(true)                     // optional, default false
```

### Authorize user by setting `accessToken` if it was previously saved:

```csharp
Client.SetAccessToken(token)
```

### Make calls:

```csharp
// Init application
Client.App
    .Init()
    .Then(settings => { ... })
    .Catch(err => { ... }) // Handle errors
```

```csharp
// Register anonymous user
Client.App
    .Register()
    .Then(token => { ... }) // Save token somethere
    .Catch(err => { ... }) // Handle errors
```

## How to install

Put this line to the `Packages/manifest.json`

```json
{
  "dependencies": {
      "com.protest-go.protest-go-client": "https://github.com/panki/protest-go-unity-client.git#v0.0.1"
  }
```

Package manager will checkout source code and install package.

## How to update

Change version tag in `Packages/manifest.json` to the new version, for example v0.0.1 to v0.0.2:

```json
{
  "dependencies": {
      "com.protest-go.protest-go-client": "https://github.com/panki/protest-go-unity-client.git#v0.0.2"
  }
```

Package manager will update package to the specified version. If for some reason package manager shows previsous version, try to remove corresponding lock section in `Packages/manifest.json`:

```json
{
  "dependencies": {
      ....
  },
  "lock": {
    "com.protest-go.protest-go-client": {
      "hash": "xxx",
      "revision": "xxx"
    }
  }
```

After that everything should be ok)
