
cd /ImaginaryCTF
sudo cp appsettings.json "src/iCTF Website"
sudo cp appsettings.json "src/iCTF Discord Bot"
sudo cp appsettings.json "src/iCTF Shared Resources" # useless

cd src
/root/.dotnet/dotnet publish -c release
