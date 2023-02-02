
$.fn.customCheckBox = function (options) {
    var settings = $.extend({
        type: "default"
    }, options);
    var customHTML = "";
    if (settings.type === "toggle") {
        customHTML = "<span class='checkbox-toggle'></span>";
    } else {
        customHTML = "<span class='checkbox-custom'></span>";
    }
    var checkBox = $(this);
    $(checkBox).each(function () {
        $(this).closest("span").contents().unwrap();
        $(this).wrap(customHTML);
        if ($(this).is(':checked')) {
            $(this).parent().addClass("selected");
        }
        else if ($(this).not(':checked')) {
            $(this).parent().removeClass("selected");
        }
        if ($(this).is(":disabled")) {
            $(this).prop("checked", false);
            $(this).parent().removeClass("selected").addClass("disabled");
        }
        else if ($(this).not(':disabled')) {
            $(this).parent().removeClass("disabled");
        }
    });
    $(checkBox).click(function () {
        if ($(this).is(':checked')) {
            $(this).parent().addClass("selected");
        }
        else if ($(this).not(':checked')) {
            $(this).parent().removeClass("selected");
        }
        if ($(this).is(":disabled")) {
            $(this).prop("checked", false);
            $(this).parent().removeClass("selected").addClass("disabled");
        }
        else if ($(this).not(':disabled')) {
            $(this).parent().removeClass("disabled");
        }
    });
};
$.fn.customRadioBtn = function () {
    var radioBtn = $(this);
    $(radioBtn).each(function () {
        $(this).wrap('<span class="radio-custom"></span>');
        if ($(this).is(':checked')) {
            $(this).parent().addClass("selected");
        }
    });
    $(radioBtn).click(function () {
        if ($(this).is(':checked')) {
            $(this).parent().addClass("selected");
        }
        $(radioBtn).not(this).each(function () {
            $(this).parent().removeClass("selected");
        });
    });
};
$.fn.customDropdownList = function () {
    var selectList = $(this);
    $(selectList).each(function () {
        $(this).wrap("<span class='select-wrapper-custom'></span>");
        $(this).after("<span class='select-holder-custom'></span>");
    });
    $(selectList).change(function () {
        var selectedOption = $(this).find(":selected").text();
        $(this).next(".select-holder-custom").text(selectedOption);
    }).trigger('change');
};
$.fn.navDrawer = function (options) {
    var settings = $.extend({
        pushMenu: false,
        buttonAnimation: false,
        overlay: false,
        cookie: true,
        navDrawerContent: "#sidemenuContent",
        navDrawerMenu: "#sidemenuNavbar"
    }, options);
    var config = {
        navDrawerMenuWidth: function () {
            return $(settings.navDrawerMenu).width();
        },
        navDrawerMenuMarginLeft: function () {
            return $(settings.navDrawerMenu).css('margin-left') === '0px' ? - config.navDrawerMenuWidth() : '0px';
        },
        navDrawerContentMarginLeft: function () {
            return $(settings.navDrawerMenu).css('margin-left') === '0px' ? '0px' : config.navDrawerMenuWidth();
        },
        setDefault: function (element) {
            if (settings.pushMenu === true) {
                if ($(window).width() < "992") {
                    $(settings.navDrawerContent).css({ 'margin-left': 0 });
                    $(settings.navDrawerMenu).css({ 'margin-left': - config.navDrawerMenuWidth() });
                }
            } else {
                $(settings.navDrawerContent).css({ 'margin-left': 0 });
            }
        },
        pushMenu: function () {
            if ($(window).width() > "992") {
                $(settings.navDrawerMenu).animate({ 'margin-left': config.navDrawerMenuMarginLeft() }, config.navDrawerMenuWidth());
                $(settings.navDrawerContent).animate({ 'margin-left': config.navDrawerContentMarginLeft() }, config.navDrawerMenuWidth());
            } else {
                config.slideMenu();
            }
        },
        slideMenu: function() {
            $(settings.navDrawerContent).css({ 'margin-left': 0 });
            $(settings.navDrawerMenu).animate({ 'margin-left': config.navDrawerMenuMarginLeft() }, config.navDrawerMenuWidth());
        },
        setCookies: function() {
            if (Cookies.get("navDrawer") === "enable") {
                Cookies.remove('navDrawer');
            } else {
                Cookies.set("navDrawer", "enable");
            }
        },
        checkCookies: function(element) {
            if (Cookies.get("navDrawer") === "enable") {
                $(settings.navDrawerMenu).css({ 'margin-left': - config.navDrawerMenuWidth() });
                $(settings.navDrawerContent).css({ 'margin-left': 0 });

                if (settings.buttonAnimation === "close") {
                    $(element).addClass("icon-close");
                } else if (settings.buttonAnimation === "back") {
                    $(element).addClass("icon-back");
                }
            }
        }
    };
    config.setDefault($(this));
    config.checkCookies($(this));
    $(this).on("click", function (event) {
        event.preventDefault();
        // COOKIE SETUP
        if (settings.cookie === true) {
            config.setCookies();
        }
        // TYPE OF AMIMANTION
        if (settings.pushMenu === true) {
            config.pushMenu();
        } else {
            config.slideMenu();
        }
        // BUTTON ANIMATION
        if (settings.buttonAnimation === "close") {
            $(this).toggleClass("icon-close");
        } else if (settings.buttonAnimation === "back") {
            $(this).toggleClass("icon-back");
        }
    });
};
$.fn.uploadPreview = function (options) {
    var settings = $.extend({
        html: "img",
        preview: ".img-preview"
    }, options);
    var config = {
        state: {
            result: ""
        },
        winPopUp: function (src) {
            var popupWindow = window.open("", "_blank", "width=750, height=600, status=no, resizable=no, toolbar=no");
            popupWindow.document.write('<embed src="' + src + '" width="100%" height="100%">');
        }
    };
    $(this).on("change", function () {
        var element = $(this);
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                config.state.result = e.target.result;
                if (settings.html === "img") {
                    $(settings.preview).attr('src', config.state.result);
                } else {
                    $(element).data("src", config.state.result);
                }
            };
            reader.readAsDataURL(this.files[0]);
        }
        $(element).closest("div.row").find(settings.preview).off("click").on("click", function (event) {
            event.preventDefault();
            config.winPopUp($(element).data("src"));
        });
    });


};
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
$.fn.exportPDF = function (options) {
    var settings = $.extend({
        elHTMLId: "elConvert2PDF",
        allowPrint: false,
        // IF ALLOW PRINT TRUE
        clientWebSocket: "ws://localhost:9090/print",
        allowSelectPrinter: false,
        // IF ALLOW SELECT PRINTER TRUE
        setPrinterDllBtn: "#btnListDll",
        ddlPrinter: "#printerlist",
        margin: 1,
        filename: 'HTML2PDF.pdf',
        jsPDF: { unit: 'cm', format: 'A4', orientation: 'portrait' }
    }, options);
    var config = {
        state: {
            ws: null
        },
        connectPrinter: function () {
            this.state.ws = new WebSocket(settings.clientWebSocket);
            this.state.ws.onopen = function () {
                console.log("Connection Established with Websocket:" + settings.clientWebSocket);
            };
            this.state.ws.onmessage = function (evt) {
                var response = $.parseJSON(evt.data);
                if (response.Code === "000") {
                    if (response.PrinterList !== null) {
                        var html = [];
                        html.push('<option value="">Select Connected Printer</option>');
                        $.each(response.PrinterList, function (key, val) {
                            html.push('<option value="' + val + '">' + val + '</option>');
                        });
                        html.join("\n");
                        $(settings.ddlPrinter).html(html);
                    }
                    else {
                        alert(response.Message);
                    }
                }
                else {
                    alert(response.Message);
                }
            };
            this.state.onclose = function () {
                alert("Connecton is Closed with Websocket:" + settings.clientWebSocket);
            };
        },
        printPDF: function (pdfContent) {
            if (this.state.ws.readyState !== 1) {
                this.connectPrinter();
            }
            if (this.state.ws && $(settings.ddlPrinter).val() !== "") {
                var content = {
                    RequestType: "PrintPDF",
                    PrinterName: $(settings.ddlPrinter).val(),
                    PrintContent: pdfContent
                };
                this.state.ws.send(JSON.stringify(content));
            } else {
                alert("Please Select Connected Printer");
            }
        },
        getPrinterList: function () {
            if (this.state.ws.readyState !== 1) {
                this.connectPrinter();
            }
            if (this.state.ws) {
                var content = {
                    RequestType: "PrinterList"
                };
                this.state.ws.send(JSON.stringify(content));
            }
        }
    };

    var el = document.getElementById(settings.elHTMLId);
    var opt = {
        margin: settings.margin,
        filename: settings.filename,
        image: { type: 'jpeg', quality: 1 },
        html2canvas: { scale: 2 },
        jsPDF: settings.jsPDF
    };
    if (settings.allowPrint === true && settings.allowSelectPrinter === true) {
        config.connectPrinter();
        $(settings.setPrinterDllBtn).on("click", function () {
            config.getPrinterList();
        });
    }
    $(this).click(function () {
        if (settings.allowPrint === true) {
            config.connectPrinter();
            html2pdf().set(opt).from(el).outputPdf().then(function (pdf) {
                config.printPDF(btoa(pdf));
            });
        }
        else {
            html2pdf().set(opt).from(el).save();
        }
    });
};
$.fn.stringToHTMLTable = function (options) {
    var settings = $.extend({

    }, options);
    $(this).each(function (key, val) {
        var str = $(this).text().replace(/\s/g, '').split("");
        var html = "<table style:'width: 100%'><tr>";
        for (var i = 0; i < str.length; i++) {
            html += "<td style='border: 1px solid #333; padding: 2px 8px;'>" + str[i] + "</td>";
        }
        html += "</tr></table>";
        $(this).html(html);
    });
};
$.fn.printContent = function (options) {
    var settings = $.extend({
        element: "#contentPrint"
    }, options);

    var divElement = $(this).html();
    var bodyElement = $("body").html();
    $("body").html(divElement);
    window.print();
    $("body").html(bodyElement);
}
$.fn.submitFormWindow = function (options) {
    var settings = $.extend({
        fullscreen: 0,
        width: "1024",
        height: "1024",
        top: "1024",
        left: "1024",
        target: "_blank",
        baseUrl: window.location.hostname,
        redirectUrl: null
    }, options);
    $(this).on("submit", function (e) {
        e.preventDefault();
        //if (settings.redirectUrl !== null) {

        //}
    });
};
$.fn.stringIsNullOrEmpty = function (options) {
    data = $(this).val();
    if (data === "" || data === undefined || data === null)
        return true;
    return false;
};

