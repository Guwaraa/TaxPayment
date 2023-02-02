var PremiumSetupManagement = (function (premiumSetupManagement, $) {
    "use strict";
    premiumSetupManagement.premiumSetup = function () {
        var config = {
            base_url: null,
            file_url: "/",
            img_url: "/img/",
            state: {

            }
        };
        var viewModel = {
            loadJExcel: function (data) {
                var spreadsheet = $('#spreadsheet').jexcel({
                    data: data,
                    colHeaders: ['CCFrom', 'CCTo', 'YaxRate'],
                    colWidths: [400, 400, 400],
                    columns: [
                        {
                            type: 'text',
                            name: 'CCFrom'
                        },
                        {
                            type: 'text',
                            name: 'CCTo'
                        },
                        {
                            type: 'text',
                            name: 'TaxRate'
                        }
                    ],
                    allowInsertColumn: false,
                    allowInsertRow: false,
                    allowDeleteRow: false,
                    csvHeaders: true,
                    tableOverflow: true,
                    onchange: function (instance, cell, x, y, value) {
                        $("#TaxSetupUploadJson").val(viewModel.jexcelToJSON($('#spreadsheet').jexcel('getData'), $('#spreadsheet').jexcel('getHeaders').split(',')));
                    }
                });
                return spreadsheet;
            },
            jexcelToJSON: function (data, headers) {
                var jsonData = JSON.stringify(data.map(x => x.reduce(function (obj, val, index) {
                    obj[headers[index]] = val;
                    return obj;
                }, {})));
                return jsonData;
            }
        };
        return ({
            renderIndex: function () {
                    $("#agentType").DataTable({
                        "responsive": true,
                        "processing": true,
                        "serverSide": true,
                        "ajax": {
                            "type": 'POST',
                            "url": "/PremiumSetup/GetGridDetails",
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
                            { 'data': 'VechicleCategory', "orderable": false },
                            { 'data': 'FiscalYear', "orderable": false },
                            { 'data': 'InsuranceCompany', "orderable": false },
                            { 'data': 'Status', "orderable": false },
                            { 'data': 'Action', "orderable": false }
                        ],
                        "columnDefs":
                            [
                                { "className": "text-center", "targets": [0, 1, 2, 3] }
                            ],
                        "initComplete": function () {
                            $("#agentType").on("click", ".confirmation",
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
                $("#manage-PremiumSetup-form").validate({
                    rules: {
                        VechicleCategory: "required",
                        FiscalYear: "required",
                        Province: "required",
                        InsuranceCompany: "required",
                        InsuranceRate: "required"
                    },
                    messages: {
                        VechicleCategory: "Please select vehicle category.",
                        FiscalYear: "Please select fiscal year.",
                        Province: "Please select province.",
                        InsuranceCompany: "Please select insurance company.",
                        InsuranceRate: "Please enter insurance rate."
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
                $('#UploadFile').on('change', function (e) {
                    $('#spreadsheet').remove(); //need to delete instance of jexcel, so remove the element
                    $('#spreadsheetDiv').append(`<div id="spreadsheet"></div>`); //create element again
                    var file = e.target.files[0];
                    var reader = new FileReader();
                    reader.readAsText(file);
                    reader.onload = function (evt) {
                        let csvData = evt.target.result;
                        let data = $.csv.toObjects(csvData);
                        let spreadsheet = viewModel.loadJExcel(data);
                        debugger;
                        $("#TaxSetupUploadJson").val(viewModel.jexcelToJSON(spreadsheet.getData(), spreadsheet.getHeaders().split(',')));
                    };
                    reader.onerror = function () {
                        alert('Unable to read ' + file.fileName);
                    };
                });
            }
        });
    };

    return premiumSetupManagement;
}(PremiumSetupManagement || {}, jQuery));