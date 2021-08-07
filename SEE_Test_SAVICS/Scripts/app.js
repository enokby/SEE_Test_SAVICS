var records = [];
$(function () {
    get();
});

var get = function () {
    $.ajax({
        url: 'http://localhost:8484/api/1.0/emr',
        contentType: "application/json"
    }).done(function (data) {
        records = data;
        $('#Save').click(post);
        $('#Search').keyup(find);
        $('#minors').change(find);
        show();
    });
}

var find = function () {
    var keyword = $('#Search').val();
    var minors = $('#minors').prop('checked') ? "&minors=1" : "&minors=0";
    $.ajax({
        url: 'http://localhost:8484/api/1.0/emr?q=' + keyword + minors,
        contentType: "application/json"
    }).done(function (data) {
        records = data;
        show();
    });
}

var show = function () {
    var html = '';
    for (var i = 0; i < records.length; i++) {
        html += '<tr><td>' + records[i].FirstName + ' ' + records[i].LastName + ' (' + records[i].Gender + '), ' + records[i].Age + ' - ' + records[i].City + '(' + records[i].Country+ ')</td></tr>';
    }
    $('#List').html(html);
};

var post = function () {
    var query = {
        FirstName: $('#FirstName').val(),
        LastName: $('#LastName').val(),
        Gender: $("input[name='Gender']:checked").val(),
        Age: $('#Age').val(),
        City: $('#City').val(),
        Country: $('#Country').val(),
        Diabetes: $("input[name='Diabetes']:checked").val()
    };
    $.ajax({
        type: 'POST',
        url: 'http://localhost:8484/api/1.0/emr',
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(query)
    }).done(function (data) {
        get();
    });
}