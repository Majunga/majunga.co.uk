var assert = require('assert');
var dir = require('../../src/Bot/dir');


describe('dir', function () {
    describe('#GetFilenames()', function () {
        it('should return a list of files for a given direction and file wildcard', function () {
            assert.notEqual((dir.GetFilenames("./*.json")), undefined);
        });
    });
});