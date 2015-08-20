var system = require('system');

if (system.args.length !== 2) {
    console.log('Usage: run-jasmine.js URL');
    phantom.exit(1);
}

var page = require('webpage').create();

var failed = false;
var testFailedRegex = /^<testFailed /;
var doneMessageRegex = /^<suiteFinished \/>$/;

page.onConsoleMessage = function (msg) {
    system.stdout.writeLine(msg);

    if (doneMessageRegex.exec(msg)) {
        phantom.exit(failed ? 1 : 0);
    }
	
	if (testFailedRegex.exec(msg)) {
		failed = true;
	}
};

page.open(system.args[1], function (status) {
	console.log(status);

    if (status !== "success") {
        system.stdout.writeLine("Couldn't load the page: " + system.args[1]);
        phantom.exit(1);
    }
});