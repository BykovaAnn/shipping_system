$(function () {

    //обработка нажатия на кнопку SaveCall
    $("#saveCall").on("click", function () {
        if ($("#Id").val().toString() == "" || $("#Id").val().toString() == "0")
            alert("New call is adding...");
        else
            alert("Existing call is editing...");
        var id_call = $("#Id").val().toString();
        var d_from = $("#DeliveryFrom").val().toString();
        var d_to = $("#DeliveryTo").val().toString();
        var d = $("#DateDelivery").val().toString();
        var dt_from = $("#DeliveryTimeFrom").val().toString();
        var dt_to = $("#DeliveryTimeTo").val().toString();
        SaveCall(id_call, d_from, d_to, d, dt_from, dt_to);
        $("#Id").val("");
        $("#DeliveryFrom").val("");
        $("#DeliveryTo").val("");
        $("#DeliveryTimeFrom").val("");
        $("#DeliveryTimeTo").val("");
        $("#DateDelivery").val("");
    });

    //функция отображения таблицы
    $("#grid").jqGrid(
        {
            url: "/CustomerCall/GetCustomerCalls",
            datatype: 'json',
            mtype: 'Get',
            colNames: ['# of call', 'Date call', 'Status', 'Call Manager', 'Actions'],
            colModel: [
            { name: 'Id', index: 'Id', width: 100, sortable: true },
            { name: 'date', index: 'date', width: 100, sortable: true },
            { name: 'status', index: 'status', width: 100, sortable: true },
            { name: 'manager', index: 'manager', width: 100, sortable: true },

            { name: 'act', index: 'act', width: 75, sortable: false },
            ],
            pager: jQuery('#pager'),

            rowNum: 10,
            rowList: [10, 20, 30, 40],
            width: 475,
            height: '100%',
            viewrecords: true,
            caption: 'All calls',
            emptyrecords: 'No calls yet',
            jsonReader: {
                root: "rows",
                page: "page",
                total: "total",
                records: "records",
                repeatittems: false,
                id: "0"
            },
            autowidth: true,
            multiselect: false,

            gridComplete: function () {
                var ids = jQuery("#grid").getDataIDs();
                for (var i = 0; i < ids.length; i++) {
                    var cl = ids[i];
    
                    de = "<input style='height:22px;' type='button' onclick='Delete(" + cl + ")' value='Cancel call'/>";
                    ed = "<input style='height:22px;' type='button' onclick='Edit(" + cl + ")' value='Edit call'/>";
                    jQuery("#grid").setRowData(ids[i], { act: ed + de })
                }
            },


        }).navGrid('#pager', { edit: false, add: false, del: false, search: true, refresh: false });

    jQuery("#grid").jqGrid('filterToolbar', { stringResult: true});

});

    //функция обработки события нажатия на кнопку отмены в таблице 
    function Delete(idCall) {

        $.ajax({
            url: 'CustomerCall/DeleteCall',
            data: { Id: idCall},
            contentType: 'application/json; charset=utf-8',
            type: 'GET',
            success: function (data) {
                alert(data.result);
                $("#grid").trigger("reloadGrid");
            }
        })

    }

    //функция обработки события нажатия на кнопку редактирования в таблице 
    function Edit(idCall) {
    

        $.ajax({
            url: 'CustomerCall/GetCall',
            data: { callID: idCall },
            contentType: 'application/json; charset=utf-8',
            type: 'GET',
            success: function (data) {
                $("#Id").val(data.Id);
                $("#DeliveryFrom").val(data.DeliveryFrom);
                $("#DeliveryTo").val(data.DeliveryTo);
                $("#DeliveryTimeFrom").val(data.DeliveryTimeFrom);
                $("#DeliveryTimeTo").val(data.DeliveryTimeTo);
                $("#DateDelivery").val(data.DateDelivery_s);
                $("#grid").trigger("reloadGrid");
            }
        })

    }

    //функция обработки события нажатия на кнопку сохранения изменений 
    function SaveCall(id_call, d_from, d_to, d, dt_from, dt_to) {

        $.ajax({
            url: 'CustomerCall/SaveCall',
            data: {
                callID: id_call,
                DeliveryFrom: d_from,
                DeliveryTo: d_to,
                DateDelivery: d,
                DeliveryTimeFrom: dt_from,
                DeliveryTimeTo: dt_to
            },
            contentType: 'application/json; charset=utf-8',
            type: 'GET',
            success: function (data) {
                alert(data.result);
                $("#grid").trigger("reloadGrid");
            }
        })

    }