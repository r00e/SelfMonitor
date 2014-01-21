if(Test-path .\package){
    rm -r -force .\package
}
else{
    mkdir .\package
}

cp -r ..\SimpleExample\Homework.App\Homework.App\bin .\package