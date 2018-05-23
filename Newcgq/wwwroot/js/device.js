function delDevice() {
    var btn = document.getElementsByClassName('button_mini');
    for (var i = 0; i < btn.length; i++) {
        btn[i].addEventListener('click', function () {
            var deviceId = this.value;
            $.getJSON(
                "/Device/DelDevice",
                { deviceId: deviceId },
            )
            var thisLi = this.parentElement;
            $(thisLi).remove();
        });
    }
}
$(document).ready(function () {
    var showdevice = $("#showdevice");
    var test;
    $.getJSON(
        "/Device/ShowDevice",
        function (result) {
            showdevice.html("");
            var myHTML = "";
            $.each(result, function (i, item) {
                myHTML += "<li>" + item + "<button class='button_mini btn_soft' value='"+item+"'>删除</button></li>";
            })
            showdevice.append(myHTML);
            delDevice();
        }
    );

    $('#device_add').click(function () {
        var deviceId = $('#device_text').val().trim();
        if (deviceId == '') {
            alert("请输入设备号");
        }
        else {
            $.getJSON(
                '/Device/AddDevice',
                { deviceId: deviceId },
                function (response) {
                    if (response == "1") {
                        var newLi = "<li>" + deviceId + "<button class='button_mini  btn_soft' value='" + deviceId + "'>删除</button></li>";
                        showdevice.append(newLi);
                        delDevice();
                    }
                    else if (response == "-1") {
                        alert("设备已经存在，请勿重复添加");
                    }
                }
            )
        }
    })
})


