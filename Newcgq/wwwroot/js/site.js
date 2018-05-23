// Write your JavaScript code.
function showDevice() {
    var deviceList = $('#deviceList');
    var deviceId;
    $.getJSON(
        '/Device/DeviceList',
        function (response) {
            deviceList.html("");
            var myHTML = "";
            $.each(response, function (i, item) {
                myHTML += "<option value='" + item + "'>" + item + "</option>";
            })
            deviceList.append(myHTML);
            deviceId = response[0];
            showModuel(deviceId);
            init(deviceId);
        }
    )

}

function showModuel(deviceId) {
    var moduelList = $('#moduelList');
    $.getJSON(
        '/Device/ModuelList',
        { deviceId: deviceId },
        function (response) {
            moduelList.html("");
            var myHTML = "";
            for (item in response) {
                var icon=item.substring(0, 2).toUpperCase();
                if (response[item] == true)
                    myHTML += "<option value='" + icon + "'>" + icon + "</option>";
            }
            moduelList.append(myHTML);
        }
    )
}

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

showDevice();

$('#back').click(function () {
    window.location = '/Device/Index';
})


$(document).ready(function () {
    var nav = $('#nav'); 
    $.getJSON(
        '/Home/NavController',
        function (response) {
            nav.html("");
            var nav_left = '<ul id="noadmin"><li> 首页 <a href = "/Device/Index" ></a></li><li>设备管理<a href="/Device/Device"></a></li><li>模块管理<a href="/Device/Moduel"></a></li><li>历史记录<a href="/Device/History"></a></li></ul >';
            var nav_right = '<span id="user_quit"><a> 退出登录</a></span >'
            if (response === 'user')
                nav.append(nav_left + nav_right);
            if (response === 'admin')
                nav.append(nav_right);
            if (response === 0) 
                nav.append("<h1>大数据与人工智能平台</h1>");

            $('#user_quit a').click(function () {
                $.getJSON(
                    '/Home/Quit',
                    function () {
                        window.location = '/Home/Index';
                    }
                )
            })
        }
    )
})

