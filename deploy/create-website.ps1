Import-Module webadministration

if(test-path IIS:\AppPools\DemoAppPool){
    Remove-WebAppPool DemoAppPool
}
New-WebAppPool DemoAppPool
Set-ItemProperty IIS:\AppPools\DemoAppPool managedRuntimeVersion v4.0

if(test-path IIS:\Sites\DemoSite){
    Remove-WebSite -Name "DemoSite"
}
New-WebSite -Name DemoSite -PhysicalPath C:\my\SelfMonitor\deploy\package -Port 9999 
Set-ItemProperty IIS:\Sites\DemoSite -name applicationPool -value DemoAppPool