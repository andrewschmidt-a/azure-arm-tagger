FROM node
RUN mkdir /app && mkdir /app/volume
WORKDIR /app
COPY ./AzureARMTagger /app
RUN npm install
ENTRYPOINT node -e 'require("./tagger.js")(process.env.pathToTemplateFile, process.env.pathToTagFile, "/app/volume")'