msbuild /t:restore SceneJect.sln
%NUGET% restore SceneJect.sln -NoCache -NonInteractive
dotnet restore SceneJect.sln
msbuild SceneJect.sln /p:Configuration=Release