#tool "nuget:?package=ReportGenerator&version=4.2.10"

#tool nuget:?package=Codecov&version=1.5.0
#addin nuget:?package=Cake.Codecov&version=0.6.0
#addin nuget:?package=Cake.Coverlet&version=2.3.4

var target = Argument("target", "default");
var configuration = Argument("configuration", "Release");
var testFramework = Argument("testFramework", "");
var framework = Argument("framework", "");

var buildDir = Directory("./src/Stubble.Extensions.StringFormatter/bin") + Directory(configuration);

var artifactsDir = Directory("./artifacts/");

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);

    CleanDirectory("./artifacts");
   
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetCoreRestore("./Stubble.Extensions.StringFormatter.sln");
});

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() =>
{
    var setting = new DotNetCoreBuildSettings {
        Configuration = configuration,
        ArgumentCustomization = args => args.Append("/property:WarningLevel=0") // Until Warnings are fixed in StyleCop
    };

    if(!string.IsNullOrEmpty(framework))
    {
        setting.Framework = framework;
    }

    var testSetting = new DotNetCoreBuildSettings {
        Configuration = configuration
    };

   
    DotNetCoreBuild("./src/Stubble.Extensions.StringFormatter/", setting);
   
});



Task("Pack")
   .IsDependentOn("Build")
    .Does(() =>
{
    var settings = new DotNetCorePackSettings
    {
        OutputDirectory = artifactsDir,
        NoBuild = true,
        Configuration = configuration,
    };

    DotNetCorePack("./src/Stubble.Extensions.StringFormatter/Stubble.Extensions.StringFormatter.csproj", settings);
});


Task("Default")
    .IsDependentOn("Pack");

RunTarget(target);