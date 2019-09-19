import tl = require('azure-pipelines-task-lib/task');
var tagger = require("../AzureARMTagger/tagger.js")

async function run() {
    try {
        const pathToTemplateFile: string = tl.getInput('pathToTemplateFile', true);
        const pathToTagFile: string = tl.getInput('pathToTagFile', true);
        const workingDirectory: string = tl.getInput('workingDirectory', true);
        
        tagger(pathToTemplateFile, pathToTagFile, workingDirectory)
    }
    catch (err) {
        tl.setResult(tl.TaskResult.Failed, err.message);
    }
}

run();
