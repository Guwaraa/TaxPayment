var TaxPaymentManagement = (function (taxPaymentManagement, $) {
    "use strict";
    taxPaymentManagement.taxPayment = function () {
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
                $("#taxPayment").DataTable({
                    "responsive": true,
                    "processing": true,
                    "serverSide": true,
                    "ajax": {
                        "type": 'POST',
                        "url": "/TaxPayment/GetGridDetails",
                        "data": function (json) {
                            return json;
                        }
                    },
                    "lengthMenu": [
                        [25, 50, 100],
                        [25, 50, 100]
                    ],
                    "columns": [
                        { 'data': 'RowNum', "orderable": false },
                        { 'data': 'KYCCode', "orderable": false },
                        { 'data': 'Province', "orderable": false },
                        { 'data': 'VechicleCategory', "orderable": false },
                        { 'data': 'VechicleNo', "orderable": false },
                        { 'data': 'TaxRate', "orderable": false },
                        { 'data': 'PaidDate', "orderable": false },
                        { 'data': 'Action', "orderable": false }
                    ],
                    "columnDefs":
                        [
                            { "className": "text-center", "targets": [0, 1, 2, 3,4,5,6,7] }
                        ],
                    "initComplete": function () {
                        $("#taxPayment").on("click", ".confirmation",
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
                    }
                });

            },
            renderManage: function () {
                $("#btnApproveDetail").click(function () {
                    debugger
                    $("#hdnApprove").val("Approve")
                });
                $("manage-TaxPayments-form").validate({
                    rules: {
                        FirstName: "required",
                        LastName: "required",
                        DateOfBirth: "required",
                        Gender: "required",
                        ParmanentAddress: "required",
                        CitizenshipNumber: "required",
                        Remarks: "required"
                    },
                    messages: {
                        FirstName: "Please enter first name.",
                        LastName: "Please enter last name.",
                        DateOfBirth: "Please select date of birth",
                        Gender: "Please select gender",
                        ParmanentAddress: "Please enter permanent address.",
                        CitizenshipNumber: "Please enter citizenship number",
                        Remarks: "Please enter citizenship number"
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
            }
        });
    };

    return taxPaymentManagement;
}(TaxPaymentManagement || {}, jQuery));