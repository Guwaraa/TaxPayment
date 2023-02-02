function getTodayDate(format, date = null) {
    if (date === null) {
        date = new Date();
    }
    var dd = String(date.getDate()).padStart(2, '0');
    var mm = String(date.getMonth() + 1).padStart(2, '0');
    var yyyy = date.getFullYear();
    if (format === "mm/dd/yyyy") {
        return today = mm + '/' + dd + '/' + yyyy;
    }
    else if (format === "yyyy-mm-dd") {
        return today = yyyy + '-' + mm + '-' + dd;
    }
    else if (format === "mm-dd-yyyy") {
        return today = mm + '-' + dd + '-' + yyyy;
    }
    else {
        return false;
    }
}

function calcAge(dateString) {
    var birthday = +new Date(dateString);
    return ~~((Date.now() - birthday)/(31557600000));
}

function ft2CM(value) {
    var height = value.split('.');
    if (height[1] === '')
        height[1] = 0;
    var feet = parseFloat(height[0] * 30.48).toFixed(2);
    var inch = parseFloat(height[1] * 2.54).toFixed(2);
    if (inch === 'undefined' || inch === 'NaN')
        inch = 0;
    if (inch === 'undefined' || inch === 'NaN')
        inch = 0;
    var cm = (parseFloat(feet) + parseFloat(inch)).toFixed(2);
    return Math.round(cm);
}

function getQueryString(field, url) {
    var href = url ? url : window.location.href;
    var reg = new RegExp('[?&]' + field + '=([^&#]*)', 'i');
    var string = reg.exec(href);
    return string ? string[1] : null;
}

function setKeyboardLayout(keyboard) {
    if (keyboard === 'UNICODE') {
        InitializeUnicodeNepali();
    } else{
        $('.local-language').on('keyup', function () {
            switch (keyboard) {
                case 'PCSNEPALI':
                    $(this).val(preeti($(this).val(), 'PCS Nepali'));
                case 'KANTIPUR':
                    $(this).val(preeti($(this).val(), 'Kantipur'));
                default:
                    $(this).val(preeti($(this).val()));
            }
        });
    }
}