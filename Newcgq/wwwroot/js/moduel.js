var moduel_add = $('#moduel_add');
var moduel_del = $('#moduel_del');
var moduelInfo = $('#moduelInfo');
var save = $('#moduel_save');
var back = $('#moduel_back');


function init(deviceId) {
    var ActiveList = $('#ActiveList');
    var NegativeList = $('#NegativeList');
    $.ajaxSettings.async = false;  
    $.getJSON(
        '/Device/ModuelList',
        { deviceId: deviceId },
        function (response) {
            ActiveList.html("");
            NegativeList.html("");
            var aHTML = "";
            var nHTML = "";
            for (item in response) {
                var icon = item.substring(0, 2).toUpperCase();
                if (response[item] == true) {
                    aHTML += '<li><input type = "checkbox" name = "moduel_active" id="' + item + '"><span></span>'
                        + icon + '模块</li > '
                }
                else {
                    nHTML += '<li><input type = "checkbox" name = "moduel_negative" id="' + item + '"><span></span>'
                        + icon + '模块</li > '
                }
            }
            ActiveList.append(aHTML);
            NegativeList.append(nHTML);
        }
    )
}

var deviceList = $('#deviceList');
deviceList.change(function () {
    var deviceId = deviceList.val();
    init(deviceId);
})

function saveModuel(deviceId) {
    var ad = $('#adModuel').get(0).checked
    var da = $('#daModuel').get(0).checked
    var io = $('#ioModuel').get(0).checked

    $.getJSON(
        '/Device/SaveModuel',
        {
            deviceId: deviceId,
            ad: ad,
            da: da,
            io: io
        }
    )
    init(deviceId);
}

moduel_add.click(function () {
    var deviceId = $('#deviceList').val();
    saveModuel(deviceId);
})

moduel_del.click(function () {
    var deviceId = $('#deviceList').val();
    saveModuel(deviceId);
})

save.click(function () {
    var result = moduelInfo.text();
    $.getJSON(
        '/Device/SaveModuel',
        { result: result }
    )
})