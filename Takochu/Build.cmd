nuget restore
pushd %CD%
cd ../References
cmd /c Update
popd
msbuild Takochu.sln -p:Configuration=Release
pushd %CD%
cd Takochu\bin\Release
7z a ../../../Release.zip *.* -r
