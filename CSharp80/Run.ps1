<#
    .SYNOPSIS
        Compiles and executes a project of the given name $ProjectName.
    .DESCRIPTION
        Compiles and executes a project of the given name $ProjectName with the gien language version $LangVersion.
    .PARAMETER ProjectName
        The name of the project to be compiled and executed.
    .PARAMETER LangVersion
        The C# version that will be used to compile a project of the given name $ProjectName.
    .PARAMETER DefineSection
        Pre-defined section to be excuted.
    .EXAMPLE
        Execute-Example -ProjectName 01.AsyncMain -LangVersion 7.1 -DefineSection CSharp70_ASYNC
    .OUTPUT
        Project name: 01.AsyncMain
        Language version: C#7.1
        Code executed: CSharp70_ASYNC

        Results:


        ActionImpl1(param: A, delay: 1282645759)
        ActionImpl2(param: B, delay: 1182199419)

        Elapsed time: 0,44s        
#>
function global:Execute-Example
{
    [CmdletBinding(PositionalBinding = $false)]
	Param (
        [Parameter(Mandatory = $true)]
        [ValidateSet('00.Example', '01.AsyncMain', '02.DefaultKeyword', '03.InferredTupleElementNames', '04.SafeEfficientCodeEnhancements', 
            '05.NonTrailingNamedArguments', '06.LeadingUnderscoresInNumericLiterals', '07.PrivateProtectedAccessModifier',
            '08.ConditionalRefExpressions',  '09.EnablingMoreEfficientSafeCode', '10.MakeExistingFeaturesBetter', 
            '11.NullableReferenceTpes', '12.AsyncStreams', '13.RangesAndIndices', '14.DefaultInterfaceMembers', 
            '15.RecursivePatterns', '16.SwitchExpressions', '17.TargetTypedNewExpressions', '18.ExtensionEverything')]
        [ValidateNotNullOrEmpty()]
        [string]$ProjectName
    ,
        [Parameter(Mandatory = $false)]
        [ValidateSet('7.0', '7.1', '7.2', '8.0')]
        [string]$LangVersion = '7.0'
    )

    DynamicParam {
        [System.Collections.ObjectModel.Collection[System.Attribute]]$attributeCollection = `
            [System.Collections.ObjectModel.Collection[System.Attribute]]::new()

        [System.Management.Automation.ParameterAttribute]$defineParamAttr = `
            [System.Management.Automation.ParameterAttribute]::new()
        $defineParamAttr.Mandatory = $false
        $defineParamAttr.HelpMessage = "Please supply a file name"

        [System.String[]]$defineValidateSet = @()
        Switch ($ProjectName)
        {
            '00.Example' {
                $defineValidateSet = @('_01_CSharp70_EXAMPLE', '_02_CSharp71_EXAMPLE')
            }
            '01.AsyncMain' { 
                $defineValidateSet = @('_01_CSharp70_NON_ASYNC', '_02_CSharp70_TASK', '_03_CSharp70_ASYNC', 
                    '_04_CSharp70_ASYNC_AWAIT', '_05_CSharp71_ASYNC', '_06_CSharp71_ASYNC_AWAIT1', '_07_CSharp71_ASYNC_AWAIT2')
            }
            '02.DefaultKeyword' {
	            $defineValidateSet = @('CSharp70', 'CSharp80')
            }
            '03.InferredTupleElementNames' {
	            $defineValidateSet = @('CSharp70', 'CSharp80')
            }
            '04.SafeEfficientCodeEnhancements' {
	            $defineValidateSet = @('CSharp70', 'CSharp80')
            }
            '05.NonTrailingNamedArguments' {
	            $defineValidateSet = @('CSharp70', 'CSharp80')
            }
            '06.LeadingUnderscoresInNumericLiterals' {
	            $defineValidateSet = @('CSharp70', 'CSharp80')
            }
            '07.PrivateProtectedAccessModifier' {
	            $defineValidateSet = @('CSharp70', 'CSharp80')
            }
            '08.ConditionalRefExpressions' {
	            $defineValidateSet = @('CSharp70', 'CSharp80')
            }
            '09.EnablingMoreEfficientSafeCode' {
	            $defineValidateSet = @('CSharp70', 'CSharp80')
            }
            '10.MakeExistingFeaturesBetter' {
	            $defineValidateSet = @('CSharp70', 'CSharp80')
            }
            '11.NullableReferenceTpes' {
	            $defineValidateSet = @('_01_CSharp73_ON_NULL_EXCEPTION', '_02_CSharp80_DISABLE_NULLABLE',
                    '_03_CSharp80_ENABLE_NULLABLE', '_04_CSharp80_ENABLE_SAFEONLY', '_05_CSharp80_RESTORE',
                    '_06_CSharp80_CHECK_NULL', '_07_CSharp80_CHECK_IS_NULL')
            }
            '12.AsyncStreams' {
	            $defineValidateSet = @('CSharp70', 'CSharp80')
            }
            '13.RangesAndIndices' {
	            $defineValidateSet = @('CSharp70', 'CSharp80')
            }
            '14.DefaultInterfaceMembers' {
	            $defineValidateSet = @('CSharp70', 'CSharp80')
            }
            '15.RecursivePatterns' {
	            $defineValidateSet = @('CSharp70', 'CSharp80')
            }
            '16.SwitchExpressions' {
	            $defineValidateSet = @('CSharp70', 'CSharp80')
            }
            '17.TargetTypedNewExpressions' {
	            $defineValidateSet = @('CSharp70', 'CSharp80')
            }
            '18.ExtensionEverything' {
	            $defineValidateSet = @('CSharp70', 'CSharp80')
            }
        }
        [System.Management.Automation.ValidateSetAttribute]$defineValidateSetParamAttr = `
            [System.Management.Automation.ValidateSetAttribute]::new($defineValidateSet)

        $attributeCollection.Add($defineParamAttr)
        $attributeCollection.Add($defineValidateSetParamAttr)

        [System.Management.Automation.RuntimeDefinedParameter]$defineSectionParam = `
            [System.Management.Automation.RuntimeDefinedParameter]::new('DefineSection', [string], $attributeCollection)
        $defineSectionParam.Value = $defineValidateSet[0]
        [System.Management.Automation.RuntimeDefinedParameterDictionary]$paramDictionary = `
            [System.Management.Automation.RuntimeDefinedParameterDictionary]::new()
        $paramDictionary.Add($defineSectionParam.Name, $defineSectionParam)
        return $paramDictionary
    }

    begin
    {
        [string]$DefineSection = $PSBoundParameters['DefineSection']

        [string]$projectFullPath = [System.IO.Path]::Combine($PSScriptRoot, $ProjectName, "$ProjectName.csproj")
        [string]$programFullPath = [System.IO.Path]::Combine($PSScriptRoot, $ProjectName, 'Program.cs')

        # Replace language version in a *.csproj file
        Replace-InFile `
            -Path $projectFullPath `
            -Pattern '<LangVersion>\d.\d' `
            -Replacement "<LangVersion>$LangVersion"

        # Comment out every other (not $DefineSection) #define
        Replace-InFile `
            -Path $programFullPath `
            -Pattern $('^(\s*\/{2})?\s*#define (?!' + $DefineSection + ')') `
            -Replacement "// #define "
        # Uncomment every #define $DefineSection
        Replace-InFile `
            -Path $programFullPath `
            -Pattern $('^(\s*\/{2})?\s*#define ' + $DefineSection) `
            -Replacement "#define $DefineSection"
    }

    process
    {
        Write-Host -ForegroundColor Green `
            $("`nProject name: {0}`nLanguage version: C#{1}`nCode executed: {2}`n`nResults:`n`n" `
                -f $ProjectName, $LangVersion, $DefineSection)

	    [string[]]$output = dotnet build "$projectFullPath" --verbosity quiet

        [string[]]$outputErrors = $output | ? { $_.Contains('error') }
        [int]$outputErrorstLineCount = $outputErrors.Length / 2    

        if ($outputErrorstLineCount -gt 0)
        {
            for ($i = 0; $i -lt $outputErrorstLineCount; $i += 1)
            {
                [string]$outputLine = $outputErrors[$i]
                [int]$beginIdx = $outputLine.IndexOf(':') + 2
                [int]$endIdx = $outputLine.LastIndexOf('[')
                Write-Host -ForegroundColor Red $outputLine.Substring(
                    $beginIdx, $endIdx - $beginIdx)
            }
        }
        else
        {
            [string[]]$outputWarnings = $output | ? { $_.Contains('warning') }
            [int]$outputWarningstLineCount = $outputWarnings.Length / 2

            for ($i = 0; $i -lt $outputWarningstLineCount; $i += 1)
            {
                [string]$outputLine = $outputWarnings[$i]
                [int]$beginIdx = $outputLine.IndexOf(':') + 2
                [int]$endIdx = $outputLine.LastIndexOf('[')
                Write-Host -ForegroundColor Yellow $outputLine.Substring(
                    $beginIdx, $endIdx - $beginIdx)
            }

            Write-Host "`n`n"

            [Diagnostics.Stopwatch]$sw = [Diagnostics.Stopwatch]::StartNew()
	        dotnet run --project "$projectFullPath"
        }
    }

    end
    {
        if ($sw) 
        {
            $sw.Stop()
            Write-Host -ForegroundColor Green `
                $("`nElapsed time: {0:f}s" -f $($sw.Elapsed.Seconds + $sw.Elapsed.Milliseconds / 1000))
        }
        
    }
}

<#
    .SYNOPSIS
        Replaces all strings that match a given $Pattern with a given $Replacement in a file on a given path $Path.
    .DESCRIPTION
        Replaces all strings that match a specified regular expression $Pattern with a specified replacement string 
        $Replacement in a file on a given path $Path.
    .PARAMETER Path
        The full path to the file which content will be replaced.
    .PARAMETER Pattern
        The regular expression pattern to match.
    .PARAMETER Replacement
        The replacement string.
#>
function local:Replace-InFile
{
    [CmdletBinding(PositionalBinding = $false)]
	Param (
        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$Path
    ,
        [Parameter(Mandatory = $true)]
        [ValidateNotNull()]
        [string]$Pattern
    ,
        [Parameter(Mandatory = $true)]
        [ValidateNotNull()]
        [string]$Replacement
    )
        if ([System.IO.File]::Exists($Path))
        {
            [string]$fileContent = [System.IO.File]::ReadAllText($Path)
            $fileContent = [System.Text.RegularExpressions.Regex]::Replace(
                $fileContent, $Pattern, $Replacement, [System.Text.RegularExpressions.RegexOptions]::Multiline)
            [System.IO.File]::WriteAllText($Path, $fileContent)
        }
}