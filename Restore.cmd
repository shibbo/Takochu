nuget restore
pushd %CD%
cd References
cmd /c Update
cd GL_EditorFramework
git remote add upstream https://github.com/Lord-Giganticus/GL_EditorFramework.git
git fetch upstream
git merge upstream/master master
popd