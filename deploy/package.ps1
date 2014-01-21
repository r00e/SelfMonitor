if(Test-path .\package){
    write-host "remove existed package"
    rm -r -force .\package
    write-host "create new package"
    mkdir .\package
}
else{
    write-host "create new package"
    mkdir .\package
}

write-host "copy bin folder"
cp -r ..\SimpleExample\Homework.App\Homework.App\bin .\package
write-host "copy Views"
cp -r ..\SimpleExample\Homework.App\Homework.App\Views .\package
write-host "copy packages.config"
cp ..\SimpleExample\Homework.App\Homework.App\packages.config .\package
write-host "copy Global.asax"
cp ..\SimpleExample\Homework.App\Homework.App\Global.asax .\package
write-host "copy web.config"
cp ..\simpleExample\homework.app\homework.app\web.config .\package