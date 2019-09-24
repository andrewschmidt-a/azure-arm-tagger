# azure-arm-tagger

This Utility will help to apply Tags (from json file) into an ARM Template (json file(s))


to get started run 
```
dotnet build AzureARMTaggerC#/AzureARMTagger.csproj
dotnet run Azure ARMTaggerC#/AzureARMTagger.csproj "FILE_TEMPLATE_PATH" "FILE_TAGS_PATH"
```
or...
```
node ARMTagger/tagger.js "FILE_TEMPLATE_PATH" "FILE_TAGS_PATH"
```
or use docker...
```
docker build . -t nemcrunchers/azure-arm-tagger
docker run -v :/app/volume  -e pathToTemplateFile=dir -e pathToTagFile=tags.json -it nemcrunchers/azure-arm-tagger
```
