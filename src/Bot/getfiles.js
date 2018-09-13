var glob = require("glob")

export default GetFiles = (filename, options) => {
    glob(filename, options, function (er, files) {
        return files
    })
}
