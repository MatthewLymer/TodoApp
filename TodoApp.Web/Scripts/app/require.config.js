require.config({
    paths: {
        'jquery': 'lib/jquery-1.11.3.min',
        'knockout': 'lib/knockout-3.3.0.min'
    },
    map: {
        "*": {
            "ko": "knockout"
        }
    },
});