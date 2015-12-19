$(function () {

    //селектор поля который хотите сделать дэйтайм.дэйтайм jquery DataPicker

    $(".saveCall").on("click", function () {
        var data = $("#saveCallForm").serialize();
        $.ajax({
            url: 'CustomerCall/SaveCall',
            data: {
                Id: $("#Id").val(),
                DeliveryFrom: $("#DeliveryFrom").val(),
                DeliveryTo: $("#DeliveryTo").val(),
                DeliveryTimeFrom: $("#DeliveryTimeFrom").val().toString(),
                DeliveryTimeTo: $("#DeliveryTimeTo").val().toString(),
                DateDelivery: $("#DateDelivery").val().toString(),
            },
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            success: function (data) {
                alert(111);
                $("#grid").trigger("reloadGrid");
            }
        })
    })

    $("#grid").jqGrid(
        {
            url: "/CustomerCall/GetCustomerCalls",
            datatype: 'json',
            mtype: 'Get',
            colNames: ['# of call', 'Date call', 'Status', 'Call Manager', 'Actions'],
            colModel: [
            { name: 'Id', index: 'Id', width: 150, sortable: true },
            { name: 'date', index: 'date', width: 100, sortable: true },
            { name: 'status', index: 'status', width: 150, sortable: true },
            { name: 'manager', index: 'manager', width: 150, sortable: true },

            { name: 'act', index: 'act', width: 75, sortable: false },
            ],
            pager: jQuery('#pager'),

            rowNum: 10,
            rowList: [10, 20, 30, 40],
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
                    de = "<input style='height:22px;' type='button' onclick='Delete(" + cl + ")' value='Delete'/>";
                    ed = "<input style='height:22px;' type='button' onclick='Edit(" + cl + ")' value='Edit'/>";
                    jQuery("#grid").setRowData(ids[i], { act: ed + de })
                }
            },


        }).navGrid('#pager', { edit: false, add: false, del: false, search: false, refresh: false });

    jQuery("#grid").jqGrid('filterToolbar', { stringResult: true, searchOnEnter: false });



    $('#grid .delete').on('click', function () {
        var id = $(this).parent().attr('id');
        alert("id=" + id);
    });

    $("#grid").jqGrid('navButtonAdd', '#pager',
    {
        caption: "Show"/*"Show"*/, buttonicon: "ui-icon-extlink", title: "Show Link",
        onClickButton: function () {
            var grid = $("#grid");
            var rowid = grid.jqGrid('getGridParam', 'selrow');
            window.location = grid.jqGrid('getCell', rowid, 'status');
        }
    });
});

    function Delete(idCall) {

        $.ajax({
            url: 'CustomerCall/DeleteCall',
            data: { Id: idCall},
            contentType: 'application/json; charset=utf-8',
            type: 'GET',
            success: function () {
                alert(1);
                $("#grid").trigger("reloadGrid");
            }
        })

    }

    function Edit(idCall) {
    

        $.ajax({
            url: 'CustomerCall/GetCall',
            data: { callID: idCall },
            contentType: 'application/json; charset=utf-8',
            type: 'GET',
            success: function (data) {
                $("#Id").val(data.Id);
                $("#DeliveryFrom").val(data.DeliveryFrom);
                $("#DeliveryTo").val(data.DeliveryFrom);
                $("#DeliveryTimeFrom").val(data.DeliveryTimeFrom);
                $("#DeliveryTimeTo").val(data.DeliveryTimeTo);
                $("#DateDelivery").val(data.DateDelivery);
                alert(33);
                $("#grid").trigger("reloadGrid");
            }
        })

    }