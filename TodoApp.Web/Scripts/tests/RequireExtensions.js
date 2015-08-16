function requireForTesting(dependency, callback) {
    beforeEach(function (done) {
        require([dependency], function (sutType) {
            callback(sutType);
            done();
        });
    });

    afterEach(function () {
        require.undef(dependency);
    });
}

function defineForTesting(dependency, obj) {
    beforeEach(function () {
        define(dependency, obj);
    });

    afterEach(function () {
        require.undef(dependency);
    });
}