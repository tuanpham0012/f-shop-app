Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd ./client | yarn dev"
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd ./server | dotnet watch run"

