FROM node
RUN mkdir /app && mkdir /app/volume
WORKDIR /app
COPY ./AzureARMTagger /app
ENTRYPOINT node -e 'require("./tagger.js")(process.env.pathToTemplateFile, process.env.pathToTagFile, "/app/volume")'