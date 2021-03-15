git submodule init
git submodule update
cd SuperBMD
git pull https://github.com/Sage-of-Mirrors/SuperBMD.git master
nuget restore
msbuild SuperBMD.sln -p:Configuration=Release
cd ..\
copy "SuperBMD\SuperBMDLib\bin\Release\SuperBMDLib.dll" %CD%
cd Spotlight
nuget restore
cd ..\
cd GL_EditorFramework
git pull https://github.com/jupahe64/GL_EditorFramework.git master
nuget restore
msbuild GL_EditorFramework.sln -p:Configuration=Release
cd ..\
copy "GL_EditorFramework\Gl_EditorFramework\bin\Release\GL_EditorFramework.dll" %CD%
