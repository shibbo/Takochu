nuget restore
pushd %CD%
cd References
cmd /c Update
cd GL_EditorFramework
git remote add upstream https://github.com/Lord-Giganticus/GL_EditorFramework.git
git fetch upstream
git merge upstream/master master
popd
msbuild Takochu.sln -p:Configuration=Release
pushd %CD%
cd Takochu\bin\Release
7z a ../../../Release.zip *.* -r