Date.prototype.format = function (fmt) {
    var o = {
        "y": this.getFullYear(),
        "M+": this.getMonth() + 1,                 //月份 
        "d+": this.getDate(),                    //日 
        "h+": this.getHours(),                   //小时 
        "m+": this.getMinutes(),                 //分 
        "s+": this.getSeconds(),                 //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds()             //毫秒 
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
}


var data = [
    { name: '2016/12/18 6:38:08', value: ['2016/12/18 6:38:08', 80] },
    { name: '2016/12/18 16:18:18', value: ['2016/12/18 16:18:18', 60] },
    { name: '2016/12/18 19:18:18', value: ['2016/12/18 19:18:18', 90] }
];


$(document).ready(function () {
    var now = new Date().format("yyyy-MM-dd");
    $("#dateBegin").attr("max", now);
    $("#dateStop").attr("max", now);
    $("#dateStop").val(now);
    var xData = [];
    var yData = [];
    var chartData = [];
    var test = [];

    $("#dateButton").click(function () {
        var begin = $("#dateBegin").val();
        var stop = $("#dateStop").val();
        var moduel = $('#moduel_title').text();
        $.getJSON(
            "/Device/HistoryData",
            {
                begin: begin,
                stop: stop,
                moduel:moduel
            },
            function (response) {
                chartData = response;
            }
        )

        for (var i = 0; i < 10; i++) {
            test[i] = chartData[i];
        }

        var myChart = echarts.init(document.getElementById('main'));

        // 指定图表的配置项和数据
        option = {
            xAxis: {
                type: 'time',
            },
            yAxis: {
                type: 'value',
                min: 'dataMin'
            },
            series: [{
                data: chartData,
                type: 'line'
            }]
        };

        // 使用刚指定的配置项和数据显示图表。
        myChart.setOption(option);
    })
})

var moduel_list = $('#moduel_list');
var moduel_title = $('#moduel_title');
var moduel_ul = $('#moduel_list ul');
moduel_title.mouseover(function () {
    var dataVisual = $('#DataVisual');
    var exportAll = $('#exportAll');
    var deviceId = '4e0003';
    $.getJSON(
        '/Device/ModuelList',
        { deviceId: deviceId },
        function (response) {
            moduel_ul.html('');
            var myHTML = '';
            for (item in response) {
                var icon = item.substring(0, 2).toUpperCase();
                if (response[item] == true) {
                    myHTML += '<li id="' + item + '">' + icon + '模块</li > '
                }
            }
            moduel_ul.append(myHTML);
            moduel_list.css('height', '200px');
        }
    )
    $('#adModuel').click(function () {
        moduel_title.html('AD模块');
        dataVisual.attr('action', '/Device/ExportAd');
        exportAll.attr('action', '/Device/ExportAdAll');
    })
    $('#daModuel').click(function () {
        moduel_title.html('DA模块');
        dataVisual.attr('action', '/Device/ExportDa');
        exportAll.attr('action', '/Device/ExportDaAll');
    })
    $('#ioModuel').click(function () {
        moduel_title.html('IO模块');
        dataVisual.attr('action', '/Device/ExportIo');
        exportAll.attr('action', '/Device/ExportIoAll');
    })
})
moduel_list.mouseleave(function () {
    moduel_list.css('height', 0);
})