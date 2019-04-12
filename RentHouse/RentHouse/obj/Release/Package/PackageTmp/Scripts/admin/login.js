$(function () {
    /**
     * jquery方法：addClass()
     * addClass() 方法向被选元素添加一个或多个类。该方法不会移除已存在的 class 属性，仅仅添加一个或多个 class 属性。
     * 如需添加多个类，请使用空格分隔类名。
     */
    $("#login").addClass("current");

    /**
     * 正则检验邮箱
     * email 传入邮箱
     * return true 表示验证通过
     */
    function check_email(email) {
        if (/^[\w\-\.]+@[\w\-]+(\.[a-zA-Z]{2,4}){1,2}$/.test(email)) {
            return true;
        }
    }

    /**
     * input 按键事件keyup
     */
    $("input[name]").keyup(function (e) {
        //禁止输入空格  把空格替换掉(空格的ASCII为32)
        if ($(this).attr('name') == "pass" && e.keyCode == 32) {
            $(this).val(function (i, v) {
                return $.trim(v);
            });
        }
        if ($.trim($(this).val()) != "") {
            $(this).nextAll('span').eq(0).css({ display: 'none' });
        }
    });

    //错误信息
    var succ_arr = [];

    /**
     * input失去焦点事件focusout
     * 这跟blur事件区别在于，他可以在父元素上检测子元素失去焦点的情况。
     */
    $(".login-input").focusout(function (e) {
        var msg = "";
        if ($.trim($(this).val()) == "") {
            if ($(this).attr('name') == 'loginname') {
                succ_arr[0] = false;
                msg = "用户名为空";
            } else if ($(this).attr('name') == 'password') {
                succ_arr[1] = false;
                msg = "密码为空";
            }
            layer.msg(JSON.stringify(msg));
        } 
    });

    

    layui.use('form', function () {
        var form = layui.form;

        form.on('submit(login)', function (data) {
            $.ajax({
                url: '../User/LoginSubmit',
                data: {
                    loginname: data.field.loginname,
                    password: data.field.password
                },
                type: 'post',
                success: function (data) {
                    if (data.Flag == true) {
                        location.href = "/home/index"
                    } else {
                        layer.alert(data.Message, { icon: 5, skin: 'layer-ext-moon' });
                    }
                }
            });
            return false;
        });
    });
});