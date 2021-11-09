$AllDumpFiles = Get-ChildItem -Path C:\ -Filter *.dmp -Recurse -File
if($AllDumpFiles){
	Write-Output "Files Found"
}
else{
	Write-Output "No Files Found"
}