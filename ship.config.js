const fs = require('fs');
const path = require('path');
const { XMLParser, XMLBuilder, XMLValidator } = require('fast-xml-parser');

module.exports = {

    versionUpdated: ({version, releaseType, dir, exec}) => {
        fs.appendFileSync("ship.log", `versionUpdated called. version: ${version}`);

        const directoryBuildPropsPath = path.resolve(dir, 'Directory.Build.props');
        const xmlSource = fs.readFileSync(directoryBuildPropsPath, 'utf-8').toString();

        fs.appendFileSync("ship.log", xmlSource);

        const parser = new XMLParser({
            ignoreAttributes: false,
            preserveOrder: true
        });

        const xml = parser.parse(xmlSource);

        fs.appendFileSync(JSON.stringify(xml));

        const builder = new XMLBuilder({
            ignoreAttributes: false,
            preserveOrder: true,
            format: true
        })
    },

    buildCommand: () => null
};
