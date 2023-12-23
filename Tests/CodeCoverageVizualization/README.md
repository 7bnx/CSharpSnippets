### Installation

 - Locally(project/solution level)

```
 dotnet new tool-manifest
```
```
 dotnet tool install dotnet-coverage
```
```
 dotnet tool install dotnet-reportgenerator-globaltool
```

 - OR Globally

```
 dotnet tool -g install dotnet-coverage
```
```
 dotnet tool -g install dotnet-reportgenerator-globaltool
```
### Creating report
In case of local usage(solution or project level) - add ***dotnet tool run*** before the followings commands
#### Parse source files
```
dotnet-coverage collect -f cobertura -o TestsCoverage.xml dotnet test
```
 - [-f] - parse format: **cobertura** or **xml** or **coverage**
 - [-o] - output path

More options: [msdn](https://learn.microsoft.com/en-us/dotnet/core/additional-tools/dotnet-coverage) or [github](https://github.com/dotnet/docs/blob/main/docs/core/additional-tools/dotnet-coverage.md)

#### Report
```
reportgenerator -reports:TestsCoverage.xml -targetdir:CoverageReport -reportTypes:HtmlSummary;MarkdownSummary
```
More options: [github](https://github.com/danielpalme/ReportGenerator)