/* JQUERY CODE FOR PAGE LOADING SCREEN */
$(window).load(function () {
    $("#loading").fadeOut("slow");
}).unload(function () {
    $("#loading").show();
});
//$(document).ajaxStart(function () {
//    $("#loading").show();
//}).ajaxComplete(function () {
//    $("#loading").hide();
//});

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip()

    $(window).keydown(function (event) {
        if (event.keyCode === 13) {
            event.preventDefault();
            return false;
        }
    });

   

    $(".btnConfirmation").on("click",
        function (event) {
            event.preventDefault();
            Swal.fire({
                title: "Confirmation",
                text: "Are you sure to carry out the operation?",
                type: 'info',
                showCancelButton: true,
                confirmButtonColor: '#2C94FB',
                cancelButtonColor: '#ff2801',
                confirmButtonText: 'Yes'
            }).then((result) => {
                if (!result.value) {
                    event.preventDefault();
                } else {
                    $(location).prop("href", $(this).prop("href"));
                }
            });
        });

    /* CUSTOMIZED CHECKBOX AND RADIO BTN JQUERY PLUGIN */
    $(".custom-checkbox").customCheckBox();
    $(".toggle-checkbox").customCheckBox({
        type: "toggle"
    });
    $(".custom-radio").customRadioBtn();
    $(".custom-dropdown").customDropdownList();

    /* THEME JQUERY PLUGIN FOR NAVIGATION DRAWER */
    $("#btnSidemenu").navDrawer({
        pushMenu: true, // true/false
        buttonAnimation: "back", // back/close/false
        overlay: true // true / false
    });

    /* THEME JQUERY PLUGIN TO CHECK PASSWORD STRENGTH */
    $("#txtNewPassword").pwdStrength({
        div: "#strengthIndicator",
        label: "#strengthLabel"
    });

    /* SLIM SCROLL JQUERY PLUGIN FOR SIDE MENU */
    $('.sidemenu-list').slimScroll({
        height: 'auto',
        size: '4px',
        color: '#ccc',
        railOpacity: 0.3
    });

    /* SLIM SCROLL JQUERY PLUGIN FOR TIMELINE OF SCHEDULE TASK */
    //$("#timelineSidebar").slimScroll({
    //    height: 'auto',
    //    size: '4px',
    //    color: '#ccc',
    //    railOpacity: 0.3
    //});

    /* SLIDE REVEAL JQUERY PLUGIN FOR SCHEDULE TASK */
    var slideRevealScheduleTask = $("#slideRevealScheduleTask").slideReveal({
        trigger: $("#triggerScheduleTask"),
        autoEscape: true,
        position: "right",
        push: false,
        overlay: false,
        width: 300,
        speed: 100
    });
    $("#closeScheduleTask").on("click", function () {
        slideRevealScheduleTask.slideReveal("hide", true);
    });

    /* JQUERY CUSTOM CODE TO SET MENU ACTIVE WITH RESPECT TO URL */
    //$(".sidemenu-list li a").each(function () {
    //    var url = window.location.pathname;
    //    var arrUrl = url.split("/").slice(0, 3);

    //    var str = $(this).attr('href');
    //    var arStr = str.split("/").slice(0, 3);

    //    if (JSON.stringify(arrUrl) === JSON.stringify(arStr)) {
    //        $(this).parent().parent().addClass('in');
    //        $(this).parent().addClass('active');
    //    }
    //});

    /* JQUERY CUSTOM CODE TO SET MENU ACTIVE WITH RESPECT TO URL */
    $(".sidemenu-list li a").each(function () {
        if (window.location.pathname === $(this).attr('href')) {
            $(this).parent().parent().addClass('in');
            $(this).parent().addClass('active');
        }
    });


    /* JQUERY UI AUTOCOMPLETE PLUGIN AND CUSTOM CODE FOR SEARCH MENU */
    var tags = {
        name: [],
        href: []
    };
    $("#accordion").contents().find("a").filter(function () {
        var url = $(this).attr("href");
        if (url.substring(0, 1) !== "#") {
            tags.name.push($.trim($(this).text()));
            tags.href.push($(this).prop("href"));
        }
    });
    $("#tagsMenu").autocomplete({
        source: tags.name,
        select: function (event, ui) {
            var indexTags = tags.name.indexOf(ui.item.label);
            $("#tagsSearch").prop("action", tags.href[indexTags]);
            $('#tagsSearch').submit();
        }
    });

    /* JQUERY CUSTOM CODE FOR ERROR IN PREVIEW IMAGES */
    $("img").on("error", function () {
        $(this).prop("src", "https://localhost:44383/img/no-img.jpg");
    });
    if ($(".img-preview").prop("src") === "") {
        $(".img-preview").prop("src", "https://localhost:44383/img/no-img.jpg");
    }

    //$("form").submit("click", function (e) {
    //    //disable the submit button
    //    if ($("form").valid()) {
    //        $(":submit").attr("disabled", true);
    //    }
    //});

   
});

