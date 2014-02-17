$packagePath = ".\package"

$srcFile = @{
    "binFolder" = "..\SimpleExample\Homework.App\Homework.App\bin";
    "viewsFolder" = "..\SimpleExample\Homework.App\Homework.App\Views";
    "packagesConfig" = "..\SimpleExample\Homework.App\Homework.App\packages.config";
    "globalAsax" = "..\SimpleExample\Homework.App\Homework.App\Global.asax";
    "webConfig" = "..\simpleExample\homework.app\homework.app\web.config";
}

Function DeleteIfExisted($target){
    if(Test-path $target){
        write-host "remove existed $target"
        rm -r -force $target
    }
}

Function CreateFolder($folder){
    write-host "create new $folder"
    mkdir $folder
}

Function CopySrcFileTo($dest) {
    $srcFile[$srcFile.keys] | % { cp -r $_ $dest }
}

Function CreatePackage {
    DeleteIfExisted -target $packagePath
    CreateFolder -folder $packagePath
    CopySrcFileTo -dest $packagePath
}

CreatePackage

# write-host "copy bin folder"
# cp -r ..\SimpleExample\Homework.App\Homework.App\bin .\package
# write-host "copy Views"
# cp -r ..\SimpleExample\Homework.App\Homework.App\Views .\package
# write-host "copy packages.config"
# cp ..\SimpleExample\Homework.App\Homework.App\packages.config .\package
# write-host "copy Global.asax"
# cp ..\SimpleExample\Homework.App\Homework.App\Global.asax .\package
# write-host "copy web.config"
# cp ..\simpleExample\homework.app\homework.app\web.config .\package