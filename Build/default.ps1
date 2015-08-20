properties {
	$configuration = 'Release'
	$outputDirectory = '..\output'
	$solutionDirectory = '..\'
	$robocopyPath = 'C:\Windows\System32\robocopy.exe'
}

function Normalize-Path($path) {
	if ($path.StartsWith('\')) {
		$path = $pwd.Drive.Name + ':' + $path
	}
	elseif (!$outputDirectory.Contains(':')) {
		$path = Join-Path $pwd $path
	}	
	
	$path
}

function Build-Project($projectName) {
	$projectPath = Join-Path (SolutionDirectory) "$projectName\$projectName.csproj"
	$outDir = Join-Path (OutputDirectory) $projectName

	exec {
		msbuild $projectPath `
			/t:'Clean,Build' `
			/p:Configuration=$configuration `
			/p:OutDir=$outDir
	}
}

function Build-WebProject($projectName) {
	$projectPath = Join-Path (SolutionDirectory) "$projectName\$projectName.csproj"
	$outDir = Join-Path (OutputDirectory) $projectName

	exec {
		msbuild $projectPath `
			/t:'Clean,WebPublish' `
			/p:Configuration=$configuration `
			/p:WebPublishMethod=FileSystem `
			/p:PublishUrl=$outDir `
			/p:VisualStudioVersion=12.0
	}
}

function Run-TestAssembly($projectName) {
	$assemblyPath = Join-Path (OutputDirectory) "$projectName\$projectName.dll"

	exec {
		..\tools\nunit\nunit-console.exe $assemblyPath
	}
}

function OutputDirectory {
	Normalize-Path $outputDirectory	
}

function SolutionDirectory {
	Normalize-Path $solutionDirectory
}

task Default -Depends RunTests, Package

task Package -Depends CompileApp {
	# zip it here maybe
}

task CompileApp -Depends NugetPackageRestore {
	Build-WebProject 'TodoApp.Web'
}

task RunTests -Depends RunJasmineTests, CompileTests {
	# do nothing
}

task CompileTests -Depends NugetPackageRestore {
	# do nothing
}

task RunJasmineTests {
	exec {
		pushd ..\TodoApp.Web\Scripts

		try
		{
			..\..\tools\phantomjs\phantomjs.exe run-jasmine.js SpecRunner.html
		}
		finally
		{
			popd
		}
	}
}

task NugetPackageRestore {
	$solutionPath = Join-Path (SolutionDirectory) "TodoApp.sln"

	Write-Host $solutionPath

	exec {
		..\tools\nuget\nuget.exe restore $solutionPath
	}
}