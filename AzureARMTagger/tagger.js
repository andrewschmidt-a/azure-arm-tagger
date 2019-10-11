var path = require('path');
var glob = require('glob');
var fs = require('fs');

function addTagsToFile(file, tags){
    var template = JSON.parse(fs.readFileSync(file));
    template['resources'].forEach(element => {
        if(element['tags'] != undefined){
            for (const [key, value] of Object.entries(tags)) {
                element['tags'][key] = value;
            }
        }
    });
    fs.writeFileSync(file, JSON.stringify(template, null, 2));
}

module.exports = function main(pathToTemplateFile, pathToTagFile, workingDirectory = "./"){
    var tags = JSON.parse(fs.readFileSync(path.join(workingDirectory,pathToTagFile)));

    pathToTemplateFile = path.join(workingDirectory,pathToTemplateFile)
    pathToTagFile = path.join(workingDirectory,pathToTagFile)

    if(fs.lstatSync(pathToTemplateFile).isDirectory()){
        glob("*.json", {cwd:pathToTemplateFile}, function (er, files) {
            files.forEach(file => {
                console.log("Processing... "+file)
                addTagsToFile(path.join(pathToTemplateFile, file), tags)
            });
        })
    }else{
        addTagsToFile(pathToTemplateFile, tags)
    }
}
