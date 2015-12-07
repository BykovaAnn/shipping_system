$(function() {
    $("#grid").jqGrid(
        {
            url: "/CustomerCall/GetCustomerCalls",
            datatype: 'json',
            mtype: 'Get',
            colNames: [ 'status', 'manager', 'date', 'date_delivery', 'delivery_from', 'delivery_to', 'delivery_time_from', 'delivery_time_to'],
            colModel: [
                //{ name: 'act', index: 'act', width: 75, sortable: false },
            { name: 'status', index: 'status', width: 150, sortable: true },
            { name: 'manager', index: 'manager', width: 150, sortable: true },
            { name: 'date', index: 'date', width: 100, sortable: false },
            { name: 'date_delivery', index: 'date_delivery', width: 100, sortable: false },
            { name: 'delivery_from', index: 'delivery_from', width: 100, sortable: false },
            { name: 'delivery_to', index: 'delivery_to', width: 100, sortable: false },
            { name: 'delivery_time_from', index: 'delivery_time_from', width: 100, sortable: false },
            { name: 'delivery_time_to', index: 'delivery_time_to', width: 100, sortable: false },
            ],
            pager: jQuery('#pager'),
            rowNum: 10,
            rowList: [10, 20, 30, 40],
            height: '100%',
            viewrecords: true,
            caption: 'Shit polnyi',
            emptyrecords: 'Net shita',
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
            //gridComplete: function () {
            //    //var ids = jQuery("#rowed2").jqGrid('getDataIDs');
                
            //        be = "<input style='height:22px;width:20px;' type='button' value='E' />";
            //        se = "<input style='height:22px;width:20px;' type='button' value='S'/>";
            //        ce = "<input style='height:22px;width:20px;' type='button' value='C'  />";
            //        jQuery("#grid").jqGrid('setRowData', 1, { act: be + se + ce });
            //    }
            //},
        }).navGrid('#pager', {edit: true, add: true, del: true, search: false, refresh: true},
        {
            zIndex: 100,
            url: '/CustomerCall/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function(response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            zIndex: 100,
            url: '/CustomerCall/Create',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function(response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            zIndex: 100,
            url: '/CustomerCall/Delete',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            msg: "Really?",
            afterComplete: function(response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        });
    });