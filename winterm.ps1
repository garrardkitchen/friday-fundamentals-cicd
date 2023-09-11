# Open Windows Terminal with configured tabs

# $rootDir = "$env:USERPROFILE\source\fj\friday-fundamentals-cicd"
$rootDir = $pwd

wt --tabColor '#009999' -p "Windows PowerShell" --startingDirectory "$rootDir\src\Web" `; sp --tabColor '#009999' -H -p "Windows PowerShell" --startingDirectory "$rootDir\src\api" `; sp --tabColor '#009999' -V -p "Windows PowerShell" --startingDirectory "$rootDir\test\unittesting_api" `; mf first `; sp -V --tabColor '#009999' -p "Windows PowerShell" --startingDirectory "$rootDir\test\End2EndTesting" `; mf first