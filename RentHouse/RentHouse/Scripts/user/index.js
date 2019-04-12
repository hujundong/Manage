
function cambiar_login() {
  document.querySelector('.cont_forms').className = "cont_forms cont_forms_active_login";  
document.querySelector('.cont_form_login').style.display = "block";
document.querySelector('.cont_form_sign_up').style.opacity = "0";               

setTimeout(function(){  document.querySelector('.cont_form_login').style.opacity = "1"; },400);  
  
setTimeout(function(){    
document.querySelector('.cont_form_sign_up').style.display = "none";
},200);  
  }

function cambiar_sign_up(at) {
  document.querySelector('.cont_forms').className = "cont_forms cont_forms_active_sign_up";
  document.querySelector('.cont_form_sign_up').style.display = "block";
document.querySelector('.cont_form_login').style.opacity = "0";
  
setTimeout(function(){  document.querySelector('.cont_form_sign_up').style.opacity = "1";
},100);  

setTimeout(function(){   document.querySelector('.cont_form_login').style.display = "none";
},400);  


}    



function ocultar_login_sign_up() {

document.querySelector('.cont_forms').className = "cont_forms";  
document.querySelector('.cont_form_sign_up').style.opacity = "0";               
document.querySelector('.cont_form_login').style.opacity = "0"; 

setTimeout(function(){
document.querySelector('.cont_form_sign_up').style.display = "none";
document.querySelector('.cont_form_login').style.display = "none";
},500);  
  
}


function loginsubmit() {
    var loginname = $("input[name=loginname]").val();
    var password = $("input[name=password]").val();
    $.ajax({
        url: '../User/LoginSubmit',
        data: {
            loginname: loginname,
            password: password
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
}

function register() {
    var username = $("input[name=username]").val();
    var phone = $("input[name=phone]").val();
    var regpass = $("input[name=regpass]").val();
    var confirmpass = $("input[name=confirmpass]").val();
    var flag = false;
    console.log(regpass);
    if (regpass == confirmpass) {
        flag = true;
    }
    else {
        layer.alert("两次密码输入不一致！", { icon: 0, skin: 'layer-ext-moon' });
    }
    if (flag) {
        $.ajax({
            url: '../User/RegisterSubmit',
            data: {
                username: username,
                phone: phone,
                password: regpass
            },
            type: 'post',
            success: function (data) {
                if (data.Flag == true) {
                    layer.alert("注册成功，请直接登录", { icon: 1, skin: 'layer-ext-moon' });
                } else {
                    layer.alert(data.Message, { icon: 5, skin: 'layer-ext-moon' });
                }
            }
        });
    }
}