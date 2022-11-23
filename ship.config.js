const { setVersion } = require('./.ship/setVersion.js');

module.exports = {
    versionUpdated: ({version, releaseType, dir, exec}) => {
        setVersion(version, dir);
    },

    buildCommand: () => null
};
