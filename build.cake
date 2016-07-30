#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
#tool nuget:?package=NUnitOrange

var target = Argument("task","Core");
var mode = Argument("mode","Release");
var slnDir = Argument("workspace","../");
var reportDir =Argument("output","./report/"+DateTime.Now.Year.ToString()+"/"+DateTime.Now.Month.ToString("D2"));
var popup = Argument("popup",true);

var allModeDir = string.Format(System.IO.Path.Combine(slnDir,"{0}bin{1}"),"**\\","\\"+mode);
var allTestAssemblyPaths = string.Format(allModeDir+"\\*.Tests.dll");
var reporterPath = "./tools/**/NUnitOrange.exe";
var outputReportFullPath = reportDir +"/report-"+ DateTime.Now.ToString("ddHHmmss")+".html";

Task("Clean")
.Does(()=>{
    CleanDirectories(allModeDir);
});

Task("Build")
.IsDependentOn("Clean")
.Does(()=>{
    var slnFiles = GetFiles(System.IO.Path.Combine(slnDir, "*.sln"));
    foreach(var sln in slnFiles)
    {
        DotNetBuild(sln, settings => settings.SetConfiguration(mode));
    }
});

Task("Core")
.IsDependentOn("Build")
.Does(()=>{
    var testAssemblies = GetFiles(allTestAssemblyPaths);
    NUnit3(testAssemblies);
})
.Finally(()=>{
  RunTarget("Report");
})
.OnError(()=>{
    
});

Task("Report").Does(()=>{
    if(FileExists("./TestResult.xml"))
    {
        var reporter = GetFiles(reporterPath);
        CreateDirectory(reportDir);
        StartProcess(reporter.First(),string.Format("{0} {1}","TestResult.xml",outputReportFullPath));
        DeleteFile("./TestResult.xml");
        if(popup)
        {
            var arg = "/c start \"\" \""+GetFiles(outputReportFullPath).First().FullPath+"\"";
            Information(arg);
            StartProcess("cmd",arg);
        }
    }
});


RunTarget(target);