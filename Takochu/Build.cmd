cd ../
pushd %CD%
cd References
cmd /c Update
popd
nuget restore
msbuild Takochu.sln -p:Configuration=Release
pushd %CD%
cd Takochu\bin\Release
7z a ../../../Release.zip *.* -r
