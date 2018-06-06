var login = $('#btn__login');
var warning = $('#warning');
var validate = $('#img__validate');


login.click(function () {
    var username = $('#username').val();
    var password = $('#password').val();
    var usergroup = $('#usergroup').val();
    var valicode = $('#validation').val();
    $.getJSON(
        '/Home/Login',
        {
            username: username,
            password: password,
            valicode: valicode,
            usergroup: usergroup
        },
        function (response) {
            var result = JSON.stringify(response);
            switch (result) {
                case '0': warning.text('用户不存在'); break;
                case '-1': warning.text('密码错误'); break;
                case '-2': warning.text('验证码错误'); break;
                case '1': {
                    if(usergroup==='1')
                        location.href = '/Device/Index';
                    if (usergroup === '0')
                        location.href = '/Admin';
                    break;
                }
                default: break;
            }
        }
    )
})

validate.click(function () {
    location.reload();
})
