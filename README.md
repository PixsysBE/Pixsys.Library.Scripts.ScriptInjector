
# Pixsys.Library.Scripts.ScriptInjector

This middleware and IResultFilter will inject scripts or script paths to your HTML, and [all your scripts tags will contain a nonce](https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders?tab=readme-ov-file#using-nonces-and-generated-hashes-with-content-security-policy) to be compliant with your content security policy.

## 1. Installation

### 1.1 Register the filter in `Program.cs`

You can call the `AddInjectionScriptsFilter` method right after `AddControllersWithViews`:

```csharp
using Pixsys.Library.Scripts.ScriptInjector;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddInjectionScriptsFilter();
```

### 1.2 Usage

#### 1.2.1 Registered scripts vs injected scripts

Depending on where you will set your scripts (or scripts paths), they will be registered in two differents services with a different Lifetime :

- `RegisteredScriptInjector` is registered as a Singleton service.
- `ScriptInjector` is registered as a scoped service.

This allows you to "pre-register" some of your scripts in `Program.cs` and call them only when you need them.

#### 1.2.1 Register scripts

Set your scripts or scripts paths and specify if they must be used on all pages or not.

```csharp
WebApplication app = builder.Build();

_ = app.RegisterScripts(
    [
        new Pixsys.Library.Scripts.ScriptInjector.Models.RegisteredScript { Name = "global-hello", Script = "console.log('registered Hello World on all pages');", UseOnAllPages = true },
        new Pixsys.Library.Scripts.ScriptInjector.Models.RegisteredScript { Name = "specific-hello", Script = "console.log('registered Hello World on specific pages');", UseOnAllPages = false },
    ]);    

_ = app.RegisterScriptPaths(
    [
        new Pixsys.Library.Scripts.ScriptInjector.Models.RegisteredPath { Name = "global-path-1", Path = "<FILE_PATH_DOT_JS>", UseOnAllPages = true },
    ]);   

```


#### 1.2.1 Inject the service into your controller

```csharp
private readonly IScriptInjector scriptInjector;

public MyController(IScriptInjector> scriptInjector)
{
     this.scriptInjector = scriptInjector;
}
```

#### 1.2.2 Methods

```csharp
scriptInjector.AddScript(new Pixsys.Library.Scripts.ScriptInjector.Models.InjectedScript
{
    Name="<SCRIPT_NAME>",
    Script="<SCRIPT>",
});

scriptInjector.AddPath(new Pixsys.Library.Scripts.ScriptInjector.Models.InjectedPath
{
    Name="<SCRIPT_NAME>",
    Path="https://<PATH>/<FILE>.js",
});
```

#### 1.3 MVC Views

You can inject scripts directly in the MVC view by calling the `script-inject-code` and/or `script-inject-path` tag helpers:

```html
<!DOCTYPE html>
<html>
<head>
</head>
<body>

<script-inject-code name="testfromView" script="console.log('injected Test from view');"></script-inject-code>
<script-inject-path name="signalr" path="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/8.0.0/signalr.min.js"></script-inject-path>

</body>
</html>
```
The following output will be generated:

```html
<!DOCTYPE html>
<html>
<head>
</head>
<body>

<script nonce="">console.log('injected Test from view');</script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/8.0.0/signalr.min.js" nonce=""></script>

</body>
</html>
```

It is also possible to use one of the registered scripts (is it has not been set to be used on all pages) via the `script-inject-call-registered-scripts` tag helper:

```html
<!DOCTYPE html>
<html>
<head>
</head>
<body>

<script-inject-call-registered-script name="specific-hello"></script-inject-call-registered-script>

</body>
</html>
```
The following output will be generated:

```html
<!DOCTYPE html>
<html>
<head>
</head>
<body>

<script nonce="">console.log('registered Hello World on specific pages');</script>

</body>
</html>
```
#### 1.3 Use of Nonce

In order to be compliant with your content security policy, this package is using the Nonce provider functionality from [NetEscapades.AspNetCore.SecurityHeaders](https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders?tab=readme-ov-file#using-nonces-and-generated-hashes-with-content-security-policy)

> Please refer to the documentation listed [here](https://github.com/andrewlock/NetEscapades.AspNetCore.SecurityHeaders?tab=readme-ov-file#using-nonces-and-generated-hashes-with-content-security-policy)
