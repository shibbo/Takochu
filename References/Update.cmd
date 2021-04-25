git submodule init
git submodule update
cd SuperBMD
git pull https://github.com/Sage-of-Mirrors/SuperBMD.git master
nuget restore
cd ..\
cd GL_EditorFramework
git pull https://github.com/jupahe64/GL_EditorFramework.git master
nuget restore
cd ..\
