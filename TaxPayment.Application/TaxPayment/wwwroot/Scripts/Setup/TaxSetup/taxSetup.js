\var TaxSetupManagement = (function (taxSetupManagement, $) {
    "use strict";
    taxSetupManagement.taxSetup = function () {
        var config = {
            base_url: null,
            file_url: "/",
            img_url: "/img/",
            state: {

            }
        };
        var viewModel = {
            
        };
        return ({
            renderIndex: function () {
              

            },
            renderManage: function () {
                $("#manage-TaxSetups-form").validate({
                    rules: {
                        NmcNo: "required",
                        DoctorName: "required",
                        DoctorNameNative: "required",
                        Dob: "required",
                        MobileNo: "required",
                        Email: {
                            required: true,
                            email:true
                        },
                        Remarks: "required",
                        AssociatedHospital: "required",
                    },
                    messages: {
                        NmcNo: "Please enter the NMC Number.",
                        DoctorName: "Please enter the doctor name.",
                        DoctorNameNative: "Please enter the doctor native name.",
                        Dob: "Please select your Date of Birth.",
                        MobileNo: "Please enter the mobile number.",
                        Email: {
                            required: "Please enter the email.",
                            email: "Please enter valid email.",
                        },
                        Remarks: "Please enter the remarks.",
                        AssociatedHospital: "Please select the associated hospital.",
                    },
                    errorPlacement: function (error, element) {
                        var attrName = $(element).attr("name");
                        error.appendTo($("#" + attrName + "_jserror"));
                    },
                    highlight: function (element, errorClass, validClass) {
                        if ($(element).hasClass("select2-hidden-accessible")) {
                            $(element).next().contents().find(".select2-selection--single").addClass(errorClass);
                        } else {
                            $(element).addClass(errorClass);
                        }
                    },
                    unhighlight: function (element, errorClass, validClass) {
                        if ($(element).hasClass("select2-hidden-accessible")) {
                            $(element).next().contents().find(".select2-selection--single").removeClass(errorClass);
                        } else {
                            $(element).removeClass(errorClass);
                        }
                    }
                });
                $("#AssociatedHospital").select2().on('change', function () {
                    $(this).valid();
                });
                $("#NmcNo").on("change", function () {
                    viewModel.checkNmcNo($(this).val()).success(function (data) {
                        if (data.code == "111") {
                            $("#NmcNo_jserror").text(data.message).addClass("error");
                            $("#NmcNo").addClass("error");
                            $("#btnAddBranch").prop('disabled', true);
                        } else {
                            $("#NmcNo_jserror").text("");
                            $("#NmcNo").removeClass("error");
                            $("#btnAddBranch").prop('disabled', false);

                        }
                    });
                });
            }
        });
    };

    return taxSetupManagement;
}(TaxSetupManagement || {}, jQuery));