function post(url, data) {
    return fetch(url, {
        body: JSON.stringify(data),
        headers: { "Content-Type": "application/json" },
        credentials: "same-origin"
    })
    .then(res => res.json())
}

function createTask() {
    return post('/api/ListApi/createTask', { nameCreate: $('#nameCreate').val() })
    .then(data => {
        console.info(data);
    });
}
        
//    $.post(
//        '/api/ListApi/createTask',
//        { nameCreate: $('#nameCreate').val() },
//        function (data) {
//            console.info(data);
//        },
//        'JSON'
//        );
//    return false;
//}

