var JasmineConsoleReporter = function (onlyErrors) {
    function quoteAttr(msg) {
        return (msg + '')
            .replace(/&/g, '&amp;')
            .replace(/'/g, '&apos;')
            .replace(/"/g, '&quot;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;')
            .replace(/\r\n/g, '&#13;')
            .replace(/[\r\n]/g, '&#13;');
    }

    function writeStartMessage(name) {
        onlyErrors && console.log("<testStarted name='" + quoteAttr(name) + "' />");
    }

    function writeFailureMessage(name, message) {
        console.log("<testFailed name='" + quoteAttr(name) + "' message='" + quoteAttr(message) + "' />");
    }

    function writeFinishedMessage(name) {
        onlyErrors && console.log("<testFinished name='" + quoteAttr(name) + "' />");
    }

    function writeDoneMessage() {
        console.log('<suiteFinished />');
    }

    return {
        specStarted: function (result) {
            writeStartMessage(result.fullName);
        },

        specDone: function (result) {
            for (var i = 0; i < result.failedExpectations.length; i++) {
                writeFailureMessage(result.fullName, result.failedExpectations[i].message);
            }

            writeFinishedMessage(result.fullName);
        },
        jasmineDone: function () {
            writeDoneMessage();
        }
    };
};