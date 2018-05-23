$(document).ready(function () {
    var deviceList = $('#deviceList');

    deviceList.change(function () {
        var deviceId = deviceList.val();
        showModuel(deviceId);
    })
})

$('#sendMessage').click(function () {
    var deviceId = $('#deviceList').val();
    var func = $('#moduelList').val();
    var interval = $('#interval').val();
    var intervalUnit = $('#intervalUnit').val();
    var data = $('#data').val();
    $.getJSON(
        '/Device/ControllerMessage',
        {
            deviceId:deviceId,
            func: func,
            interval: interval,
            intervalUnit: intervalUnit,
            data:data
        }
    )
})

$('#moduelList').change(function () {
    if ($('#moduelList').val() === 'AD') {
        $('#data').attr('disabled',false)
    }
    else {
        $('#data').prop('disabled', true);
    }
})

