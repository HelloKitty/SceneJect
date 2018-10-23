msbuild /t:restore SceneJect.sln
dotnet restore SceneJect.sln
msbuild SceneJect.sln /p:Configuration=Release