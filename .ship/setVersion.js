const fs = require('fs');
const path = require('path');
const { DOMParser, XMLSerializer } = require('@xmldom/xmldom');
const xpath = require('xpath');

module.exports = {
    setVersion: (version, dir, exec) => {
        const directoryBuildPropsPath = path.resolve(dir, 'Directory.Build.props');
        const xmlSource = fs.readFileSync(directoryBuildPropsPath, 'utf-8').toString();

        const dom = new DOMParser().parseFromString(xmlSource, 'text/xml');
        const versionElement = xpath.select('//PropertyGroup[@Label="Packaging"]/Version', dom);

        versionElement[0].textContent = version;

        const newContent = new XMLSerializer().serializeToString(dom);
        fs.writeFileSync(directoryBuildPropsPath, newContent);
    }
};
