# Path to the MSDeploy executable
$msdeployPath = "C:\Program Files (x86)\IIS\Microsoft Web Deploy V3\msdeploy.exe"

# Server and credentials
$publishProfile = @{
    publishUrl = "site8091.siteasp.net"
    msdeploySite = "site8091"
    username = "site8091"
    password = "mL_8#c9FeB-2"  # Use your actual password
    port = 8172
}

# Path to your published release folder
$publishFolder = "D:\backend\ecommerce\api\bin\Release\net8.0\publish"

# Source and destination settings
$source = "contentPath='$publishFolder'"  # Path to your published release folder
$destination = "contentPath=$($publishProfile.msdeploySite),ComputerName=""https://$($publishProfile.publishUrl):$($publishProfile.port)/msdeploy.axd?site=$($publishProfile.msdeploySite)"",UserName=""$($publishProfile.username)"",Password=""$($publishProfile.password)"",AuthType=""Basic"""

# Execute MSDeploy command
& $msdeployPath -verb:sync `
    -source:$source `
    -dest:$destination `
    -allowUntrusted
