$.fn.pwdStrength = function (options) {
    var settings = $.extend({
        div: "#strengthIndicator",
        label: "#strengthLabel"
    }, options);
    $(this).keyup(function () {
        txtPwdValue = $(this).val();
        var strength = 0;
        if (txtPwdValue.length === 0) {
            $(settings.div).removeClass();
            $(settings.label).html('Empty');
        }
        else if (txtPwdValue.length < 6) {
            $(settings.div).removeClass();
            $(settings.div).addClass('strength-danger');
            $(settings.label).html('Too Short');
        }
        else {
            strength += 1;
            if (txtPwdValue.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/)) { strength += 1; }
            if (txtPwdValue.match(/([a-zA-Z])/) && txtPwdValue.match(/([0-9])/)) { strength += 1; }
            if (txtPwdValue.match(/([!,%,&,@,#,$,^,*,?,_,~])/)) { strength += 1; }
            if (txtPwdValue.match(/(.*[!,%,&,@,#,$,^,*,?,_,~].*[!,%,&,@,#,$,^,*,?,_,~])/)) { strength += 1; }

            if (strength < 2) {
                $(settings.div).removeClass().addClass('strength-danger');
                $(settings.label).html('Weak');
            }
            else if (strength === 2) {
                $(settings.div).removeClass().addClass('strength-warning');
                $(settings.label).html('Good');
            }
            else {
                $(settings.div).removeClass().addClass('strength-success');
                $(settings.label).html('Strong');
            }
        }
    });
};

$("#NewPassword").pwdStrength({
    div: "#strengthIndicator",
    label: "#strengthLabel"
});