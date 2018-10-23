dotnet restore SceneJect.sln --configfile Nuget.config
%NUGET% restore SceneJect.sln -NoCache -NonInteractive
dotnet build SceneJect.sln --configuration Release