Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd ./client | yarn dev --port 50001"
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd ./user-client | yarn dev --port 50002"
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd ./server | dotnet watch run"


