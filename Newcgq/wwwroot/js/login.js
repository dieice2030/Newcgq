var user_input = document.getElementById('username');
var pwd_input = document.getElementById('password');
var valid_input = document.getElementById('validation');
var form_content = document.getElementsByClassName('form__wrapper');
var btn = document.getElementsByTagName('button')[0];
var html = document;
var warning = document.getElementById('warning');

// 因为动效一样，所以可以抽象出来（但是一开始是拿email测试的，所以命名。。。）
// 将label文字缩放的时候，会使其不能做对齐，所以需要一个向左的偏移量与之抵消
function getfocused(e, scalex,lineWidth) {
    e.onfocus = function () {
        var email = this.nextElementSibling;
        var email_text = email.firstElementChild;
        email_text.style.transform = 'translate(' + scalex + ',-10px) scale(0.8,0.8)';
        email_text.style.transition = 'all 200ms linear';
        var line = email.lastElementChild;
        var lineStyle = line.style;
        lineStyle.position = 'absolute';
        lineStyle.bottom = '0';
        lineStyle.width = lineWidth;
        lineStyle.height = '2px';
        lineStyle.background = '#0d8aee';
        lineStyle.transition = '';
    }
}

function losefocused(e) {
    e.onblur = function () {
        var email = this.nextElementSibling;
        var email_text = email.firstElementChild;
        var line = email.lastElementChild;
        var lineStyle = line.style;
        lineStyle.position = 'absolute';
        lineStyle.bottom = '0';
        lineStyle.width = '0';
        lineStyle.height = '2px';
        lineStyle.background = '#0d8aee';
        lineStyle.transition = 'width 300ms linear';
        if (e.value.length === 0) {
            email_text.style.transform = 'translateY(0) scale(1,1)';
            email_text.style.transition = 'all 200ms linear';
        }

    }
}

// 向左的偏移量大概就是字符串的长度
getfocused(user_input, '-6px','100%');
losefocused(user_input);
getfocused(pwd_input, '-4px', '100%');
losefocused(pwd_input);
getfocused(valid_input, '-6px', '70%');
losefocused(valid_input);

html.addEventListener('click',function () {
    var isEmpty = 0;
    warning.text('');
    for (i = 0; i < form_content.length; i++) {
        if (form_content[i].firstElementChild.value.length === 0) {
            isEmpty++;
        }
    }
    if (isEmpty === 0) {
        btn.style.animation = 'pulse 600ms';
        btn.style.animationIterationCount = '3';
    }
})
// 动画结束后移除动画，以便每次符合条件都能够触发动画
btn.addEventListener("animationend", function () { btn.style.animation = '' }); 