var target = Argument("target", "build");
var output = Argument("output", "./artifacts");
var versionSuffix = Argument<string>("versionSuffix", null);

Task("clean")
    .Does(() => {
        CleanDirectories("./src/**/bin/");
        CleanDirectories("./src/**/obj/");
        CleanDirectories("./test/**/bin/");
        CleanDirectories("./test/**/obj/");
        CleanDirectory("./artifacts");
    });   

Task("build")
    .IsDependentOn("clean")
    .Does(() => {
        DotNetCoreRestore("./src/Shorthand.Geodesy/Shorthand.Geodesy.csproj");
        
        var buildSettings = new DotNetCoreBuildSettings {
            Configuration = "Release",
            VersionSuffix = versionSuffix
        };

        DotNetCoreBuild("./src/Shorthand.Geodesy/Shorthand.Geodesy.csproj", buildSettings);
    });

Task("pack")
    .Does(() => {
        var packSettings = new DotNetCorePackSettings{
            Configuration = "Release",
            OutputDirectory = output,
            VersionSuffix = versionSuffix
        };

        DotNetCorePack("./src/Shorthand.Geodesy/Shorthand.Geodesy.csproj", packSettings);
    });

Task("test")
    .Does(() => {
        var settings = new DotNetCoreTestSettings { };

        DotNetCoreRestore("./tests/Shorthand.Geodesy.Tests/Shorthand.Geodesy.Tests.csproj");                
        DotNetCoreTest("./tests/Shorthand.Geodesy.Tests/Shorthand.Geodesy.Tests.csproj", settings);
    });

RunTarget(target);