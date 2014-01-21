Import-Module webadministration

if(test-path IIS:\AppPools\DemoAppPool){
    Remove-Item -r -force IIS:\AppPools\DemoAppPool
}
New-Item -force IIS:\AppPools\DemoAppPool
Set-ItemProperty IIS:\AppPools\DemoAppPool managedRuntimeVersion v4.0

if(test-path IIS:\Sites\DemoSite){
    Remove-Item -r -force IIS:\Sites\DemoSite
}
New-Item -force IIS:\Sites\DemoSite -physicalPath C:\my\SelfMonitor\deploy\package -bindings @{protocol="http";bindingInformation=":9999:"}
Set-ItemProperty IIS:\Sites\DemoSite -name applicationPool -value DemoAppPool

# New-Item IIS:\Sites\DemoSite\DemoApp -physicalPath C:\DemoSite\DemoApp -type Application
# Set-ItemProperty IIS:\sites\DemoSite\DemoApp -name applicationPool -value DemoAppPool

# New-Item IIS:\Sites\DemoSite\DemoVirtualDir1 -physicalPath C:\DemoSite\DemoVirtualDir1 -type VirtualDirectory
# New-Item IIS:\Sites\DemoSite\DemoApp\DemoVirtualDir2 -physicalPath C:\DemoSite\DemoVirtualDir2 -type VirtualDirectory