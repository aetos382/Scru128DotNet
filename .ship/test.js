const path = require('node:path');
const dir = path.normalize(path.join(__dirname, '..'));

const { setVersion } = require('./setVersion.js');
setVersion('1.2.3.4', dir);
