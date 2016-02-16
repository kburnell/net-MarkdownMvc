<p align="center">
    <a href="#markdownmvc">
        <img alt="logo" src="Assets/logo.png">
    </a>
</p>

# MarkdownMvc

[![][build-img]][build]
[![][nuget-img]][nuget]

ASP.NET MVC view engine and HTML helper that renders Markdown.

[build]:     https://ci.appveyor.com/project/TallesL/net-MarkdownMvc
[build-img]: https://ci.appveyor.com/api/projects/status/github/tallesl/net-MarkdownMvc?svg=true
[nuget]:     https://www.nuget.org/packages/MarkdownMvc
[nuget-img]: https://badge.fury.io/nu/markdownmvc.svg

## View Engine

Register the engine on your Global.asax's Application_Start:

```cs
ViewEngines.Engines.Add(new MarkdownViewEngine());
```

You can also pass a custom CSS URL to be used if you want:

```cs
ViewEngines.Engines.Add(new MarkdownViewEngine("/css/markdown.css"));
```

The views are searched in `~/Views/{controller name}/{action name or view name if provided}.md`.

The rendered HTML is simple, it's a sum of:

* `<!doctype html>`;
* `<style>a custom css</style>` ([sample]) or `<link rel="stylesheet" href="the provided CSS URL">`;
* `<title>View's filename (without extension)</title>`;
* and the parsed Markdown content.

Also, don't forget to set the "Build Action" of your ".md" files to "Content" (else they won't be copied when
publishing):

![][build-action]

[sample]:       https://rawgit.com/tallesl/net-MarkdownMvc/master/Assets/sample.html
[build-action]: Assets/build-action.png

## HTML Helper

```
@using MarkdownMvcLibrary

@{
    ViewBag.Title = "My Stupid Page";
    Layout = "~/Views/Shared/Layout.cshtml";
}

<h1>Here, have some Markdown:</h1>

@Html.Markdown("crazy-markdown-content.md")
```